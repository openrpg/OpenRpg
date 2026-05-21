using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Providers;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;
using OpenRpg.Demos.Battler.Code.Services;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Genres.Requirements;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle;

public class BattleScene : IScene
{
    private readonly IPersistentGameState _gameState;
    private readonly IEnemyFormationProvider _enemyFormationProvider;
    private readonly IGameServices _gameServices;
    private readonly ISceneManager _sceneManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDataSource _dataSource;
    private readonly ICharacterRequirementChecker _requirementChecker;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly IEntityAttackGenerator _attackGenerator;
    private readonly IEntityAttackProcessor _attackProcessor;
    private readonly ILootService _lootService;
    private readonly TurnManager _turnManager;
    private readonly SpriteCache _spriteCache = new();
    private readonly EntityRenderer _entityRenderer = new();
    private readonly List<FloatingDamageNumber> _floatingNumbers = [];

    private BattleBottomPanelUi _bottomPanel;
    private TurnOrderUi _turnOrderUi;
    private CombatLogUi _combatLogUi;
    private CommandMenuUi _commandMenu;
    private KeyboardState _previousKeyboard;
    private double _totalTime;
    private SpriteFont _font;
    private bool _loaded;

    // State for post-battle results
    private List<ItemData> _lootItems = [];
    private string _victoryMessage = "";

    public List<BattleEntity> Party { get; private set; } = [];
    public List<BattleEntity> Enemies { get; private set; } = [];

    public BattleScene(
        IPersistentGameState gameState,
        IEnemyFormationProvider enemyFormationProvider,
        IGameServices gameServices,
        ISceneManager sceneManager,
        IServiceProvider serviceProvider,
        IDataSource dataSource,
        ICharacterRequirementChecker requirementChecker,
        ILocaleDataSource localeDataSource,
        IEntityAttackGenerator attackGenerator,
        IEntityAttackProcessor attackProcessor,
        ILootService lootService)
    {
        _gameState = gameState;
        _enemyFormationProvider = enemyFormationProvider;
        _gameServices = gameServices;
        _sceneManager = sceneManager;
        _serviceProvider = serviceProvider;
        _dataSource = dataSource;
        _requirementChecker = requirementChecker;
        _localeDataSource = localeDataSource;
        _attackGenerator = attackGenerator;
        _attackProcessor = attackProcessor;
        _lootService = lootService;
        _turnManager = new TurnManager(_dataSource, _requirementChecker, _localeDataSource, _attackGenerator, _attackProcessor);
    }

