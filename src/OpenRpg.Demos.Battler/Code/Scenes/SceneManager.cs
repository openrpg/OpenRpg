#nullable enable
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes;

public class SceneManager : ISceneManager
{
    public IScene? ActiveScene { get; private set; }

    public async Task SetScene(IScene scene)
    {
        ActiveScene?.Unload();
        ActiveScene = scene;
        await scene.LoadAsync();
    }

    public void Update(GameTime gameTime) => ActiveScene?.Update(gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) => ActiveScene?.Draw(gameTime, spriteBatch);
    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) => ActiveScene?.DrawUI(gameTime, spriteBatch);
}
