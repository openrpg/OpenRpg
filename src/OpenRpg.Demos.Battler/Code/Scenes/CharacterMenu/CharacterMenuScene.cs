using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Core.Effects;
using OpenRpg.Data;
using OpenRpg.Entities.Extensions;
using OpenRpg.Demos.Battler.Code.Scenes.Battle;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.CharacterMenu;

public class CharacterMenuScene : IScene
{
    private enum Screen { PartySelect, CharacterDetail, EquipmentSelect, Inventory }

    private Screen _currentScreen = Screen.PartySelect;
    private int _selectedIndex;

    // State for equipment browsing
    private int _selectedCharacter;
    private int _selectedSlotIndex; // index into SlotOrder

    private readonly IPersistentGameState _gameState;
    private readonly ISceneManager _sceneManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IGameServices _gameServices;
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly IEquipmentSlotValidator _slotValidator;
    private readonly ICharacterPopulator _characterPopulator;

    private bool _loaded;
    private SpriteFont _font;
    private Texture2D _pixel;
    private KeyboardState _previousKeyboard;

    // For EquipmentSelect: the items available for the current slot
    private int _browsingSlotType;
    private List<(ItemData Data, ItemTemplate Template)> _candidateItems = [];

    // Equipment slot definitions
    private static readonly int[] SlotOrder =
    [
        FantasyEquipmentSlotTypes.MainHandSlot,
        FantasyEquipmentSlotTypes.OffHandSlot,
        FantasyEquipmentSlotTypes.HeadSlot,
        FantasyEquipmentSlotTypes.UpperBodySlot,
        FantasyEquipmentSlotTypes.LowerBodySlot,
        FantasyEquipmentSlotTypes.FootSlot,
        FantasyEquipmentSlotTypes.NeckSlot,
        FantasyEquipmentSlotTypes.BackSlot,
        FantasyEquipmentSlotTypes.WristSlot,
        FantasyEquipmentSlotTypes.Ring1Slot,
        FantasyEquipmentSlotTypes.Ring2Slot,
    ];

    private static readonly Dictionary<int, string> SlotNames = new()
    {
        { FantasyEquipmentSlotTypes.MainHandSlot, "Main Hand" },
        { FantasyEquipmentSlotTypes.OffHandSlot, "Off Hand" },
        { FantasyEquipmentSlotTypes.HeadSlot, "Head" },
        { FantasyEquipmentSlotTypes.UpperBodySlot, "Body" },
        { FantasyEquipmentSlotTypes.LowerBodySlot, "Legs" },
        { FantasyEquipmentSlotTypes.FootSlot, "Feet" },
        { FantasyEquipmentSlotTypes.NeckSlot, "Neck" },
        { FantasyEquipmentSlotTypes.BackSlot, "Back" },
        { FantasyEquipmentSlotTypes.WristSlot, "Wrists" },
        { FantasyEquipmentSlotTypes.Ring1Slot, "Ring 1" },
        { FantasyEquipmentSlotTypes.Ring2Slot, "Ring 2" },
    };

    public CharacterMenuScene(
        IPersistentGameState gameState,
        ISceneManager sceneManager,
        IServiceProvider serviceProvider,
        IGameServices gameServices,
        IDataSource dataSource,
        ILocaleDataSource localeDataSource,
        IEquipmentSlotValidator slotValidator,
        ICharacterPopulator characterPopulator)
    {
        _gameState = gameState;
        _sceneManager = sceneManager;
        _serviceProvider = serviceProvider;
        _gameServices = gameServices;
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _slotValidator = slotValidator;
        _characterPopulator = characterPopulator;
    }

