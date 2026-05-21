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
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

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

    private static string[] BuildMainMenuItems()
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
        }
    }

    private void HandleMainMenuConfirm()
    {
        switch (_selectedIndex)
        {
            case 0:
                _selectedAbility = null;
                SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
                break;
            case 1:
                SwitchToScreen(MenuScreen.AbilitySelect, BuildAbilityItems());
                break;
            case 2:
                FireAction(new PlayerAction { Type = ActionType.UseItem });
                break;
            case 3:
                FireAction(new PlayerAction { Type = ActionType.Flee });
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

        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);

        if (targetType == CombatTargetTypes.MultipleTarget)
        {
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
            SwitchToScreen(MenuScreen.TargetSelect, BuildTargetItems());
        }
    }

    private void HandleTargetConfirm()
    {
        if (_selectedIndex == _aliveEnemies.Count)
        {
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
        if (_currentScreen == MenuScreen.TargetSelect && index == _aliveEnemies.Count) return true;
        return false;
    }
}
