using Microsoft.Xna.Framework;
using MonoGameGum.GueDeriving;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public static class GumExtensions
{
    public static void SetRectColor(this ColoredRectangleRuntime rect, Color color)
    {
        rect.Red = color.R;
        rect.Green = color.G;
        rect.Blue = color.B;
        rect.Alpha = color.A;
    }
}
