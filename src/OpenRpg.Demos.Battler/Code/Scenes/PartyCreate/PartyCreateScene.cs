using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Builders;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;
using OpenRpg.Demos.Battler.Code.Scenes.CharacterMenu;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.PartyCreate;

public class PartyCreateScene : IScene
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly GameCharacterBuilder _characterBuilder;
    private readonly ISceneManager _sceneManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IPersistentGameState _gameState;
    private readonly IGameServices _gameServices;

    private bool _loaded;
    private SpriteFont _font;
    private Texture2D _pixel;
    private KeyboardState _previousKeyboard;

    // Pre-computed stat previews for all 5 classes
    private readonly List<ClassPreviewInfo> _classPreviews = [];

    // The classes the player has chosen (in order)
    private readonly List<int> _chosenClassIds = [];

    // Which class in the available list has focus (index into AllClassIds order)
    private int _selectedClassIndex;

    private class ClassPreviewInfo
    {
        public int ClassId;
        public string Name;
        public string Description;
        public string AssetCode;
        public int MaxHp;
        public int Attack;
        public int Defense;
        public int Speed;
        public int MaxMana;
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Wisdom;
        public int Charisma;
    }

    public PartyCreateScene(
        IDataSource dataSource,
        ILocaleDataSource localeDataSource,
        GameCharacterBuilder characterBuilder,
        ISceneManager sceneManager,
        IServiceProvider serviceProvider,
        IPersistentGameState gameState,
        IGameServices gameServices)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _characterBuilder = characterBuilder;
        _sceneManager = sceneManager;
        _serviceProvider = serviceProvider;
        _gameState = gameState;
        _gameServices = gameServices;
    }

    public Task LoadAsync()
    {
        try
        {
            _loaded = false;
            _chosenClassIds.Clear();
            _selectedClassIndex = 0;

            var content = _gameServices.GetContentManager;
            _font = content.Load<SpriteFont>("Fonts/KenneyPixel");
            _pixel = new Texture2D(_gameServices.GetSpriteBatch.GraphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);

            // Pre-build characters for each class to extract real stats
            _classPreviews.Clear();
            foreach (var classId in ClassLookups.AllClassIds)
            {
                var template = _dataSource.Get<ClassTemplate>(classId);
                if (template == null) continue;

                var name = _localeDataSource.Get("en-gb", template.NameLocaleId);
                var desc = _localeDataSource.Get("en-gb", template.DescriptionLocaleId);

                var character = _characterBuilder
                    .CreateNew()
                    .WithRaceId(RaceLookups.Human)
                    .WithClassId(classId, 1)
                    .WithName(name)
                    .Build();

                var assetCode = character.Variables.AssetCode;

                _classPreviews.Add(new ClassPreviewInfo
                {
                    ClassId = classId,
                    Name = name,
                    Description = desc,
                    AssetCode = assetCode,
                    MaxHp = character.Stats.MaxHealth,
                    Attack = (int)character.Stats.Damage,
                    Defense = (int)character.Stats.Defense,
                    Speed = (int)character.Stats.MovementSpeed,
                    MaxMana = (int)character.Stats.MaxMana,
                    Strength = (int)character.Stats.Strength,
                    Dexterity = (int)character.Stats.Dexterity,
                    Constitution = (int)character.Stats.Constitution,
                    Intelligence = (int)character.Stats.Intelligence,
                    Wisdom = (int)character.Stats.Wisdom,
                    Charisma = (int)character.Stats.Charisma
                });
            }

            _loaded = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PartyCreate] FAILED TO LOAD: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        return Task.CompletedTask;
    }

    public void Unload()
    {
        _pixel?.Dispose();
        _pixel = null;
        _font = null;
    }

    public void Update(GameTime gameTime)
    {
        if (!_loaded) return;

        var currentKeyboard = Keyboard.GetState();

        var isPartyFull = _chosenClassIds.Count >= 4;
        // When full: classes (0-4) + Proceed (5) + Random (6)
        // When not full: classes (0-4) + Random (5)
        var randomIndex = ClassLookups.AllClassIds.Length + (isPartyFull ? 1 : 0);
        var proceedIndex = ClassLookups.AllClassIds.Length;
        var maxIndex = randomIndex;

        if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Up))
        {
            if (_selectedClassIndex == randomIndex)
            {
                // Random → Proceed (if full) or last class
                _selectedClassIndex = isPartyFull ? proceedIndex : ClassLookups.AllClassIds.Length - 1;
            }
            else if (isPartyFull && _selectedClassIndex == proceedIndex)
            {
                _selectedClassIndex = ClassLookups.AllClassIds.Length - 1;
            }
            else
            {
                _selectedClassIndex = Math.Max(0, _selectedClassIndex - 1);
            }
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Down))
        {
            if (_selectedClassIndex < ClassLookups.AllClassIds.Length - 1)
            {
                _selectedClassIndex = _selectedClassIndex + 1;
            }
            else if (_selectedClassIndex == ClassLookups.AllClassIds.Length - 1)
            {
                // Last class → Proceed (if full) or Random
                _selectedClassIndex = isPartyFull ? proceedIndex : randomIndex;
            }
            else if (isPartyFull && _selectedClassIndex == proceedIndex)
            {
                _selectedClassIndex = randomIndex;
            }
            else
            {
                _selectedClassIndex = Math.Min(maxIndex, _selectedClassIndex + 1);
            }
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Enter) ||
                 InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Space))
        {
            if (_selectedClassIndex == randomIndex)
            {
                // Random party — fill remaining slots randomly
                FillRemainingRandomly();
                FinalizeParty();
            }
            else if (isPartyFull && _selectedClassIndex == proceedIndex)
            {
                // Proceed with current party
                FinalizeParty();
            }
            else
            {
                // Try to add the selected class to the party
                var classId = GetSelectedClassId();
                if (classId > 0 && !_chosenClassIds.Contains(classId))
                {
                    _chosenClassIds.Add(classId);
                    _selectedClassIndex = 0;
                }
            }
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Escape) ||
                 InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Back))
        {
            if (_chosenClassIds.Count > 0)
            {
                // Undo the last selection
                _chosenClassIds.RemoveAt(_chosenClassIds.Count - 1);
                if (_selectedClassIndex > ClassLookups.AllClassIds.Length - 1)
                    _selectedClassIndex = ClassLookups.AllClassIds.Length - 1;
            }
        }

        _previousKeyboard = currentKeyboard;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // World layer - nothing for party creation
    }

    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;

        // Title
        TextHelper.DrawStringWithSpacing(spriteBatch, _font, "CREATE YOUR PARTY",
            new Vector2(400, 8), Color.White, centered: true);

        var isPartyFull = _chosenClassIds.Count >= 4;
        var panelY = 36;
        var panelH = 275;

        // ===================================================================
        // LEFT PANEL: Available Classes
        // ===================================================================
        var leftX = 20;
        var leftW = 370;

        UiHelper.DrawPanel(spriteBatch, _pixel, leftX, panelY, leftW, panelH, Palette.PanelBg);
        UiHelper.DrawTitleBar(spriteBatch, _pixel, leftX + 1, panelY + 1, leftW - 2, 20);

        TextHelper.DrawStringWithSpacing(spriteBatch, _font, "AVAILABLE CLASSES",
            new Vector2(leftX + 12, panelY + 4), Palette.UiSectionTitle);

        var listY = panelY + 28;
        for (var i = 0; i < _classPreviews.Count; i++)
        {
            var info = _classPreviews[i];
            var isSelected = i == _selectedClassIndex && !isPartyFull;
            var isChosen = _chosenClassIds.Contains(info.ClassId);

            // Highlight the selected row
            if (isSelected)
                UiHelper.DrawSelectionHighlight(spriteBatch, _pixel, leftX + 4, listY - 2, leftW - 8, 22);

            var prefix = isSelected ? "> " : "  ";
            var suffix = isChosen ? " (selected)" : "";

            var textColor = isChosen
                ? new Color(100, 160, 100)
                : isSelected
                    ? Color.Lime
                    : new Color(180, 180, 190);

            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                $"{prefix}{info.Name}{suffix}",
                new Vector2(leftX + 16, listY), textColor);

            listY += 24;
        }

        // Description of the selected class
        var selectedInfo = GetSelectedPreview();
        if (selectedInfo != null)
        {
            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                selectedInfo.Description,
                new Vector2(leftX + 12, panelY + panelH - 50),
                Palette.UiTextLabel);

            // Asset code / sprite hint
            var spriteLabel = $"[Sprite: {selectedInfo.AssetCode}]";
            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                spriteLabel,
                new Vector2(leftX + 12, panelY + panelH - 28),
                Palette.UiTextLabel * 0.7f);
        }

        // ===================================================================
        // RIGHT PANEL: Your Party
        // ===================================================================
        var rightX = 410;
        var rightW = 370;

        UiHelper.DrawPanel(spriteBatch, _pixel, rightX, panelY, rightW, panelH, Palette.PanelBg);
        UiHelper.DrawTitleBar(spriteBatch, _pixel, rightX + 1, panelY + 1, rightW - 2, 20);

        TextHelper.DrawStringWithSpacing(spriteBatch, _font, "YOUR PARTY",
            new Vector2(rightX + 12, panelY + 4), Palette.UiSectionTitle);

        var slotY = panelY + 28;
        for (var i = 0; i < 4; i++)
        {
            var isFilled = i < _chosenClassIds.Count;
            var slotText = isFilled ? GetClassName(_chosenClassIds[i]) : $"---";

            var slotColor = isFilled
                ? new Color(180, 230, 180)
                : new Color(80, 80, 90);

            // Slot number
            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                $"{i + 1}. {slotText}",
                new Vector2(rightX + 20, slotY), slotColor);

            slotY += 28;
        }

        // Selection count
        var statusY = panelY + 148;
        if (_chosenClassIds.Count > 0)
        {
            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                $"[{_chosenClassIds.Count}/4 selected]",
                new Vector2(rightX + 12, statusY),
                new Color(160, 200, 160));
        }

        // Proceed button (only when party is full)
        var buttonStartY = statusY + 20;
        if (isPartyFull)
        {
            var proceedY = buttonStartY;
            var isProceedSel = _selectedClassIndex == ClassLookups.AllClassIds.Length;

            UiHelper.DrawButton(spriteBatch, _pixel,
                rightX + 20, proceedY - 4, rightW - 40, 30,
                isProceedSel,
                fillColor: new Color(20, 45, 25),
                selectedFillColor: new Color(40, 90, 50),
                borderColor: new Color(30, 60, 35),
                selectedBorderColor: new Color(80, 160, 100));

            TextHelper.DrawStringWithSpacing(spriteBatch, _font,
                ">>> PROCEED <<<",
                new Vector2(rightX + rightW / 2, proceedY + 2),
                isProceedSel ? Color.Lime : new Color(100, 200, 100),
                centered: true);

            buttonStartY = proceedY + 36;
        }

        // Random button (always visible)
        var randomSelIndex = isPartyFull
            ? ClassLookups.AllClassIds.Length + 1
            : ClassLookups.AllClassIds.Length;
        var isRandomSel = _selectedClassIndex == randomSelIndex;

        UiHelper.DrawButton(spriteBatch, _pixel,
            rightX + 20, buttonStartY - 4, rightW - 40, 30,
            isRandomSel);

        TextHelper.DrawStringWithSpacing(spriteBatch, _font,
            _chosenClassIds.Count > 0 ? "[ Random Party ]" : ">>> RANDOM PARTY <<<",
            new Vector2(rightX + rightW / 2, buttonStartY + 2),
            isRandomSel ? Color.LightBlue : new Color(130, 140, 160),
            centered: true);

        // ===================================================================
        // BOTTOM PANEL: Stats Preview
        // ===================================================================
        var statsY = 326;
        var statsH = 190;

        UiHelper.DrawPanel(spriteBatch, _pixel, leftX, statsY, 760, statsH, Palette.PanelBg);
        UiHelper.DrawTitleBar(spriteBatch, _pixel, leftX + 1, statsY + 1, 758, 20);

        TextHelper.DrawStringWithSpacing(spriteBatch, _font,
            $"{selectedInfo?.Name ?? "?"} STATS",
            new Vector2(leftX + 12, statsY + 4), Palette.UiSectionTitle);

        if (selectedInfo != null)
        {
            var col1X = leftX + 20;
            var col2X = leftX + 280;
            var col3X = leftX + 540;
            var statRowY = statsY + 30;

            // Row 1: HP, MP, Attack
            DrawStat(spriteBatch, col1X, statRowY, "HP", selectedInfo.MaxHp.ToString(), Palette.HpGreen);
            DrawStat(spriteBatch, col2X, statRowY, "MP", selectedInfo.MaxMana.ToString(), Palette.MpText);
            DrawStat(spriteBatch, col3X, statRowY, "Attack", selectedInfo.Attack.ToString(), Color.White);

            // Row 2: Defense, Speed, --
            DrawStat(spriteBatch, col1X, statRowY + 24, "Defense", selectedInfo.Defense.ToString(), Color.White);
            DrawStat(spriteBatch, col2X, statRowY + 24, "Speed", selectedInfo.Speed.ToString(), Color.White);

            // Divider
            TextHelper.DrawStringWithSpacing(spriteBatch, _font, "ATTRIBUTES",
                new Vector2(leftX + 12, statRowY + 56), Palette.UiSectionTitle);

            // Row 3: STR, DEX, CON
            DrawStat(spriteBatch, col1X, statRowY + 78, "STR", selectedInfo.Strength.ToString(), new Color(240, 180, 80));
            DrawStat(spriteBatch, col2X, statRowY + 78, "DEX", selectedInfo.Dexterity.ToString(), new Color(80, 200, 80));
            DrawStat(spriteBatch, col3X, statRowY + 78, "CON", selectedInfo.Constitution.ToString(), new Color(180, 120, 80));

            // Row 4: INT, WIS, CHA
            DrawStat(spriteBatch, col1X, statRowY + 102, "INT", selectedInfo.Intelligence.ToString(), new Color(80, 140, 240));
            DrawStat(spriteBatch, col2X, statRowY + 102, "WIS", selectedInfo.Wisdom.ToString(), new Color(140, 180, 220));
            DrawStat(spriteBatch, col3X, statRowY + 102, "CHA", selectedInfo.Charisma.ToString(), new Color(220, 140, 220));
        }

        // ===================================================================
        // Help text at bottom
        // ===================================================================
        var helpY = 565;
        var helpText = isPartyFull
            ? "Up/Down Navigate  |  Enter Proceed  |  Back Undo"
            : "Up/Down Browse  |  Enter Select / Random  |  Back Undo";
        TextHelper.DrawStringWithSpacing(spriteBatch, _font,
            helpText,
            new Vector2(400, helpY), Palette.UiHelpText, centered: true);
    }

    // ========================================================================
    // Helpers
    // ========================================================================

    private int GetSelectedClassId()
    {
        if (_selectedClassIndex >= 0 && _selectedClassIndex < _classPreviews.Count)
            return _classPreviews[_selectedClassIndex].ClassId;
        return 0;
    }

    private ClassPreviewInfo GetSelectedPreview()
    {
        // When browsing after party is full, we still want to show a preview
        // Use the valid class index, clamped
        var idx = Math.Min(_selectedClassIndex, _classPreviews.Count - 1);
        if (idx >= 0 && idx < _classPreviews.Count)
            return _classPreviews[idx];
        return null;
    }

    private string GetClassName(int classId)
    {
        var info = _classPreviews.FirstOrDefault(c => c.ClassId == classId);
        return info?.Name ?? $"Class #{classId}";
    }

    private void FillRemainingRandomly()
    {
        var remaining = ClassLookups.AllClassIds
            .Where(id => !_chosenClassIds.Contains(id))
            .ToList();

        var rng = new Random();
        while (_chosenClassIds.Count < 4 && remaining.Count > 0)
        {
            var idx = rng.Next(remaining.Count);
            _chosenClassIds.Add(remaining[idx]);
            remaining.RemoveAt(idx);
        }
    }

    private void FinalizeParty()
    {
        // Initialize the game state with the chosen party composition
        _gameState.InitializeParty(_chosenClassIds.ToArray());

        // Transition to Character Menu
        var menuScene = _serviceProvider.GetRequiredService<CharacterMenuScene>();
        _ = _sceneManager.SetScene(menuScene);
    }

    // ========================================================================
    // Drawing Helpers
    // ========================================================================

    private void DrawStat(SpriteBatch sb, int x, int y, string label, string value, Color valueColor)
    {
        TextHelper.DrawStringWithSpacing(sb, _font, $"{label}:",
            new Vector2(x, y), Palette.UiTextLabel);
        var labelW = TextHelper.MeasureStringWidth(_font, $"{label}:");
        TextHelper.DrawStringWithSpacing(sb, _font, value,
            new Vector2(x + labelW + 6, y), valueColor);
    }
}
