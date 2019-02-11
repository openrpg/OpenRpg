namespace OpenRpg.Combat.Attacks
{
    public class Damage
    {
        public int Type { get; }
        public float Value { get; }

        public Damage(int type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}