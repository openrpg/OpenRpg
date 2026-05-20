#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public enum Team { Player, Enemy }

public class BattleEntity
{
    public string Name { get; set; } = string.Empty;
    public string AssetCode { get; set; } = string.Empty;
    public Team Team { get; set; }
    public int Row { get; set; }
    public int SlotInRow { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 OriginPosition { get; set; }
    public int Hp { get; set; }
    public int Initiative { get; set; }
    public int MaxHp { get; set; }
    public bool IsAlive => Hp > 0;
    public Texture2D? Sprite { get; set; }
}