    public async Task LoadAsync()
    {
        try
        {
            _loaded = false;
            _floatingNumbers.Clear();

            Party = _gameState.Party;
            Enemies = await _enemyFormationProvider.GenerateFormationAsync();
            LayoutEntities();

            foreach (var e in Party.Concat(Enemies))
                e.OriginPosition = e.Position;

            var content = _gameServices.GetContentManager;
            _spriteCache.LoadSprites(Party.Concat(Enemies), content);
            _turnManager.OnDamageDealt += OnDamageDealt;
            _turnManager.Start(Party, Enemies);

            _font = content.Load<SpriteFont>("Fonts/KenneyPixel");
            _turnOrderUi = new TurnOrderUi(_font);
            _bottomPanel = new BattleBottomPanelUi();
            _bottomPanel.Update(Party, Enemies);
            _turnOrderUi.Update(_turnManager.TurnOrder, _turnManager.CurrentTurnIndex);
            _combatLogUi = new CombatLogUi();
            _combatLogUi.Update("");

            _commandMenu = new CommandMenuUi();
            _commandMenu.OnActionConfirmed += OnPlayerActionConfirmed;

            _loaded = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BattleScene] FAILED TO LOAD: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    public void Unload()
    {
        _turnManager.OnDamageDealt -= OnDamageDealt;
        _commandMenu?.Hide();
        _turnOrderUi.Unload();
        _bottomPanel.Unload();
        _combatLogUi.Unload();
        _entityRenderer.Dispose();
        _font = null;
    }

    private void OnDamageDealt(BattleEntity target, int damage, bool isCrit)
    {
        var slotW = 80;
        var cx = target.Position.X + slotW / 2;
        var cy = target.Position.Y - 10;
        _floatingNumbers.Add(new FloatingDamageNumber(
            new Vector2(cx, cy), damage, isCrit, isHealing: damage < 0));
    }

    public void Update(GameTime gameTime)
    {
        if (!_loaded) return;
        _totalTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_turnManager.CurrentPhase == TurnManager.Phase.GameOver)
        {
            // On first frame of game over, collect loot if player won
            if (_lootItems.Count == 0 && _turnManager.WinningTeam == Team.Player)
            {
                var deadEnemies = Enemies.Where(e => !e.IsAlive).ToList();
                _lootItems = _lootService.GenerateLoot(deadEnemies);
                _gameState.SharedInventory.AddRange(_lootItems);
                _victoryMessage = BuildLootMessage();
            }

            var kstate = Keyboard.GetState();
            if (InputHelper.IsKeyJustPressed(kstate, _previousKeyboard, Keys.Space) ||
                InputHelper.IsKeyJustPressed(kstate, _previousKeyboard, Keys.Enter))
            {
                // On loss, reset the party so a fresh one is created next time
                if (_turnManager.WinningTeam == Team.Enemy)
                    _gameState.ResetParty();

                var menuScene = _serviceProvider.GetRequiredService<CharacterMenu.CharacterMenuScene>();
                _ = _sceneManager.SetScene(menuScene);
            }
            _previousKeyboard = kstate;
            return;
        }

        AnimateEntities();
        _turnManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        for (var i = _floatingNumbers.Count - 1; i >= 0; i--)
        {
            _floatingNumbers[i].Update(dt);
            if (_floatingNumbers[i].IsExpired)
                _floatingNumbers.RemoveAt(i);
        }

        if (_turnManager.IsInPlayerInput)
        {
            var currentKeyboard = Keyboard.GetState();

            if (_commandMenu.IsHidden)
                {
                    var aliveEnemies = Enemies.Where(e => e.IsAlive).ToList();
                    var aliveParty = Party.Where(e => e.IsAlive).ToList();
                    var abilities = _turnManager.GetAvailableAbilities(_turnManager.CurrentAttacker);
                    _commandMenu.Show(_turnManager.CurrentAttacker, aliveEnemies, abilities, _localeDataSource,
                        _gameState.SharedInventory, aliveParty, _dataSource);
                }

            _commandMenu.HandleInput(currentKeyboard, _previousKeyboard);
            _previousKeyboard = currentKeyboard;

            _turnManager.HighlightedTargets = _commandMenu.PreviewTargets;
        }
        else if (!_commandMenu.IsHidden)
        {
            _commandMenu.Hide();
            _turnManager.HighlightedTargets = [];
        }

        _bottomPanel.Update(Party, Enemies);
        _turnOrderUi.Update(_turnManager.TurnOrder, _turnManager.CurrentTurnIndex, GetPulseBrightness());
        _combatLogUi.Update(_turnManager.LastActionMessage);
    }

    private void OnPlayerActionConfirmed(PlayerAction action)
    {
        // Deduct used items from shared inventory immediately
        if (action.Type == ActionType.UseItem && action.UsedItem != null)
        {
            var index = _gameState.SharedInventory.FindIndex(i => i.TemplateId == action.UsedItem.TemplateId);
            if (index >= 0)
                _gameState.SharedInventory.RemoveAt(index);
        }

        _turnManager.SubmitPlayerAction(action);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;
        _entityRenderer.EnsureTextures(spriteBatch.GraphicsDevice);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _entityRenderer.DrawBackground(spriteBatch);
        _entityRenderer.DrawEntities(spriteBatch, Enemies, _turnManager, _totalTime);
        _entityRenderer.DrawEntities(spriteBatch, Party, _turnManager, _totalTime);

        spriteBatch.End();
    }

    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;

        foreach (var fn in _floatingNumbers)
        {
            var text = fn.IsCrit ? $"CRIT! {fn.Damage}" : (fn.IsHealing ? $"+{fn.Damage}" : fn.Damage.ToString());
            var size = _font.MeasureString(text);
            var pos = new Vector2(fn.Position.X - size.X / 2, fn.Position.Y);
            var color = fn.Color * fn.Opacity;
            TextHelper.DrawStringWithSpacing(spriteBatch, _font, text, pos, color);
        }

