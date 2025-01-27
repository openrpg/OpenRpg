namespace OpenRpg.Genres.Fantasy.Types
{
    public interface FantasyEntityStatsVariableTypes : Genres.Types.GenreEntityStatsVariableTypes
    {
        public static readonly int MaxMagic = 50;
        
        // Base stats
        public static readonly int Strength = 60;
        public static readonly int Dexterity = 61;
        public static readonly int Constitution = 62;
        public static readonly int Intelligence = 63;
        public static readonly int Wisdom = 64;
        public static readonly int Charisma = 65;

        // Attack stats
        public static readonly int SlashingDamage = 80;
        public static readonly int BluntDamage = 81;
        public static readonly int PiercingDamage = 82;
        public static readonly int UnarmedDamage = 83;
        public static readonly int FireDamage = 84;
        public static readonly int IceDamage = 85;
        public static readonly int EarthDamage = 86;
        public static readonly int WindDamage = 87;
        public static readonly int LightDamage = 88;
        public static readonly int DarkDamage = 89;
        
        // Defense stats
        public static readonly int SlashingDefense = 100;
        public static readonly int BluntDefense = 101;
        public static readonly int PiercingDefense = 102;
        public static readonly int UnarmedDefense = 103;
        public static readonly int FireDefense = 104;
        public static readonly int IceDefense = 105;
        public static readonly int EarthDefense = 106;
        public static readonly int WindDefense = 107;
        public static readonly int LightDefense = 108;
        public static readonly int DarkDefense = 109;
    }
}