using Microsoft.Xna.Framework.Input;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public static class InputHelper
{
    public static bool IsKeyJustPressed(KeyboardState current, KeyboardState previous, Keys key)
    {
        return current.IsKeyDown(key) && !previous.IsKeyDown(key);
    }
}
