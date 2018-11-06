namespace OpenRpg.Genres.Fantasy.Types
{
    public static class BonusTypes
    {
        public static byte UnknownBonus = 0;
            
        // Generic Melee/Magic
        public static byte MeleeAttackBonus = 1;
        public static byte MeleeDefenseBonus = 2;
        public static byte MagicAttackBonus = 3;
        public static byte MagicDefenseBonus = 4;
            
        // General
        public static byte DexterityBonus = 5;
        public static byte IntelligenceBonus = 6;
        public static byte StrengthBonus = 7;
        public static byte ConstitutionBonus = 8;
        public static byte CharismaBonus = 9;
        public static byte HealthBonus = 10;
        public static byte HealthRestore = 11;
        public static byte MagicRestore = 12;
        public static byte ExperienceRestore = 13;
        public static byte MagicBonus = 14;

        // Melee Damage (Raw Damage)
        public static byte SlashingDamage = 25;
        public static byte PiercingDamage = 26;
        public static byte BluntDamage = 27;
        public static byte UnarmedDamage = 28;

        // Melee Bonus (Damage Bonus)
        public static byte SlashingBonus = 29;
        public static byte PiercingBonus = 30;
        public static byte BluntBonus = 31;
        public static byte UnarmedBonus = 32;

        // Melee Defense (Defense Bonus)
        public static byte SlashingDefense = 33;
        public static byte PiercingDefense = 34;
        public static byte BluntDefense = 35;
        public static byte UnarmedDefense = 36;

        // Magic Damage (Raw Damage)
        public static byte FireDamage = 50;
        public static byte IceDamage = 51;
        public static byte EarthDamage = 52;
        public static byte WindDamage = 53;
        public static byte LightDamage = 54;
        public static byte DarkDamage = 55;

        // Magic Bonus (Damage Bonus)
        public static byte FireBonus = 56;
        public static byte IceBonus = 57;
        public static byte EarthBonus = 58;
        public static byte WindBonus = 59;
        public static byte LightBonus = 60;
        public static byte DarkBonus = 61;

        // Magic Defense (Defense Bonus)
        public static byte FireDefense = 62;
        public static byte IceDefense = 63;
        public static byte EarthDefense = 64;
        public static byte WindDefense = 65;
        public static byte LightDefense = 66;
        public static byte DarkDefense = 67;
    }
}