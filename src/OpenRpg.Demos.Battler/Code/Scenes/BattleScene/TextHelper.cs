using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public static class TextHelper
{
    private const int SpaceGap = 4;

    public static void DrawStringWithSpacing(SpriteBatch sb, SpriteFont font, string text, Vector2 position, Color color)
    {
        if (string.IsNullOrEmpty(text)) return;

        var x = (int)position.X;
        var y = (int)position.Y;
        var words = text.Split(' ');

        foreach (var word in words)
        {
            if (word.Length == 0) continue;
            sb.DrawString(font, word, new Vector2(x, y), color);
            x += (int)font.MeasureString(word).X + SpaceGap;
        }
    }
}
