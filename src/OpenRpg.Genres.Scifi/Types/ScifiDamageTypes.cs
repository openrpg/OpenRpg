namespace OpenRpg.Genres.Scifi.Types
{
    public interface ScifiDamageTypes : Genres.Types.GenreDamageTypes
    {
        public static readonly int Physical = 50;
        public static readonly int Ion = 51;
        public static readonly int Laser = 52;
        public static readonly int Ballistic = 53;
        public static readonly int Explosive = 54;
    }
}