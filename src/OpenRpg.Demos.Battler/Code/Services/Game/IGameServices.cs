using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Services.Game;

public interface IGameServices
{
    SpriteBatch GetSpriteBatch { get; }
    GraphicsDeviceManager GetGraphicsDeviceManager { get; }
}