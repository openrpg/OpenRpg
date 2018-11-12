namespace OpenRpg.Genres.Fantasy.Combat
{
    public class Damage
    {
        public byte Type { get; }
        public float Value { get; }

        public Damage(byte type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}