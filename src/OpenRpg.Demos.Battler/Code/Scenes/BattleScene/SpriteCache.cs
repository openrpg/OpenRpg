using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class SpriteCache
{
    private readonly Dictionary<string, Texture2D> _cache = [];

    public void LoadSprites(IEnumerable<BattleEntity> entities, ContentManager content)
    {
        foreach (var entity in entities)
        {
            if (_cache.TryGetValue(entity.AssetCode, out var tex))
            {
                entity.Sprite = tex;
                continue;
            }

            var subDir = entity.Team == Team.Player ? "Players" : "Enemies";
            var assetPath = $"Sprites/{subDir}/{entity.AssetCode}";

            try
            {
                tex = content.Load<Texture2D>(assetPath);
                _cache[entity.AssetCode] = tex;
                entity.Sprite = tex;
            }
            catch (ContentLoadException)
            {
                System.Diagnostics.Debug.WriteLine($"Missing sprite: {assetPath}");
            }
        }
    }

    public void Dispose()
    {
        foreach (var tex in _cache.Values)
            tex.Dispose();
        _cache.Clear();
    }
}
