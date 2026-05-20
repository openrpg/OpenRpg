using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class CombatLogUi
{
    private readonly ColoredRectangleRuntime _bg;
    private string _message = "";

    private const int BarY = 310;
    private const int BarH = 34;
    private const int TextX = 6;
    private const int TextY = 316;

    private static readonly Color BarColor = new(10, 10, 25);

    public CombatLogUi()
    {
        _bg = new ColoredRectangleRuntime
        {
            X = 0,
            Y = BarY,
            Width = 800,
            Height = BarH
        };
        SetRectColor(_bg, BarColor * 0.85f);
        _bg.AddToRoot();
    }

    public void Update(string message)
    {
        _message = message;
    }

    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        TextHelper.DrawStringWithSpacing(sb, font, _message, new Vector2(TextX, TextY), Color.White);
    }

    public void Unload()
    {
        _bg.RemoveFromRoot();
    }

    private static void SetRectColor(ColoredRectangleRuntime rect, Color color)
    {
        rect.Red = color.R;
        rect.Green = color.G;
        rect.Blue = color.B;
        rect.Alpha = color.A;
    }
}
