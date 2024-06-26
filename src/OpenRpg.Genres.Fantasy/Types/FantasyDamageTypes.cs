namespace OpenRpg.Genres.Fantasy.Types
{
    public interface FantasyDamageTypes : Genres.Types.GenreDamageTypes
    {
        // Melee types
        public static readonly int SlashingDamage = 50;
        public static readonly int BluntDamage = 51;
        public static readonly int PiercingDamage = 52;
        public static readonly int UnarmedDamage = 53;

        // Magic types
        public static readonly int FireDamage = 80;
        public static readonly int IceDamage = 81;
        public static readonly int EarthDamage = 82;
        public static readonly int WindDamage = 83;
        public static readonly int LightDamage = 84;
        public static readonly int DarkDamage = 85;
    }
}