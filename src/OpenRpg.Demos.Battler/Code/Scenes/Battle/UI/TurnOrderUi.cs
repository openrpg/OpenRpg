using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public class TurnOrderUi
{
    private readonly ColoredRectangleRuntime _stripBg;
    private readonly List<ColoredRectangleRuntime> _chipBgs = [];
    private readonly List<string> _chipLabels = [];
    private readonly SpriteFont _font;
    private int _currentIndex;
    private float _pulseBrightness;

    private const int StripY = 344;
    private const int StripHeight = 36;
    private const int ChipGap = 8;
    private const int ChipPaddingX = 8;
    private const int MaxSlots = 10;
    private const int TextY = StripY + 6;

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
        _stripBg.SetRectColor(Palette.StripBg);
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
            chipBg.SetRectColor(Palette.FutureChip);
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
            var teamColor = entity.Team == Team.Player ? Palette.ChipPartyTint : Palette.ChipEnemyTint;
            var label = isCurrent ? $"> {NameHelper.NormalizeName(entity.Name)}" : NameHelper.NormalizeName(entity.Name);

            _chipLabels.Add(label);
            labelWidths[i] = _font.MeasureString(label).X;

            if (visible)
                totalTextWidth += labelWidths[i];

            if (isCurrent && pulseBrightness > 0)
            {
                var factor = pulseBrightness * 0.5f;
                var r = (byte)(Palette.CurrentChip.R + (255 - Palette.CurrentChip.R) * factor);
                var g = (byte)(Palette.CurrentChip.G + (255 - Palette.CurrentChip.G) * factor);
                var b = (byte)(Palette.CurrentChip.B + (255 - Palette.CurrentChip.B) * factor);
                _chipBgs[i].Red = r;
                _chipBgs[i].Green = g;
                _chipBgs[i].Blue = b;
            }
            else if (isCurrent)
                _chipBgs[i].SetRectColor(Palette.CurrentChip);
            else if (i < currentTurnIndex)
                _chipBgs[i].SetRectColor(Palette.PastChip);
            else
                _chipBgs[i].SetRectColor(Palette.FutureChip);

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
                    (byte)(Palette.CurrentText.R + (255 - Palette.CurrentText.R) * t),
                    (byte)(Palette.CurrentText.G + (255 - Palette.CurrentText.G) * t),
                    (byte)(Palette.CurrentText.B + (255 - Palette.CurrentText.B) * t));
            }
            else if (i == _currentIndex)
                color = Palette.CurrentText;
            else if (i < _currentIndex)
                color = Palette.PastText;
            else
                color = Palette.FutureText;

            TextHelper.DrawStringWithSpacing(sb, _font, label, new Vector2(x, y), color);
        }
    }

    public void Unload()
    {
        _stripBg.RemoveFromRoot();
        foreach (var bg in _chipBgs)
            bg.RemoveFromRoot();
    }
}
