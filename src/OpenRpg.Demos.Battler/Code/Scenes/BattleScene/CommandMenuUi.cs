using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class CommandMenuUi
{
    public enum MenuScreen { Hidden, MainMenu, AbilitySelect, TargetSelect }

    private MenuScreen _currentScreen = MenuScreen.Hidden;
    private int _selectedIndex;
    private BattleEntity _currentAttacker;
    private List<BattleEntity> _aliveEnemies;
    private List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> _availableAbilities;
    private AbilityTemplate _selectedAbility;
    private ILocaleDataSource _localeDataSource;

    private string[] _currentItems = [];

    // Gum elements
    private ColoredRectangleRuntime _menuBg;
    private ColoredRectangleRuntime _tooltipBg;
    private readonly List<ColoredRectangleRuntime> _itemRects = [];

    // Layout
    private const int MenuX = 290;
    private const int MenuW = 220;
    private const int MenuItemH = 24;
    private const int MenuPadding = 8;
    private const int TooltipH = 22;
    private const int TooltipGap = 4;

    private int _menuY; // computed per menu based on item count
    private string _tooltipText = "";

    public event Action<PlayerAction> OnActionConfirmed;
    public MenuScreen CurrentScreen => _currentScreen;
    public bool IsHidden => _currentScreen == MenuScreen.Hidden;

    /// <summary>
    /// Returns the list of enemy targets to visually highlight based on current menu state.
    /// - TargetSelect: highlights the currently selected enemy
    /// - AbilitySelect (multi-target): highlights enemies that would be hit
    /// - Otherwise: empty list
    /// </summary>
    public List<BattleEntity> PreviewTargets
    {
        get
        {
            if (_currentScreen == MenuScreen.TargetSelect)
            {
                if (_selectedIndex < _aliveEnemies.Count)
                    return [_aliveEnemies[_selectedIndex]];
                return [];
            }

            if (_currentScreen == MenuScreen.AbilitySelect && _selectedIndex < _availableAbilities.Count)
            {
                var (template, _, canAfford) = _availableAbilities[_selectedIndex];
                if (!canAfford) return [];

                var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
                if (targetType == CombatTargetTypes.MultipleTarget)
                {
                    var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
                    var actualCount = Math.Min(targetCount, _aliveEnemies.Count);
                    return _aliveEnemies.Take(actualCount).ToList();
                }
            }

            return [];
        }
    }

    public void Show(
        BattleEntity attacker,
        List<BattleEntity> aliveEnemies,
        List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> abilities,
        ILocaleDataSource localeDataSource)
    {
        _currentAttacker = attacker;
        _aliveEnemies = aliveEnemies;
        _availableAbilities = abilities;
        _selectedAbility = null;
        _localeDataSource = localeDataSource;

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
        return ["Attack", "Ability", "Items", "Flee"];
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
        var items = _aliveEnemies.Select(e => e.Name).ToList();
        items.Add("Back");
        return [.. items];
    }

    private void BuildGumElements()
    {
        ClearGumElements();

        var itemCount = _currentItems.Length;
        var menuH = itemCount * MenuItemH + MenuPadding * 2;
        _menuY = (310 - menuH) / 2 + 30; // center in battlefield area (Y=30-310)

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
            SetRectColor(_tooltipBg, new Color(10, 10, 25) * 0.92f);
            _tooltipBg.AddToRoot();
        }

        _menuBg = new ColoredRectangleRuntime
        {
            X = MenuX,
            Y = _menuY,
            Width = MenuW,
            Height = menuH
        };
        SetRectColor(_menuBg, new Color(10, 10, 25) * 0.92f);
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
            SetRectColor(rect, new Color(20, 20, 40) * 0.5f);
            rect.AddToRoot();
            _itemRects.Add(rect);
        }

        UpdateSelectionHighlight();
    }

    private void UpdateSelectionHighlight()
    {
        for (var i = 0; i < _itemRects.Count; i++)
        {
            SetRectColor(_itemRects[i],
                i == _selectedIndex
                    ? new Color(60, 60, 90)
                    : new Color(20, 20, 40) * 0.5f);
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

        return "";
    }

    public void HandleInput(KeyboardState current, KeyboardState previous)
    {
        if (IsHidden) return;

        if (IsKeyJustPressed(current, previous, Keys.Up))
        {
            _selectedIndex = _selectedIndex > 0 ? _selectedIndex - 1 : _currentItems.Length - 1;
            UpdateSelectionHighlight();
            _tooltipText = GetCurrentDescription();
        }
        else if (IsKeyJustPressed(current, previous, Keys.Down))
        {
            _selectedIndex = (_selectedIndex + 1) % _currentItems.Length;
            UpdateSelectionHighlight();
            _tooltipText = GetCurrentDescription();
        }
        else if (IsKeyJustPressed(current, previous, Keys.Enter) ||
                 IsKeyJustPressed(current, previous, Keys.Space))
        {
            ConfirmSelection();
        }
        else if (IsKeyJustPressed(current, previous, Keys.Escape) ||
                 IsKeyJustPressed(current, previous, Keys.Back))
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
        }
    }

    private void HandleMainMenuConfirm()
    {
        switch (_selectedIndex)
        {
            case 0: // Attack
                _selectedAbility = null;
                SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
                break;
            case 1: // Ability
                SwitchToScreen(MenuScreen.AbilitySelect, BuildAbilityItems());
                break;
            case 2: // Items
                FireAction(new PlayerAction { Type = ActionType.UseItem });
                break;
            case 3: // Flee
                FireAction(new PlayerAction { Type = ActionType.Flee });
                break;
        }
    }

    private void HandleAbilityConfirm()
    {
        var abilityCount = _availableAbilities.Count;
        if (_selectedIndex == abilityCount)
        {
            // Back
            SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
            return;
        }

        var (template, _, canAfford) = _availableAbilities[_selectedIndex];
        if (!canAfford) return;

        _selectedAbility = template;

        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);

        if (targetType == CombatTargetTypes.MultipleTarget)
        {
            // Auto-target for multi-target abilities
            var actualCount = Math.Min(targetCount, _aliveEnemies.Count);
            var targets = _aliveEnemies.Take(actualCount).ToList();
            FireAction(new PlayerAction
            {
                Type = ActionType.Ability,
                Ability = template,
                Targets = targets
            });
        }
        else
        {
            // Single-target: go to target selection
            SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
        }
    }

    private void HandleTargetConfirm()
    {
        if (_selectedIndex == _aliveEnemies.Count)
        {
            // Back
            var backScreen = _selectedAbility != null ? MenuScreen.AbilitySelect : MenuScreen.MainMenu;
            var backItems = _selectedAbility != null ? BuildAbilityItems() : BuildMainMenuItems();
            SwitchToScreen(backScreen, backItems);
            return;
        }

        var target = _aliveEnemies[_selectedIndex];

        FireAction(_selectedAbility != null
            ? new PlayerAction { Type = ActionType.Ability, Ability = _selectedAbility, Targets = [target] }
            : new PlayerAction { Type = ActionType.BasicAttack, Targets = [target] });
    }

    private void GoBack()
    {
        switch (_currentScreen)
        {
            case MenuScreen.MainMenu:
                // Escape on main menu → basic attack on first enemy as default
                if (_aliveEnemies.Count > 0)
                    FireAction(new PlayerAction { Type = ActionType.BasicAttack, Targets = [_aliveEnemies[0]] });
                break;
            case MenuScreen.AbilitySelect:
                SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
                break;
            case MenuScreen.TargetSelect:
                if (_selectedAbility != null)
                    SwitchToScreen(MenuScreen.AbilitySelect, BuildAbilityItems());
                else
                    SwitchToScreen(MenuScreen.MainMenu, BuildMainMenuItems());
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

        // Draw tooltip description above the menu
        if (!string.IsNullOrEmpty(_tooltipText))
        {
            var tooltipY = _menuY - TooltipH - TooltipGap;
            TextHelper.DrawStringWithSpacing(sb, font, _tooltipText,
                new Vector2(MenuX + MenuPadding, tooltipY + 3), new Color(200, 200, 180));
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
                    color = new Color(120, 60, 60);
                else if (isSelected)
                    color = Color.White;
                else
                    color = new Color(180, 180, 190);
            }
            else if (IsBackItem(i))
            {
                color = new Color(180, 180, 100);
            }
            else if (isSelected)
            {
                color = Color.White;
            }
            else
            {
                color = new Color(180, 180, 190);
            }

            TextHelper.DrawStringWithSpacing(sb, font, _currentItems[i], new Vector2(x, y), color);
        }
    }

    private bool IsBackItem(int index)
    {
        if (_currentScreen == MenuScreen.AbilitySelect && index == _availableAbilities.Count) return true;
        if (_currentScreen == MenuScreen.TargetSelect && index == _aliveEnemies.Count) return true;
        return false;
    }

    private static bool IsKeyJustPressed(KeyboardState current, KeyboardState previous, Keys key)
    {
        return current.IsKeyDown(key) && !previous.IsKeyDown(key);
    }

    private static void SetRectColor(ColoredRectangleRuntime rect, Color color)
    {
        rect.Red = color.R;
        rect.Green = color.G;
        rect.Blue = color.B;
        rect.Alpha = color.A;
    }
}
