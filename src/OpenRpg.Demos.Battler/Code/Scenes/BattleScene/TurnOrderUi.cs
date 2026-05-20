using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class TurnOrderUi
{
    private readonly ColoredRectangleRuntime _stripBg;
    private readonly List<ColoredRectangleRuntime> _chipBgs = [];
    private readonly List<string> _chipLabels = [];
    private readonly SpriteFont _font;
    private int _currentIndex;
    private float _pulseBrightness;

    private const int StripHeight = 36;
    private const int StripY = 344;
    private const int ChipGap = 8;
    private const int ChipPaddingX = 8;
    private const int MaxSlots = 10;
    private const int TextY = StripY + 6;

    private static readonly Color StripBgColor = new Color(10, 10, 25) * 0.85f;
    private static readonly Color CurrentChipColor = new(70, 70, 95);
    private static readonly Color FutureChipColor = new(35, 35, 50);
    private static readonly Color PastChipColor = new(18, 18, 28);
    private static readonly Color PartyColor = new(80, 140, 220);
    private static readonly Color EnemyColor = new(220, 80, 80);
    private static readonly Color CurrentTextColor = Color.White;
    private static readonly Color FutureTextColor = new(180, 180, 190);
    private static readonly Color PastTextColor = new(60, 60, 70);

    public TurnOrderUi(SpriteFont font)
    {
        _font = font;

        _stripBg = new ColoredRectangleRuntime
        {
            X = 0,
            Y = StripY,
            Width = 800,
            Height = StripHeight
        };
        SetRectColor(_stripBg, StripBgColor);
        _stripBg.AddToRoot();

        for (var i = 0; i < MaxSlots; i++)
        {
            var chipBg = new ColoredRectangleRuntime
            {
                X = 0,
                Y = StripY + 3,
                Width = 0,
                Height = StripHeight - 6
            };
            SetRectColor(chipBg, FutureChipColor);
            chipBg.AddToRoot();
            _chipBgs.Add(chipBg);
            _chipLabels.Add("");
        }
    }

    public void Update(List<BattleEntity> allEntities, int currentTurnIndex, float pulseBrightness = 0)
    {
        _currentIndex = currentTurnIndex;
        _pulseBrightness = pulseBrightness;
        _chipLabels.Clear();

        var totalGapWidth = ChipGap * (Math.Min(allEntities.Count, MaxSlots) - 1);
        var availableWidth = 800 - totalGapWidth;
        var totalTextWidth = 0f;
        var labelWidths = new float[Math.Min(allEntities.Count, MaxSlots)];

        for (var i = 0; i < MaxSlots; i++)
        {
            var visible = i < allEntities.Count;
            _chipBgs[i].Visible = visible;

            if (!visible)
            {
                _chipLabels.Add("");
                continue;
            }

            var entity = allEntities[i];
            var isCurrent = i == currentTurnIndex;
            var teamColor = entity.Team == Team.Player ? PartyColor : EnemyColor;
            var label = isCurrent ? $"> {NormalizeName(entity.Name)}" : NormalizeName(entity.Name);

            _chipLabels.Add(label);
            labelWidths[i] = _font.MeasureString(label).X;

            if (visible)
                totalTextWidth += labelWidths[i];

            if (isCurrent && pulseBrightness > 0)
            {
                var factor = pulseBrightness * 0.5f;
                var r = (byte)(CurrentChipColor.R + (255 - CurrentChipColor.R) * factor);
                var g = (byte)(CurrentChipColor.G + (255 - CurrentChipColor.G) * factor);
                var b = (byte)(CurrentChipColor.B + (255 - CurrentChipColor.B) * factor);
                _chipBgs[i].Red = r;
                _chipBgs[i].Green = g;
                _chipBgs[i].Blue = b;
            }
            else if (isCurrent)
                SetRectColor(_chipBgs[i], CurrentChipColor);
            else if (i < currentTurnIndex)
                SetRectColor(_chipBgs[i], PastChipColor);
            else
                SetRectColor(_chipBgs[i], FutureChipColor);

            _chipBgs[i].Red = (byte)(_chipBgs[i].Red * 0.7 + teamColor.R * 0.3);
            _chipBgs[i].Green = (byte)(_chipBgs[i].Green * 0.7 + teamColor.G * 0.3);
            _chipBgs[i].Blue = (byte)(_chipBgs[i].Blue * 0.7 + teamColor.B * 0.3);
        }

        var scale = totalTextWidth > 0 && totalTextWidth > availableWidth
            ? availableWidth / totalTextWidth : 1f;

        var cursorX = 2;
        for (var i = 0; i < MaxSlots && i < _chipLabels.Count; i++)
        {
            if (!_chipBgs[i].Visible) continue;

            var chipW = (int)(labelWidths[i] * scale) + ChipPaddingX * 2;
            _chipBgs[i].X = cursorX;
            _chipBgs[i].Width = chipW;
            cursorX += chipW + ChipGap;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        for (var i = 0; i < MaxSlots && i < _chipLabels.Count; i++)
        {
            if (!_chipBgs[i].Visible) continue;

            var label = _chipLabels[i];
            if (string.IsNullOrEmpty(label)) continue;

            var x = _chipBgs[i].X + ChipPaddingX;
            var y = TextY;

            Color color;
            if (i == _currentIndex && _pulseBrightness > 0)
            {
                var t = _pulseBrightness * 0.75f;
                color = new Color(
                    (byte)(CurrentTextColor.R + (255 - CurrentTextColor.R) * t),
                    (byte)(CurrentTextColor.G + (255 - CurrentTextColor.G) * t),
                    (byte)(CurrentTextColor.B + (255 - CurrentTextColor.B) * t));
            }
            else if (i == _currentIndex)
                color = CurrentTextColor;
            else if (i < _currentIndex)
                color = PastTextColor;
            else
                color = FutureTextColor;

            TextHelper.DrawStringWithSpacing(sb, _font, label, new Vector2(x, y), color);
        }
    }

    public void Unload()
    {
        _stripBg.RemoveFromRoot();
        foreach (var bg in _chipBgs)
            bg.RemoveFromRoot();
    }

    private static void SetRectColor(ColoredRectangleRuntime rect, Color color)
    {
        rect.Red = color.R;
        rect.Green = color.G;
        rect.Blue = color.B;
        rect.Alpha = color.A;
    }

    private static string NormalizeName(string name)
    {
        return Regex.Replace(name, "(?<=[a-z])(?=[A-Z])", " ");
    }
}
