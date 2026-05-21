using Microsoft.Xna.Framework;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class FloatingDamageNumber
{
    public Vector2 Position { get; set; }
    public int Damage { get; set; }
    public float Lifetime { get; set; }
    public float MaxLifetime { get; set; }
    public bool IsCrit { get; set; }
    public Color Color { get; set; }
    public float Opacity => MathHelper.Clamp(Lifetime / MaxLifetime, 0, 1);

    public FloatingDamageNumber(Vector2 position, int damage, bool isCrit)
    {
        Position = position;
        Damage = damage;
        IsCrit = isCrit;
        MaxLifetime = isCrit ? 1.2f : 1.0f;
        Lifetime = MaxLifetime;
        Color = isCrit ? Color.Gold : Color.Red;
    }

    public void Update(float dt)
    {
        Lifetime -= dt;
        Position = new Vector2(Position.X, Position.Y - dt * (IsCrit ? 60f : 40f));
    }

    public bool IsExpired => Lifetime <= 0;
}
