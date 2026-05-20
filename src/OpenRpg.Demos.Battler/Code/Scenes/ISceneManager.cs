#nullable enable
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes;

public interface ISceneManager
{
    IScene? ActiveScene { get; }
    Task SetScene(IScene scene);
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
