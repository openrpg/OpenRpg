namespace OpenRpg.Genres.Scifi.Types
{
    public interface ScifiShipStatsVariableTypes
    {
        public static readonly int Unknown = 0;
        
        // Vital stats
        public static readonly int MaxEnergy = 50;
        public static readonly int MaxArmour = 51;
        public static readonly int MaxShield = 52;
        
        // Attack stats
        public static readonly int PhysicalDamage = 80;
        public static readonly int IonDamage = 81;
        public static readonly int LaserDamage = 82;
        public static readonly int BallisticDamage = 83;
        public static readonly int ExplosiveDamage = 84;
        
        // Defense stats
        public static readonly int PhysicalDefense = 100;
        public static readonly int IonDefense = 101;
        public static readonly int LaserDefense = 102;
        public static readonly int BallisticDefense = 103;
        public static readonly int ExplosiveDefense = 104;
    }
}