    public Task LoadAsync()
    {
        try
        {
            _loaded = false;
            _gameState.InitializeParty();

            var content = _gameServices.GetContentManager;
            _font = content.Load<SpriteFont>("Fonts/KenneyPixel");
            _pixel = new Texture2D(_gameServices.GetSpriteBatch.GraphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);

            // Load sprites for display
            foreach (var e in _gameState.Party)
            {
                try
                {
                    var path = $"Sprites/Players/{e.AssetCode}";
                    e.Sprite = content.Load<Texture2D>(path);
                }
                catch
                {
                    Console.WriteLine($"[CharacterMenu] No sprite for {e.AssetCode}");
                }
            }

            ResetToPartySelect();
            _loaded = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CharacterMenu] FAILED TO LOAD: {ex.Message}");
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

        if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Up))
        {
            _selectedIndex = Math.Max(0, _selectedIndex - 1);
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Down))
        {
            _selectedIndex = Math.Min(GetItemCount() - 1, _selectedIndex + 1);
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Enter) ||
                 InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Space))
        {
            ConfirmSelection();
        }
        else if (InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Escape) ||
                 InputHelper.IsKeyJustPressed(currentKeyboard, _previousKeyboard, Keys.Back))
        {
            GoBack();
        }

        _previousKeyboard = currentKeyboard;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // World layer - nothing for character menu
    }

    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;

        switch (_currentScreen)
        {
            case Screen.PartySelect: DrawPartySelect(spriteBatch); break;
            case Screen.CharacterDetail: DrawCharacterDetail(spriteBatch); break;
            case Screen.EquipmentSelect: DrawEquipmentSelect(spriteBatch); break;
            case Screen.Inventory: DrawInventory(spriteBatch); break;
        }

        // Help text at bottom
        var helpColor = new Color(140, 140, 150);
        TextHelper.DrawStringWithSpacing(spriteBatch, _font,
            "Up/Down Navigate  |  Enter Select  |  Esc Back",
            new Vector2(400, 575), helpColor, centered: true);
    }

    // ========================================================================
    // Item Counts
    // ========================================================================

    private int GetItemCount()
    {
        return _currentScreen switch
        {
            Screen.PartySelect => _gameState.Party.Count + 2, // party members + Inventory + Proceed to Battle
            Screen.CharacterDetail => SlotOrder.Length + 1, // equipment slots + Back
            Screen.EquipmentSelect => GetEquipmentItemCount(),
            Screen.Inventory => _gameState.SharedInventory.Count + 1, // items + Back
            _ => 0
        };
    }

    private int GetEquipmentItemCount()
    {
        var slots = _gameState.Party[_selectedCharacter].Entity.Variables.Equipment?.Slots;
        var hasCurrent = slots?.Get(_browsingSlotType) != null;
        return (hasCurrent ? 1 : 0) + _candidateItems.Count + 1; // [Unequip] + items + Back
    }

    // ========================================================================
    // Party Select
    // ========================================================================

    private void ResetToPartySelect()
    {
        _currentScreen = Screen.PartySelect;
        _selectedIndex = 0;
        _selectedCharacter = 0;
        _selectedSlotIndex = 0;
    }

    private void DrawPartySelect(SpriteBatch sb)
    {
        var panelX = 20;
        var panelW = 760;
        var titleY = 8;

        TextHelper.DrawStringWithSpacing(sb, _font, "CHARACTER MENU",
            new Vector2(400, titleY), Color.White, centered: true);

        var startY = 40;
        var itemH = 58;

        DrawRect(sb, panelX, startY, panelW, _gameState.Party.Count * itemH + 60, new Color(10, 10, 25) * 0.85f);

        for (var i = 0; i < _gameState.Party.Count; i++)
        {
            var entity = _gameState.Party[i];
            var y = startY + 6 + i * itemH;
            var isSelected = i == _selectedIndex;

            if (isSelected)
                DrawRect(sb, panelX + 4, y - 2, panelW - 8, itemH - 4, new Color(60, 60, 90));

            // Sprite
            if (entity.Sprite != null)
            {
                var destRect = new Rectangle(panelX + 14, y + 4, 40, 40);
                sb.Draw(entity.Sprite, destRect, null, Color.White, 0, Vector2.Zero,
                    SpriteEffects.FlipHorizontally, 0);
            }

            var textX = panelX + 64;
            TextHelper.DrawStringWithSpacing(sb, _font, entity.Name,
                new Vector2(textX, y + 2), Palette.PartyName);

            // Class info
            var classTemplate = _dataSource.Get<ClassTemplate>(entity.Entity.Variables.Class.TemplateId);
            var className = classTemplate != null
                ? _localeDataSource.Get("en-gb", classTemplate.NameLocaleId)
                : "Unknown";
            TextHelper.DrawStringWithSpacing(sb, _font, className,
                new Vector2(textX, y + 22), new Color(160, 160, 170));

            if (!entity.IsAlive)
            {
                TextHelper.DrawStringWithSpacing(sb, _font, "DEFEATED",
                    new Vector2(panelW - 80, y + 14), Palette.HpRed);
                continue;
            }

            // HP
            var hpRatio = (float)entity.Hp / entity.MaxHp;
            var hpColor = Palette.RatioToColor(hpRatio);
            var barW = 130f;
            var barH = 8f;
            var barX = panelW - 190;
            var barY = y + 8;

            DrawRect(sb, (int)barX, (int)barY, (int)barW, (int)barH, Palette.HpBarBg);
            DrawRect(sb, (int)barX, (int)barY, (int)(barW * hpRatio), (int)barH, hpColor);
            TextHelper.DrawStringWithSpacing(sb, _font, $"HP {entity.Hp}/{entity.MaxHp}",
                new Vector2(barX, barY + barH + 1), hpColor);

            // MP
            var mpRatio = entity.MaxMana > 0 ? (float)entity.Mana / entity.MaxMana : 0;
            var mpBarY = barY + barH + 12;
            DrawRect(sb, (int)barX, (int)mpBarY, (int)barW, (int)barH, Palette.HpBarBg);
            DrawRect(sb, (int)barX, (int)mpBarY, (int)(barW * mpRatio), (int)barH, Palette.MpText);
            TextHelper.DrawStringWithSpacing(sb, _font, $"MP {entity.Mana}/{entity.MaxMana}",
                new Vector2(barX, mpBarY + barH + 1), Palette.MpText);
        }

        // Inventory button
        var invY = startY + _gameState.Party.Count * itemH + 10;
        var isInvSelected = _selectedIndex == _gameState.Party.Count;

        if (isInvSelected)
            DrawRect(sb, panelX + 180, invY - 4, panelW - 360, 28, new Color(40, 50, 70));
        else
            DrawRect(sb, panelX + 180, invY - 4, panelW - 360, 28, new Color(20, 25, 35));

        var invCount = _gameState.SharedInventory.Count;
        TextHelper.DrawStringWithSpacing(sb, _font, $">>> INVENTORY ({invCount}) <<<",
            new Vector2(400, invY + 4),
            isInvSelected ? Color.LightBlue : new Color(120, 160, 200), centered: true);

        // Proceed to Battle button
        var proceedY = invY + 36;
        var isProceedSelected = _selectedIndex == _gameState.Party.Count + 1;

        if (isProceedSelected)
            DrawRect(sb, panelX + 180, proceedY - 4, panelW - 360, 34, new Color(40, 80, 40));
        else
            DrawRect(sb, panelX + 180, proceedY - 4, panelW - 360, 34, new Color(20, 40, 20));

        TextHelper.DrawStringWithSpacing(sb, _font, ">>> PROCEED TO BATTLE <<<",
            new Vector2(400, proceedY + 4),
            isProceedSelected ? Color.Lime : new Color(100, 200, 100), centered: true);
    }

    // ========================================================================
    // Character Detail
    // ========================================================================

    private void DrawCharacterDetail(SpriteBatch sb)
    {
        var entity = _gameState.Party[_selectedCharacter];
        if (entity == null) return;

        TextHelper.DrawStringWithSpacing(sb, _font, $"CHARACTER: {entity.Name.ToUpper()}",
            new Vector2(400, 8), Color.White, centered: true);

        // Background panel
        var panelX = 20;
        var panelY = 36;
        var panelW = 760;
        var panelH = 420;

        DrawRect(sb, panelX, panelY, panelW, panelH, new Color(10, 10, 25) * 0.85f);

        // Left: sprite + basic info
        var lx = panelX + 20;

        if (entity.Sprite != null)
        {
            var destRect = new Rectangle(lx, panelY + 10, 64, 48);
            sb.Draw(entity.Sprite, destRect, null, Color.White, 0, Vector2.Zero,
                SpriteEffects.FlipHorizontally, 0);
        }

        var classTemplate = _dataSource.Get<ClassTemplate>(entity.Entity.Variables.Class.TemplateId);
        var className = classTemplate != null
            ? _localeDataSource.Get("en-gb", classTemplate.NameLocaleId)
            : "Unknown";

        TextHelper.DrawStringWithSpacing(sb, _font, entity.Name,
            new Vector2(lx, panelY + 64), Palette.PartyName);
        TextHelper.DrawStringWithSpacing(sb, _font, $"{className}  Lv.1",
            new Vector2(lx, panelY + 84), new Color(160, 160, 170));

        // Stats columns
        var col1X = lx + 120;
        var col2X = lx + 340;
        var statY = panelY + 10;

        DrawStat(sb, col1X, statY, "HP", $"{entity.Hp}/{entity.MaxHp}", Palette.HpGreen);
        DrawStat(sb, col2X, statY, "MP", $"{entity.Mana}/{entity.MaxMana}", Palette.MpText);
        DrawStat(sb, col1X, statY + 20, "Attack", entity.AttackDamage.ToString(), Color.White);
        DrawStat(sb, col2X, statY + 20, "Defense", ((int)entity.Entity.Stats.Defense).ToString(), Color.White);
        DrawStat(sb, col1X, statY + 40, "Speed", entity.Initiative.ToString(), Color.White);
        DrawStat(sb, col2X, statY + 40, "Range", ((int)entity.Entity.Stats.AttackRange).ToString(), Color.White);
        DrawStat(sb, col1X, statY + 60, "Crit %", $"{entity.Entity.Stats.CriticalDamageChance:F1}%", Color.White);
        DrawStat(sb, col2X, statY + 60, "Crit Mult", $"{entity.Entity.Stats.CriticalDamageMultiplier:F1}x", Color.White);

        // Attributes
        var attrY = panelY + 100;
        TextHelper.DrawStringWithSpacing(sb, _font, "-- Attributes --",
            new Vector2(lx, attrY), new Color(200, 200, 180));
        DrawStat(sb, lx, attrY + 20, "STR", ((int)entity.Entity.Stats.Strength).ToString(), new Color(240, 180, 80));
        DrawStat(sb, lx + 140, attrY + 20, "DEX", ((int)entity.Entity.Stats.Dexterity).ToString(), new Color(80, 200, 80));
        DrawStat(sb, lx + 280, attrY + 20, "CON", ((int)entity.Entity.Stats.Constitution).ToString(), new Color(180, 120, 80));
        DrawStat(sb, lx, attrY + 40, "INT", ((int)entity.Entity.Stats.Intelligence).ToString(), new Color(80, 140, 240));
        DrawStat(sb, lx + 140, attrY + 40, "WIS", ((int)entity.Entity.Stats.Wisdom).ToString(), new Color(140, 180, 220));
        DrawStat(sb, lx + 280, attrY + 40, "CHA", ((int)entity.Entity.Stats.Charisma).ToString(), new Color(220, 140, 220));

        // Equipment section (interactive items)
        var equipY = panelY + 192;
        TextHelper.DrawStringWithSpacing(sb, _font, "-- Equipment (select to change) --",
            new Vector2(lx, equipY), new Color(200, 200, 150));

        var slots = entity.Entity.Variables.Equipment?.Slots;
        for (var i = 0; i < SlotOrder.Length; i++)
        {
            var slotType = SlotOrder[i];
            var slotName = SlotNames.GetValueOrDefault(slotType, $"Slot {slotType}");
            var itemData = slots?.Get(slotType);
            var itemName = "Empty";
            if (itemData != null)
            {
                var itemTemplate = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
                itemName = itemTemplate != null
                    ? _localeDataSource.Get("en-gb", itemTemplate.NameLocaleId)
                    : $"Item #{itemData.TemplateId}";
            }

            var isSelected = i == _selectedIndex;
            var slotColor = isSelected ? Color.Lime : new Color(180, 180, 190);
            if (itemData == null && !isSelected)
                slotColor = new Color(100, 100, 110);

            // Two columns for slots
            var col = i < 6 ? lx : lx + 280;
            var row = i < 6 ? i : i - 6;
            var sy = equipY + 18 + row * 19;

            TextHelper.DrawStringWithSpacing(sb, _font,
                $"{slotName}: {itemName}", new Vector2(col, sy), slotColor);
        }

        // Back
        var backY = panelY + panelH - 24;
        var isBack = _selectedIndex == SlotOrder.Length;
        TextHelper.DrawStringWithSpacing(sb, _font, "[ Back ]",
            new Vector2(lx, backY), isBack ? Color.White : Palette.MenuBackColor);
    }

    // ========================================================================
    // Equipment Select
    // ========================================================================

    private void RefreshCandidates()
    {
        _candidateItems = [];
        foreach (var itemData in _gameState.SharedInventory)
        {
            var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
            if (template == null) continue;
            if (_slotValidator.CanEquipItemType(_browsingSlotType, template.ItemType))
                _candidateItems.Add((itemData, template));
        }
    }

    private void DrawEquipmentSelect(SpriteBatch sb)
    {
        var entity = _gameState.Party[_selectedCharacter];
        var slotName = SlotNames.GetValueOrDefault(_browsingSlotType, "Unknown Slot");

        TextHelper.DrawStringWithSpacing(sb, _font,
            $"EQUIP: {entity.Name.ToUpper()} - {slotName}",
            new Vector2(400, 8), Color.White, centered: true);

        var panelX = 80;
        var panelY = 40;
        var panelW = 640;
        var panelH = 440;

        DrawRect(sb, panelX, panelY, panelW, panelH, new Color(10, 10, 25) * 0.92f);

        TextHelper.DrawStringWithSpacing(sb, _font,
            "Select an item to equip:",
            new Vector2(panelX + 20, panelY + 12), new Color(180, 180, 190));

        var slots = entity.Entity.Variables.Equipment?.Slots;
        var currentItem = slots?.Get(_browsingSlotType);
        var hasCurrent = currentItem != null;

        var y = panelY + 40;
        var idx = 0;

        // Unequip option if slot is occupied
        if (hasCurrent)
        {
            var isSel = _selectedIndex == idx;
            var unequipColor = isSel ? new Color(255, 200, 100) : new Color(180, 160, 100);
            TextHelper.DrawStringWithSpacing(sb, _font,
                $"[Unequip] ({GetItemName(currentItem)})",
                new Vector2(panelX + 20, y), unequipColor);
            y += 26;
            idx++;
        }

        // Available items from inventory
        if (_candidateItems.Count == 0)
        {
            TextHelper.DrawStringWithSpacing(sb, _font,
                "(No compatible items in inventory)",
                new Vector2(panelX + 20, y), new Color(100, 100, 110));
            y += 26;
            idx++;
        }
        else
        {
            foreach (var (itemData, template) in _candidateItems)
            {
                var isSel = _selectedIndex == idx;
                var itemName = _localeDataSource.Get("en-gb", template.NameLocaleId);

                // Check for stat bonuses
                var bonusText = GetItemBonusText(template);

                var textColor = isSel ? Color.Lime : new Color(180, 180, 190);
                TextHelper.DrawStringWithSpacing(sb, _font,
                    $"{itemName}{bonusText}",
                    new Vector2(panelX + 20, y), textColor);
                y += 26;
                idx++;
            }
        }

        // Back
        var backY = panelY + panelH - 28;
        var isBack = _selectedIndex == idx;
        TextHelper.DrawStringWithSpacing(sb, _font, "[ Back ]",
            new Vector2(panelX + 20, backY),
            isBack ? Color.White : Palette.MenuBackColor);
    }

    private string GetItemBonusText(ItemTemplate template)
    {
        var parts = new List<string>();
        if (template.Variables.Effects != null)
        {
            foreach (var effect in template.Variables.Effects)
            {
                if (effect is not OpenRpg.Core.Effects.StaticEffect se) continue;
                if (se.EffectType == 1) // DamageBonusAmount
                    parts.Add($" ATK+{se.Potency}");
                else if (se.EffectType == 21) // DefenseBonusAmount
                    parts.Add($" DEF+{se.Potency}");
                else if (se.EffectType == 60) // HealthBonusAmount
                    parts.Add($" HP+{se.Potency}");
                else if (se.EffectType == 44) // MovementSpeedBonusAmount
                    parts.Add($" SPD+{se.Potency}");
                else if (se.EffectType == 263) // UnarmedDamageBonusAmount
                    parts.Add($" ATK+{se.Potency}");
            }
        }
        return parts.Count > 0 ? string.Join("", parts) : "";
    }

    // ========================================================================
    // Inventory Screen
    // ========================================================================

    private void DrawInventory(SpriteBatch sb)
    {
        TextHelper.DrawStringWithSpacing(sb, _font, "SHARED INVENTORY",
            new Vector2(400, 8), Color.White, centered: true);

        var panelX = 40;
        var panelY = 36;
        var panelW = 720;
        var panelH = 440;

        DrawRect(sb, panelX, panelY, panelW, panelH, new Color(10, 10, 25) * 0.92f);

        var items = _gameState.SharedInventory;
        if (items.Count == 0)
        {
            TextHelper.DrawStringWithSpacing(sb, _font, "(No items in inventory)",
                new Vector2(panelX + 20, panelY + 20), new Color(100, 100, 110));
        }
        else
        {
            var y = panelY + 12;
            for (var i = 0; i < items.Count; i++)
            {
                var itemData = items[i];
                var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
                var name = template != null
                    ? _localeDataSource.Get("en-gb", template.NameLocaleId)
                    : $"Item #{itemData.TemplateId}";

                var isSelected = i == _selectedIndex;
                if (isSelected)
                    DrawRect(sb, panelX + 4, y - 2, panelW - 8, 22, new Color(60, 60, 90));

                var baseColor = isSelected ? Color.White : new Color(180, 180, 190);

                // Determine item type label
                var typeLabel = template != null ? GetItemTypeLabel(template.ItemType) : "";

                TextHelper.DrawStringWithSpacing(sb, _font, name,
                    new Vector2(panelX + 16, y), baseColor);

                if (!string.IsNullOrEmpty(typeLabel))
                {
                    TextHelper.DrawStringWithSpacing(sb, _font, typeLabel,
                        new Vector2(panelX + panelW - 140, y), new Color(140, 140, 150));
                }

                y += 24;
            }

            // Show selected item's stats/bonuses at bottom
            if (_selectedIndex < items.Count)
            {
                var template = _dataSource.Get<ItemTemplate>(items[_selectedIndex].TemplateId);
                if (template != null)
                {
                    var bonusText = GetItemBonusText(template);
                    var desc = _localeDataSource.Get("en-gb", template.DescriptionLocaleId);
                    if (!string.IsNullOrEmpty(desc))
                    {
                        TextHelper.DrawStringWithSpacing(sb, _font, desc,
                            new Vector2(panelX + 16, panelY + panelH - 48), new Color(160, 160, 170));
                    }

                    if (!string.IsNullOrEmpty(bonusText))
                    {
                        TextHelper.DrawStringWithSpacing(sb, _font, bonusText,
                            new Vector2(panelX + 16, panelY + panelH - 28), new Color(200, 200, 150));
                    }
                }
            }
        }

        // Back
        var backY = panelY + panelH - 8;
        var isBack = _selectedIndex == items.Count;
        TextHelper.DrawStringWithSpacing(sb, _font, "[ Back ]",
            new Vector2(panelX + 16, backY),
            isBack ? Color.White : Palette.MenuBackColor);
    }

    private static string GetItemTypeLabel(int itemType)
    {
        return itemType switch
        {
            2 => "Weapon",
            30 => "Head",
            31 => "Body",
            32 => "Legs",
            33 => "Back",
            34 => "Feet",
            35 => "Wrist",
            36 => "Neck",
            37 => "Ring",
            50 => "Off Hand",
            60 => "Consumable",
            _ => ""
        };
    }

    // ========================================================================
    // Input Handlers
    // ========================================================================

    private void ConfirmSelection()
    {
        switch (_currentScreen)
        {
            case Screen.PartySelect:
                HandlePartySelectConfirm();
                break;
            case Screen.CharacterDetail:
                HandleCharacterDetailConfirm();
                break;
            case Screen.EquipmentSelect:
                HandleEquipmentConfirm();
                break;
            case Screen.Inventory:
                HandleInventoryConfirm();
                break;
        }
    }

    private void HandlePartySelectConfirm()
    {
        if (_selectedIndex < _gameState.Party.Count)
        {
            _selectedCharacter = _selectedIndex;
            _selectedIndex = 0;
            _currentScreen = Screen.CharacterDetail;
        }
        else if (_selectedIndex == _gameState.Party.Count)
        {
            // Inventory button
            _selectedIndex = 0;
            _currentScreen = Screen.Inventory;
        }
        else
        {
            ProceedToBattle();
        }
    }

    private void HandleCharacterDetailConfirm()
    {
        if (_selectedIndex < SlotOrder.Length)
        {
            // Start equipment selection for this slot
            _selectedSlotIndex = _selectedIndex;
            _browsingSlotType = SlotOrder[_selectedSlotIndex];
            _selectedIndex = 0;

            RefreshCandidates();
            _currentScreen = Screen.EquipmentSelect;
        }
        else
        {
            // Back
            ResetToPartySelect();
        }
    }

    private void HandleEquipmentConfirm()
    {
        var entity = _gameState.Party[_selectedCharacter];
        var slots = entity.Entity.Variables.Equipment?.Slots;
        if (slots == null) return;

        var currentItem = slots.Get(_browsingSlotType);
        var hasCurrent = currentItem != null;
        var idx = 0;

        // Unequip?
        if (hasCurrent)
        {
            if (_selectedIndex == idx)
            {
                _gameState.SharedInventory.Add(currentItem);
                slots[_browsingSlotType] = null;
                _characterPopulator.Populate(entity.Entity, refreshState: false);
                _selectedIndex = 0;
                RefreshCandidates();
                return;
            }
            idx++;
        }

        // Inventory items?
        if (_selectedIndex < idx + _candidateItems.Count)
        {
            var itemIndex = _selectedIndex - idx;
            var (itemData, _) = _candidateItems[itemIndex];

            _gameState.SharedInventory.Remove(itemData);
            if (currentItem != null)
                _gameState.SharedInventory.Add(currentItem);

            slots[_browsingSlotType] = itemData;
            _characterPopulator.Populate(entity.Entity, refreshState: false);

            // Return to character detail after equipping
            _selectedIndex = _selectedSlotIndex;
            _currentScreen = Screen.CharacterDetail;
            return;
        }

        // Back to character detail
        _selectedIndex = _selectedSlotIndex;
        _currentScreen = Screen.CharacterDetail;
    }

    private void HandleInventoryConfirm()
    {
        var itemCount = _gameState.SharedInventory.Count;
        if (_selectedIndex == itemCount)
        {
            // Back to party select
            _selectedIndex = _gameState.Party.Count;
            _currentScreen = Screen.PartySelect;
        }
        // Selecting an individual item is just viewing info (no action needed)
    }

    private void GoBack()
    {
        switch (_currentScreen)
        {
            case Screen.PartySelect:
                // At top level - Escape exits the game
                _sceneManager.RequestExit?.Invoke();
                break;
            case Screen.CharacterDetail:
                ResetToPartySelect();
                break;
            case Screen.EquipmentSelect:
                _selectedIndex = _selectedSlotIndex;
                _currentScreen = Screen.CharacterDetail;
                break;
            case Screen.Inventory:
                _selectedIndex = _gameState.Party.Count;
                _currentScreen = Screen.PartySelect;
                break;
        }
    }

    private void ProceedToBattle()
    {
        var battleScene = _serviceProvider.GetRequiredService<BattleScene>();
        _ = _sceneManager.SetScene(battleScene);
    }

    // ========================================================================
    // Drawing Helpers
    // ========================================================================

    private void DrawRect(SpriteBatch sb, int x, int y, int w, int h, Color color)
    {
        if (_pixel != null)
            sb.Draw(_pixel, new Rectangle(x, y, w, h), color);
    }

    private void DrawStat(SpriteBatch sb, int x, int y, string label, string value, Color valueColor)
    {
        TextHelper.DrawStringWithSpacing(sb, _font, $"{label}:",
            new Vector2(x, y), new Color(140, 140, 150));
        var labelW = TextHelper.MeasureStringWidth(_font, $"{label}:");
        TextHelper.DrawStringWithSpacing(sb, _font, value,
            new Vector2(x + labelW + 6, y), valueColor);
    }

    private string GetItemName(ItemData itemData)
    {
        var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
        return template != null
            ? _localeDataSource.Get("en-gb", template.NameLocaleId)
            : $"Item #{itemData.TemplateId}";
    }
}
