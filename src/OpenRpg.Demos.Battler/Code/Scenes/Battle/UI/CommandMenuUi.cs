using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public class CommandMenuUi
{
    public enum MenuScreen { Hidden, MainMenu, AbilitySelect, TargetSelect, ItemSelect, ItemTargetSelect }

    private MenuScreen _currentScreen = MenuScreen.Hidden;
    private int _selectedIndex;
    private BattleEntity _currentAttacker;
    private List<BattleEntity> _aliveEnemies;
    private List<BattleEntity> _aliveParty;
    private List<BattleEntity> _allParty;
    private List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> _availableAbilities;
    private AbilityTemplate _selectedAbility;
    private ILocaleDataSource _localeDataSource;
    private IDataSource _dataSource;
    private string[] _currentItems = [];

    // Item usage state
    private List<ItemData> _inventoryItems;
    private ItemData _selectedItemData;
    private ItemTemplate _selectedItemTemplate;
    private bool _targetingParty;

    private ColoredRectangleRuntime _menuBg;
    private ColoredRectangleRuntime _tooltipBg;
    private readonly List<ColoredRectangleRuntime> _itemRects = [];

    private const int MenuX = 290;
    private const int MenuW = 220;
    private const int MenuItemH = 24;
    private const int MenuPadding = 8;
    private const int TooltipH = 22;
    private const int TooltipGap = 4;

    private int _menuY;
    private string _tooltipText = "";

    public event Action<PlayerAction> OnActionConfirmed;
    public MenuScreen CurrentScreen => _currentScreen;
    public bool IsHidden => _currentScreen == MenuScreen.Hidden;

    public List<BattleEntity> PreviewTargets
    {
        get
        {
            if (_currentScreen == MenuScreen.TargetSelect)
            {
                var pool = _targetingParty ? _aliveParty : _aliveEnemies;
                if (pool != null && _selectedIndex < pool.Count)
                    return [pool[_selectedIndex]];
                return [];
            }

            if (_currentScreen == MenuScreen.AbilitySelect && _selectedIndex < _availableAbilities.Count)
            {
                var (template, _, canAfford) = _availableAbilities[_selectedIndex];
                if (!canAfford) return [];
                return TargetResolver.ResolvePreviewTargets(template, _aliveParty, _aliveEnemies);
            }

            return [];
        }
    }

    public void Show(CommandMenuContext context)
    {
        Show(context.Attacker, context.AliveEnemies, context.Abilities, context.LocaleDataSource,
            context.InventoryItems, context.AliveParty, context.DataSource, context.AllParty);
    }

    public void Show(
        BattleEntity attacker,
        List<BattleEntity> aliveEnemies,
        List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> abilities,
        ILocaleDataSource localeDataSource,
        List<ItemData> inventoryItems = null,
        List<BattleEntity> aliveParty = null,
        IDataSource dataSource = null,
        List<BattleEntity> allParty = null)
    {
        _currentAttacker = attacker;
        _aliveEnemies = aliveEnemies;
        _availableAbilities = abilities;
        _selectedAbility = null;
        _targetingParty = false;
        _localeDataSource = localeDataSource;
        _inventoryItems = inventoryItems;
        _aliveParty = aliveParty;
        _dataSource = dataSource;
        _allParty = allParty;

        SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
    }

    private void SwitchToScreen(MenuScreen screen, string[] items)
    {
        _currentScreen = screen;
        _selectedIndex = 0;
        _currentItems = items;
        BuildGumElements();
    }

    private string[] BuildMainMenuItems()
    {
        return [$"Attack", $"Ability", $"Items"];
    }

    private string[] BuildAbilityItems()
    {
        var items = new List<string>();
        foreach (var (template, manaCost, _) in _availableAbilities)
        {
            var name = _localeDataSource?.Get("en-gb", template.NameLocaleId) ?? $"Ability {template.Id}";
            items.Add($"{name}  (MP:{manaCost})");
        }
        items.Add("Back");
        return [.. items];
    }

    private string[] BuildTargetItems()
    {
        var pool = _targetingParty ? _aliveParty : _aliveEnemies;
        var items = (pool ?? []).Select(e => e.Name).ToList();
        items.Add("Back");
        return [.. items];
    }

    private string[] BuildItemItems()
    {
        var items = new List<string>();
        if (_inventoryItems != null)
        {
            foreach (var itemData in _inventoryItems)
            {
                var template = _dataSource?.Get<ItemTemplate>(itemData.TemplateId);
                var name = template != null
                    ? _localeDataSource?.Get("en-gb", template.NameLocaleId) ?? $"Item #{itemData.TemplateId}"
                    : $"Item #{itemData.TemplateId}";
                items.Add(name);
            }
        }
        items.Add("Back");
        return [.. items];
    }

    private string[] BuildItemTargetItems()
    {
        if (_selectedItemTemplate == null)
        {
            var fallback = _aliveParty?.Select(e => e.Name).ToList() ?? [];
            if (fallback.Count == 0) fallback.Add("(No valid targets)");
            fallback.Add("Back");
            return [.. fallback];
        }

        var isLifeRestore = HasLifeRestoreEffect(_selectedItemTemplate);
        var pool = isLifeRestore ? _allParty?.Where(e => !e.IsAlive).ToList() : _aliveParty;

        var items = (pool ?? []).Select(e =>
        {
            var name = e.Name;
            if (!e.IsAlive) name += " (Dead)";
            return name;
        }).ToList();

        if (items.Count == 0)
            items.Add(isLifeRestore ? "(No dead members)" : "(No valid targets)");
        items.Add("Back");
        return [.. items];
    }

    private static bool HasLifeRestoreEffect(ItemTemplate template)
    {
        return ItemEffectApplier.HasLifeRestoreEffect(template);
    }

    private void BuildGumElements()
    {
        ClearGumElements();

        var itemCount = _currentItems.Length;
        var menuH = itemCount * MenuItemH + MenuPadding * 2;
        _menuY = (310 - menuH) / 2 + 30;

        _tooltipText = GetCurrentDescription();
        var hasTooltip = !string.IsNullOrEmpty(_tooltipText);

        if (hasTooltip)
        {
            var tooltipY = _menuY - TooltipH - TooltipGap;
            _tooltipBg = new ColoredRectangleRuntime
            {
                X = MenuX,
                Y = tooltipY,
                Width = MenuW,
                Height = TooltipH
            };
            _tooltipBg.SetRectColor(Palette.MenuBg);
            _tooltipBg.AddToRoot();
        }

        _menuBg = new ColoredRectangleRuntime
        {
            X = MenuX,
            Y = _menuY,
            Width = MenuW,
            Height = menuH
        };
        _menuBg.SetRectColor(Palette.MenuBg);
        _menuBg.AddToRoot();

        for (var i = 0; i < itemCount; i++)
        {
            var rect = new ColoredRectangleRuntime
            {
                X = MenuX + 3,
                Y = _menuY + MenuPadding + i * MenuItemH,
                Width = MenuW - 6,
                Height = MenuItemH - 2
            };
            rect.SetRectColor(Palette.MenuItemBg);
            rect.AddToRoot();
            _itemRects.Add(rect);
        }

        UpdateSelectionHighlight();
    }

    private void UpdateSelectionHighlight()
    {
        for (var i = 0; i < _itemRects.Count; i++)
        {
            _itemRects[i].SetRectColor(
                i == _selectedIndex
                    ? Palette.MenuItemSelected
                    : Palette.MenuItemBg);
        }
    }

    public void Hide()
    {
        _currentScreen = MenuScreen.Hidden;
        ClearGumElements();
    }

    private void ClearGumElements()
    {
        _tooltipBg?.RemoveFromRoot();
        _tooltipBg = null;
        _menuBg?.RemoveFromRoot();
        _menuBg = null;
        foreach (var r in _itemRects)
            r.RemoveFromRoot();
        _itemRects.Clear();
    }

    private string GetCurrentDescription()
    {
        if (_currentScreen == MenuScreen.AbilitySelect && _selectedIndex < _availableAbilities.Count)
        {
            var (template, _, canAfford) = _availableAbilities[_selectedIndex];
            if (!canAfford)
                return "Not enough MP!";
            return _localeDataSource?.Get("en-gb", template.DescriptionLocaleId) ?? "";
        }

        if (_currentScreen == MenuScreen.TargetSelect && _selectedAbility != null)
        {
            var name = _localeDataSource?.Get("en-gb", _selectedAbility.NameLocaleId) ?? $"Ability {_selectedAbility.Id}";
            return $"Choose target for {name}";
        }

        if (_currentScreen == MenuScreen.TargetSelect && _selectedAbility == null)
            return "Choose target for Attack";

        if (_currentScreen == MenuScreen.ItemSelect && _selectedIndex < (_inventoryItems?.Count ?? 0))
        {
            var template = _dataSource?.Get<ItemTemplate>(_inventoryItems[_selectedIndex].TemplateId);
            if (template != null)
                return _localeDataSource?.Get("en-gb", template.DescriptionLocaleId) ?? "";
            return "";
        }

        if (_currentScreen == MenuScreen.ItemTargetSelect)
        {
            var itemName = _selectedItemTemplate != null
                ? _localeDataSource?.Get("en-gb", _selectedItemTemplate.NameLocaleId) ?? "Item"
                : "Item";
            return $"Use {itemName} on which party member?";
        }

        return "";
    }

    public void HandleInput(KeyboardState current, KeyboardState previous)
    {
        if (IsHidden) return;

        if (InputHelper.IsKeyJustPressed(current, previous, Keys.Up))
        {
            _selectedIndex = _selectedIndex > 0 ? _selectedIndex - 1 : _currentItems.Length - 1;
            UpdateSelectionHighlight();
            _tooltipText = GetCurrentDescription();
        }
        else if (InputHelper.IsKeyJustPressed(current, previous, Keys.Down))
        {
            _selectedIndex = (_selectedIndex + 1) % _currentItems.Length;
            UpdateSelectionHighlight();
            _tooltipText = GetCurrentDescription();
        }
        else if (InputHelper.IsKeyJustPressed(current, previous, Keys.Enter) ||
                 InputHelper.IsKeyJustPressed(current, previous, Keys.Space))
        {
            ConfirmSelection();
        }
        else if (InputHelper.IsKeyJustPressed(current, previous, Keys.Escape) ||
                 InputHelper.IsKeyJustPressed(current, previous, Keys.Back))
        {
            GoBack();
        }
    }

    private void ConfirmSelection()
    {
        switch (_currentScreen)
        {
            case MenuScreen.MainMenu:
                HandleMainMenuConfirm();
                break;
            case MenuScreen.AbilitySelect:
                HandleAbilityConfirm();
                break;
            case MenuScreen.TargetSelect:
                HandleTargetConfirm();
                break;
            case MenuScreen.ItemSelect:
                HandleItemConfirm();
                break;
            case MenuScreen.ItemTargetSelect:
                HandleItemTargetConfirm();
                break;
        }
    }

    private void HandleMainMenuConfirm()
    {
        switch (_selectedIndex)
        {
            case 0:
                _selectedAbility = null;
                _targetingParty = false;
                SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
                break;
            case 1:
                SwitchToScreen(MenuScreen.AbilitySelect, BuildAbilityItems());
                break;
            case 2:
                // Items - show inventory
                if (_inventoryItems == null || _inventoryItems.Count == 0)
                    return; // No items to use
                _selectedIndex = 0;
                SwitchToScreen(MenuScreen.ItemSelect, BuildItemItems());
                break;
        }
    }

    private void HandleAbilityConfirm()
    {
        var abilityCount = _availableAbilities.Count;
        if (_selectedIndex == abilityCount)
        {
            SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
            return;
        }

        var (template, _, canAfford) = _availableAbilities[_selectedIndex];
        if (!canAfford) return;

        _selectedAbility = template;

        var isHealing = TargetResolver.IsHealing(template);
        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);

        if (targetType == CombatTargetTypes.MultipleTarget)
        {
            var pool = isHealing ? _aliveParty : _aliveEnemies;
            if (pool == null || pool.Count == 0) return;
            var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
            var actualCount = Math.Min(targetCount, pool.Count);
            var targets = pool.Take(actualCount).ToList();
            FireAction(new PlayerAction
            {
                Type = ActionType.Ability,
                Ability = template,
                Targets = targets
            });
        }
        else
        {
            _targetingParty = isHealing;
            SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
        }
    }

    private void HandleTargetConfirm()
    {
        var pool = _targetingParty ? _aliveParty : _aliveEnemies;
        var poolCount = pool?.Count ?? 0;

        if (_selectedIndex == poolCount)
        {
            var backScreen = _selectedAbility != null ? MenuScreen.AbilitySelect : MenuScreen.MainMenu;
            var backItems = _selectedAbility != null ? BuildAbilityItems() : BuildMainMenuItems();
            _targetingParty = false;
            SwitchToScreen(backScreen, backItems);
            return;
        }

        var target = pool![_selectedIndex];

        FireAction(_selectedAbility != null
            ? new PlayerAction { Type = ActionType.Ability, Ability = _selectedAbility, Targets = [target] }
            : new PlayerAction { Type = ActionType.BasicAttack, Targets = [target] });
    }

    private void HandleItemConfirm()
    {
        var itemCount = _inventoryItems?.Count ?? 0;
        if (_selectedIndex == itemCount)
        {
            SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
            return;
        }

        if (_inventoryItems == null || _selectedIndex >= _inventoryItems.Count) return;

        _selectedItemData = _inventoryItems[_selectedIndex];
        _selectedItemTemplate = _dataSource?.Get<ItemTemplate>(_selectedItemData.TemplateId);

        if (_selectedItemTemplate == null) return;

        _selectedIndex = 0;
        SwitchToScreen(MenuScreen.ItemTargetSelect, BuildItemTargetItems());
    }

    private void HandleItemTargetConfirm()
    {
        var isLifeRestore = _selectedItemTemplate != null && HasLifeRestoreEffect(_selectedItemTemplate);
        var pool = isLifeRestore ? _allParty?.Where(e => !e.IsAlive).ToList() : _aliveParty;
        var targetCount = pool?.Count ?? 0;

        if (pool == null || _selectedIndex >= targetCount)
        {
            // Back to item select
            _selectedIndex = 0;
            SwitchToScreen(MenuScreen.ItemSelect, BuildItemItems());
            return;
        }

        if (_selectedItemData == null) return;

        var target = pool[_selectedIndex];
        var itemCopy = new ItemData { TemplateId = _selectedItemData.TemplateId };

        FireAction(new PlayerAction
        {
            Type = ActionType.UseItem,
            Targets = [target],
            UsedItem = itemCopy
        });
    }

    private void GoBack()
    {
        switch (_currentScreen)
        {
            case MenuScreen.MainMenu:
                if (_aliveEnemies.Count > 0)
                    FireAction(new PlayerAction { Type = ActionType.BasicAttack, Targets = [_aliveEnemies[0]] });
                break;
            case MenuScreen.AbilitySelect:
                SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
                break;
            case MenuScreen.TargetSelect:
                _targetingParty = false;
                if (_selectedAbility != null)
                    SwitchToScreen(MenuScreen.AbilitySelect, BuildAbilityItems());
                else
                    SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
                break;
            case MenuScreen.ItemSelect:
                SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
                break;
            case MenuScreen.ItemTargetSelect:
                _selectedIndex = 0;
                SwitchToScreen(MenuScreen.ItemSelect, BuildItemItems());
                break;
        }
    }

    private void FireAction(PlayerAction action)
    {
        OnActionConfirmed?.Invoke(action);
        Hide();
    }

    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        if (IsHidden) return;

        if (!string.IsNullOrEmpty(_tooltipText))
        {
            var tooltipY = _menuY - TooltipH - TooltipGap;
            TextHelper.DrawStringWithSpacing(sb, font, _tooltipText,
                new Vector2(MenuX + MenuPadding, tooltipY + 3), Palette.MenuTooltip);
        }

        var x = MenuX + MenuPadding;
        var baseY = _menuY + MenuPadding;

        for (var i = 0; i < _currentItems.Length; i++)
        {
            var y = baseY + i * MenuItemH + 2;
            var isSelected = i == _selectedIndex;

            Color color;
            if (_currentScreen == MenuScreen.AbilitySelect && i < _availableAbilities.Count)
            {
                if (!_availableAbilities[i].CanAfford)
                    color = Palette.MenuCannotAfford;
                else if (isSelected)
                    color = Color.White;
                else
                    color = Palette.MenuItemNormal;
            }
            else if (IsBackItem(i))
            {
                color = Palette.MenuBackColor;
            }
            else if (isSelected)
            {
                color = Color.White;
            }
            else
            {
                color = Palette.MenuItemNormal;
            }

            TextHelper.DrawStringWithSpacing(sb, font, _currentItems[i], new Vector2(x, y), color);
        }
    }

    private bool IsBackItem(int index)
    {
        if (_currentScreen == MenuScreen.AbilitySelect && index == _availableAbilities.Count) return true;
        if (_currentScreen == MenuScreen.TargetSelect)
        {
            var poolCount = _targetingParty ? (_aliveParty?.Count ?? 0) : (_aliveEnemies?.Count ?? 0);
            if (index == poolCount) return true;
        }
        if (_currentScreen == MenuScreen.ItemSelect && index == (_inventoryItems?.Count ?? 0)) return true;
        if (_currentScreen == MenuScreen.ItemTargetSelect)
        {
            var isLifeRestore = _selectedItemTemplate != null && HasLifeRestoreEffect(_selectedItemTemplate);
            var pool = isLifeRestore ? _allParty?.Where(e => !e.IsAlive).ToList() : _aliveParty;
            if (index == (pool?.Count ?? 0)) return true;
        }
        return false;
    }
}
