using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class BattleBottomPanelUi
{
    private readonly ColoredRectangleRuntime _panelBg;
    private readonly List<ColoredRectangleRuntime> _enemyHpBarBgs = [];
    private readonly List<ColoredRectangleRuntime> _enemyHpBarFills = [];
    private readonly List<ColoredRectangleRuntime> _partyHpBarBgs = [];
    private readonly List<ColoredRectangleRuntime> _partyHpBarFills = [];

    private readonly List<string> _enemyNames = [];
    private readonly List<int> _enemyHps = [];
    private readonly List<int> _enemyMaxHps = [];
    private readonly List<bool> _enemyAlive = [];
    private readonly List<string> _partyNames = [];
    private readonly List<int> _partyHps = [];
    private readonly List<int> _partyMaxHps = [];
    private readonly List<int> _partyManas = [];
    private readonly List<int> _partyMaxManas = [];
    private readonly List<bool> _partyAlive = [];

    private const int MaxEnemyRows = 6;
    private const int MaxPartyRows = 4;
    private const int RowStartY = 414;
    private const int RowSpacing = 24;

    private static readonly Color HpGreen = new(80, 200, 60);
    private static readonly Color HpYellow = new(220, 200, 40);
    private static readonly Color HpRed = new(200, 40, 40);
    private static readonly Color HpBarBgColor = new(40, 40, 50);
    private static readonly Color EnemyNameColor = new(220, 150, 150);
    private static readonly Color PartyNameColor = new(150, 180, 220);
    private static readonly Color HpTextColor = new(200, 200, 200);

    public BattleBottomPanelUi()
    {
        _panelBg = new ColoredRectangleRuntime();
        _panelBg.Width = 800;
        _panelBg.Height = 220;
        _panelBg.X = 0;
        _panelBg.Y = 380;
        SetRectColor(_panelBg, new Color(10, 10, 25) * 0.9f);
        _panelBg.AddToRoot();

        for (var i = 0; i < MaxEnemyRows; i++)
        {
            var y = RowStartY + i * RowSpacing;
            _enemyHpBarBgs.Add(CreateHpBarBg(150, y));
            _enemyHpBarFills.Add(CreateHpBarFill(150, y));
        }

        for (var i = 0; i < MaxPartyRows; i++)
        {
            var y = RowStartY + i * RowSpacing;
            _partyHpBarBgs.Add(CreateHpBarBg(548, y));
            _partyHpBarFills.Add(CreateHpBarFill(548, y));
        }
    }

    public void Update(List<BattleEntity> party, List<BattleEntity> enemies)
    {
        _enemyNames.Clear();
        _enemyHps.Clear();
        _enemyMaxHps.Clear();
        _enemyAlive.Clear();

        for (var i = 0; i < MaxEnemyRows; i++)
        {
            var visible = i < enemies.Count;
            _enemyHpBarBgs[i].Visible = visible;
            _enemyHpBarFills[i].Visible = visible;

            if (visible)
            {
                var e = enemies[i];
                _enemyNames.Add(e.Name);
                _enemyHps.Add(e.Hp);
                _enemyMaxHps.Add(e.MaxHp);
                _enemyAlive.Add(e.IsAlive);
                var ratio = e.MaxHp > 0 ? (float)e.Hp / e.MaxHp : 0f;
                _enemyHpBarFills[i].Width = ratio * 100;
                SetRectColor(_enemyHpBarFills[i], RatioToColor(ratio));
            }
            else
            {
                _enemyNames.Add("");
                _enemyHps.Add(0);
                _enemyMaxHps.Add(0);
                _enemyAlive.Add(false);
            }
        }

        _partyNames.Clear();
        _partyHps.Clear();
        _partyMaxHps.Clear();
        _partyManas.Clear();
        _partyMaxManas.Clear();
        _partyAlive.Clear();

        for (var i = 0; i < MaxPartyRows; i++)
        {
            var visible = i < party.Count;
            _partyHpBarBgs[i].Visible = visible;
            _partyHpBarFills[i].Visible = visible;

            if (visible)
            {
                var e = party[i];
                _partyNames.Add(e.Name);
                _partyHps.Add(e.Hp);
                _partyMaxHps.Add(e.MaxHp);
                _partyManas.Add(e.Mana);
                _partyMaxManas.Add(e.MaxMana);
                _partyAlive.Add(e.IsAlive);
                var ratio = e.MaxHp > 0 ? (float)e.Hp / e.MaxHp : 0f;
                _partyHpBarFills[i].Width = ratio * 100;
                SetRectColor(_partyHpBarFills[i], RatioToColor(ratio));
            }
            else
            {
                _partyNames.Add("");
                _partyHps.Add(0);
                _partyMaxHps.Add(0);
                _partyManas.Add(0);
                _partyMaxManas.Add(0);
                _partyAlive.Add(false);
            }
        }
    }

    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        var headerY = 392;
        TextHelper.DrawStringWithSpacing(sb, font, "ENEMIES", new Vector2(6, headerY), Color.White);
        TextHelper.DrawStringWithSpacing(sb, font, "PARTY", new Vector2(404, headerY), Color.White);

        for (var i = 0; i < MaxEnemyRows; i++)
        {
            if (!_enemyHpBarBgs[i].Visible) continue;

            var y = RowStartY + i * RowSpacing;
            TextHelper.DrawStringWithSpacing(sb, font, NormalizeName(_enemyNames[i]), new Vector2(6, y), _enemyAlive[i] ? EnemyNameColor : Color.Gray);
            var hpText = $"{_enemyHps[i]}/{_enemyMaxHps[i]}";
            TextHelper.DrawStringWithSpacing(sb, font, hpText, new Vector2(254, y), HpTextColor);
        }

        for (var i = 0; i < MaxPartyRows; i++)
        {
            if (!_partyHpBarBgs[i].Visible) continue;

            var y = RowStartY + i * RowSpacing;
            TextHelper.DrawStringWithSpacing(sb, font, NormalizeName(_partyNames[i]), new Vector2(404, y), _partyAlive[i] ? PartyNameColor : Color.Gray);
            var hpText = $"{_partyHps[i]}/{_partyMaxHps[i]}";
            TextHelper.DrawStringWithSpacing(sb, font, hpText, new Vector2(652, y), HpTextColor);
            if (_partyMaxManas[i] > 0)
            {
                var mpText = $"MP:{_partyManas[i]}/{_partyMaxManas[i]}";
                TextHelper.DrawStringWithSpacing(sb, font, mpText, new Vector2(652, y + 12), new Color(100, 160, 255));
            }
        }
    }

    private static string NormalizeName(string name)
    {
        return Regex.Replace(name, "(?<=[a-z])(?=[A-Z])", " ");
    }

    public void Unload()
    {
        _panelBg.RemoveFromRoot();
        foreach (var r in _enemyHpBarBgs) r.RemoveFromRoot();
        foreach (var r in _enemyHpBarFills) r.RemoveFromRoot();
        foreach (var r in _partyHpBarBgs) r.RemoveFromRoot();
        foreach (var r in _partyHpBarFills) r.RemoveFromRoot();
    }

    private static ColoredRectangleRuntime CreateHpBarBg(int x, int y)
    {
        var rect = new ColoredRectangleRuntime();
        rect.X = x;
        rect.Y = y + 2;
        rect.Width = 100;
        rect.Height = 14;
        SetRectColor(rect, HpBarBgColor);
        rect.AddToRoot();
        return rect;
    }

    private static ColoredRectangleRuntime CreateHpBarFill(int x, int y)
    {
        var rect = new ColoredRectangleRuntime();
        rect.X = x;
        rect.Y = y + 2;
        rect.Width = 100;
        rect.Height = 14;
        SetRectColor(rect, HpGreen);
        rect.AddToRoot();
        return rect;
    }

    private static void SetRectColor(ColoredRectangleRuntime rect, Color color)
    {
        rect.Red = color.R;
        rect.Green = color.G;
        rect.Blue = color.B;
        rect.Alpha = color.A;
    }

    private static Color RatioToColor(float ratio)
    {
        if (ratio > 0.5f) return HpGreen;
        if (ratio > 0.25f) return HpYellow;
        return HpRed;
    }
}
