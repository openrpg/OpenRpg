using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public enum Team { Player, Enemy }

public class BattleEntity
{
    public required Character Entity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AssetCode { get; set; } = string.Empty;
    public Team Team { get; set; }
    public int Row { get; set; }
    public int SlotInRow { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 OriginPosition { get; set; }
    public int Hp { get => Entity.State.Health; set => Entity.State.Health = value; }
    public int MaxHp => Entity.Stats.MaxHealth;
    public int Initiative => (int)Entity.Stats.MovementSpeed;
    public int AttackDamage => (int)Entity.Stats.Damage;
    public int Mana => (int)Entity.State.Mana;
    public int MaxMana => (int)Entity.Stats.MaxMana;
    public bool IsAlive => !Entity.State.IsDead;
    public Texture2D Sprite { get; set; }
}
