using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Services.Game;

public class GameServices : IGameServices
{
    public SpriteBatch GetSpriteBatch { get; set; } = null;
    public GraphicsDeviceManager GetGraphicsDeviceManager { get; set; } = null;
}