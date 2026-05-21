using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public static class TextHelper
{
    private const int SpaceGap = 4;

    public static void DrawStringWithSpacing(SpriteBatch sb, SpriteFont font, string text, Vector2 position, Color color, bool centered = false)
    {
        if (string.IsNullOrEmpty(text)) return;

        var x = (int)position.X;
        var y = (int)position.Y;

        if (centered)
        {
            var totalWidth = MeasureStringWidth(font, text);
            x -= totalWidth / 2;
        }

        var words = text.Split(' ');

        foreach (var word in words)
        {
            if (word.Length == 0) continue;
            sb.DrawString(font, word, new Vector2(x, y), color);
            x += (int)font.MeasureString(word).X + SpaceGap;
        }
    }

    public static int MeasureStringWidth(SpriteFont font, string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        var words = text.Split(' ');
        var total = 0;
        foreach (var word in words)
        {
            if (word.Length == 0) continue;
            total += (int)font.MeasureString(word).X + SpaceGap;
        }
        return total > 0 ? total - SpaceGap : 0;
    }
}
