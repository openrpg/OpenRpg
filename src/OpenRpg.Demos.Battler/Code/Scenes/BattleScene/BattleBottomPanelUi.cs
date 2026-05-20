using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameGum.GueDeriving;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class BattleBottomPanelUi
{
    private readonly ColoredRectangleRuntime _panelBg;
    private readonly List<TextRuntime> _enemyNameTexts = [];
    private readonly List<ColoredRectangleRuntime> _enemyHpBarBgs = [];
    private readonly List<ColoredRectangleRuntime> _enemyHpBarFills = [];
    private readonly List<TextRuntime> _enemyHpTexts = [];

    private readonly List<TextRuntime> _partyNameTexts = [];
    private readonly List<ColoredRectangleRuntime> _partyHpBarBgs = [];
    private readonly List<ColoredRectangleRuntime> _partyHpBarFills = [];
    private readonly List<TextRuntime> _partyHpTexts = [];

    private const int MaxEnemyRows = 6;
    private const int MaxPartyRows = 4;
    private const int RowStartY = 418;
    private const int RowSpacing = 22;

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

        var enemyHeader = new TextRuntime();
        enemyHeader.Text = "ENEMIES";
        enemyHeader.X = 6;
        enemyHeader.Y = 392;
        enemyHeader.Width = 390;
        enemyHeader.Height = 24;
        enemyHeader.FontScale = 1.0f;
        enemyHeader.AddToRoot();

        var partyHeader = new TextRuntime();
        partyHeader.Text = "PARTY";
        partyHeader.X = 404;
        partyHeader.Y = 392;
        partyHeader.Width = 390;
        partyHeader.Height = 24;
        partyHeader.FontScale = 1.0f;
        partyHeader.AddToRoot();

        for (var i = 0; i < MaxEnemyRows; i++)
        {
            var y = RowStartY + i * RowSpacing;

            var nameText = CreateRowText(6, y, 140, EnemyNameColor);
            _enemyNameTexts.Add(nameText);

            var hpBg = CreateHpBarBg(150, y);
            _enemyHpBarBgs.Add(hpBg);

            var hpFill = CreateHpBarFill(150, y);
            _enemyHpBarFills.Add(hpFill);

            var hpText = CreateRowText(254, y, 55, HpTextColor);
            _enemyHpTexts.Add(hpText);
        }

        for (var i = 0; i < MaxPartyRows; i++)
        {
            var y = RowStartY + i * RowSpacing;

            var nameText = CreateRowText(404, y, 140, PartyNameColor);
            _partyNameTexts.Add(nameText);

            var hpBg = CreateHpBarBg(548, y);
            _partyHpBarBgs.Add(hpBg);

            var hpFill = CreateHpBarFill(548, y);
            _partyHpBarFills.Add(hpFill);

            var hpText = CreateRowText(652, y, 55, HpTextColor);
            _partyHpTexts.Add(hpText);
        }
    }

    private static TextRuntime CreateRowText(int x, int y, int width, Color color)
    {
        var text = new TextRuntime();
        text.X = x;
        text.Y = y;
        text.Width = width;
        text.Height = 18;
        text.FontScale = 0.82f;
        text.Color = color;
        text.AddToRoot();
        return text;
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

    public void Update(List<BattleEntity> party, List<BattleEntity> enemies)
    {
        for (var i = 0; i < MaxEnemyRows; i++)
        {
            var visible = i < enemies.Count;
            SetRowVisible(i, visible, false);

            if (visible)
            {
                var e = enemies[i];
                _enemyNameTexts[i].Text = e.Name;
                var ratio = e.MaxHp > 0 ? (float)e.Hp / e.MaxHp : 0f;
                _enemyHpBarFills[i].Width = ratio * 100;
                SetRectColor(_enemyHpBarFills[i], RatioToColor(ratio));
                _enemyHpTexts[i].Text = $"{e.Hp}/{e.MaxHp}";
            }
        }

        for (var i = 0; i < MaxPartyRows; i++)
        {
            var visible = i < party.Count;
            SetRowVisible(i, visible, true);

            if (visible)
            {
                var e = party[i];
                _partyNameTexts[i].Text = e.Name;
                var ratio = e.MaxHp > 0 ? (float)e.Hp / e.MaxHp : 0f;
                _partyHpBarFills[i].Width = ratio * 100;
                SetRectColor(_partyHpBarFills[i], RatioToColor(ratio));
                _partyHpTexts[i].Text = $"{e.Hp}/{e.MaxHp}";
            }
        }
    }

    private void SetRowVisible(int index, bool visible, bool isParty)
    {
        var names = isParty ? _partyNameTexts : _enemyNameTexts;
        var hpBgs = isParty ? _partyHpBarBgs : _enemyHpBarBgs;
        var hpFills = isParty ? _partyHpBarFills : _enemyHpBarFills;
        var hpTexts = isParty ? _partyHpTexts : _enemyHpTexts;

        names[index].Visible = visible;
        hpBgs[index].Visible = visible;
        hpFills[index].Visible = visible;
        hpTexts[index].Visible = visible;
    }

    private static Color RatioToColor(float ratio)
    {
        if (ratio > 0.5f) return HpGreen;
        if (ratio > 0.25f) return HpYellow;
        return HpRed;
    }

    public void Unload()
    {
        _panelBg.RemoveFromRoot();

        foreach (var t in _enemyNameTexts) t.RemoveFromRoot();
        foreach (var r in _enemyHpBarBgs) r.RemoveFromRoot();
        foreach (var r in _enemyHpBarFills) r.RemoveFromRoot();
        foreach (var t in _enemyHpTexts) t.RemoveFromRoot();

        foreach (var t in _partyNameTexts) t.RemoveFromRoot();
        foreach (var r in _partyHpBarBgs) r.RemoveFromRoot();
        foreach (var r in _partyHpBarFills) r.RemoveFromRoot();
        foreach (var t in _partyHpTexts) t.RemoveFromRoot();
    }
}