        _combatLogUi.Draw(spriteBatch, _font);
        _turnOrderUi.Draw(spriteBatch);
        _bottomPanel.Draw(spriteBatch, _font);

        _commandMenu.Draw(spriteBatch, _font);

        if (_turnManager.CurrentPhase == TurnManager.Phase.GameOver)
            DrawResultsOverlay(spriteBatch);
    }

    private float GetPulseBrightness()
    {
        if (_turnManager.CurrentPhase != TurnManager.Phase.TurnDwell &&
            _turnManager.CurrentPhase != TurnManager.Phase.PlayerInput) return 0;
        return (float)(0.5 + Math.Sin(_totalTime * 10) * 0.5);
    }

    private void AnimateEntities()
    {
        for (var i = 0; i < Enemies.Count; i++)
        {
            var e = Enemies[i];
            if (!e.IsAlive) continue;
            var phase = i * 1.3;
            var offX = Math.Sin(_totalTime * 1.2 + phase) * 2.0;
            var offY = Math.Cos(_totalTime * 0.9 + phase) * 1.5;
            e.Position = e.OriginPosition + new Vector2((float)offX, (float)offY);
        }

        for (var i = 0; i < Party.Count; i++)
        {
            var e = Party[i];
            if (!e.IsAlive) continue;
            var phase = i * 1.7 + 3.0;
            var offX = Math.Sin(_totalTime * 1.0 + phase) * 2.0;
            var offY = Math.Cos(_totalTime * 1.1 + phase) * 1.5;
            e.Position = e.OriginPosition + new Vector2((float)offX, (float)offY);
        }
    }

    private string BuildLootMessage()
    {
        if (_lootItems.Count == 0)
            return "No items were dropped.";

        var itemNames = new List<string>();
        foreach (var item in _lootItems)
        {
            var template = _dataSource.Get<ItemTemplate>(item.TemplateId);
            var name = template != null
                ? _localeDataSource.Get("en-gb", template.NameLocaleId)
                : $"Item #{item.TemplateId}";
            itemNames.Add(name);
        }

        return $"Loot collected: {string.Join(", ", itemNames)}";
    }

    private void DrawResultsOverlay(SpriteBatch sb)
    {
        var isVictory = _turnManager.WinningTeam == Team.Player;

        // Full-screen dark overlay
        _entityRenderer.DrawOverlay(sb);

        // Title
        var title = isVictory ? "VICTORY!" : "DEFEATED";
        var titleColor = isVictory ? Color.LightGreen : Color.OrangeRed;

        TextHelper.DrawStringWithSpacing(sb, _font, title,
            new Vector2(400, 140), titleColor, centered: true);

        if (isVictory && _lootItems.Count > 0)
        {
            // Subtitle
            TextHelper.DrawStringWithSpacing(sb, _font, "LOOT COLLECTED:",
                new Vector2(400, 190), new Color(200, 200, 150), centered: true);

            var lootY = 220;
            foreach (var item in _lootItems)
            {
                var template = _dataSource.Get<ItemTemplate>(item.TemplateId);
                var name = template != null
                    ? _localeDataSource.Get("en-gb", template.NameLocaleId)
                    : $"Item #{item.TemplateId}";
                TextHelper.DrawStringWithSpacing(sb, _font, $"- {name}",
                    new Vector2(400, lootY), Color.White, centered: true);
                lootY += 22;
            }
        }
        else if (isVictory)
        {
            TextHelper.DrawStringWithSpacing(sb, _font, "No items were dropped.",
                new Vector2(400, 200), new Color(140, 140, 150), centered: true);
        }
        else
        {
            TextHelper.DrawStringWithSpacing(sb, _font, "Your party has fallen...",
                new Vector2(400, 200), new Color(180, 120, 120), centered: true);
        }

        // Continue prompt
        TextHelper.DrawStringWithSpacing(sb, _font, "Press Enter to continue",
            new Vector2(400, 480), new Color(180, 180, 200), centered: true);
    }

    private void LayoutEntities()
    {
        foreach (var entity in Enemies)
            entity.Position = new Vector2(30 + entity.SlotInRow * 90, 30 + entity.Row * 100);

        foreach (var entity in Party)
            entity.Position = new Vector2(600 + entity.SlotInRow * 100, 30 + entity.Row * 100);
    }
}
