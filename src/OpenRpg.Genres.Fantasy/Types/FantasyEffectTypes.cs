namespace OpenRpg.Genres.Fantasy.Types
{
    public interface FantasyEffectTypes : Genres.Types.GenreEffectTypes
    {
        // Generic Melee/Magic
        public static readonly int AllMeleeAttackBonusAmount = 50;
        public static readonly int AllMeleeDefenseBonusAmount = 51;
        public static readonly int AllElementDamageBonusAmount = 53;
        public static readonly int AllElementDefenseBonusAmount = 54;
        public static readonly int AllMeleeAttackBonusPercentage = 55;
        public static readonly int AllMeleeDefenseBonusPercentage = 56;
        public static readonly int AllElementDamageBonusPercentage = 57;
        public static readonly int AllElementDefenseBonusPercentage = 58;
            
        // General Attribute Amount
        public static readonly int DexterityBonusAmount = 70;
        public static readonly int IntelligenceBonusAmount = 71;
        public static readonly int WisdomBonusAmount = 72;
        public static readonly int StrengthBonusAmount = 73;
        public static readonly int ConstitutionBonusAmount = 74;
        public static readonly int CharismaBonusAmount = 75;
        public static readonly int HealthBonusAmount = 76;
        public static readonly int MagicBonusAmount = 77;
        public static readonly int HealthRestoreAmount = 78;
        public static readonly int MagicRestoreAmount = 79;
        public static readonly int ExperienceRestoreAmount = 80;
        
        // General Attribute Percentage
        public static readonly int DexterityBonusPercentage = 90;
        public static readonly int IntelligenceBonusPercentage = 91;
        public static readonly int WisdomBonusPercentage = 92;
        public static readonly int StrengthBonusPercentage = 93;
        public static readonly int ConstitutionBonusPercentage = 94;
        public static readonly int CharismaBonusPercentage = 95;
        public static readonly int HealthBonusPercentage = 96;
        public static readonly int HealthRestorePercentage = 97;
        public static readonly int MagicBonusPercentage = 98;
        public static readonly int MagicRestorePercentage = 99;
        public static readonly int ExperienceRestorePercentage = 100;

        // Melee Damage (Damage Types)
        public static readonly int SlashingDamageAmount = 110;
        public static readonly int PiercingDamageAmount = 111;
        public static readonly int BluntDamageAmount = 112;
        public static readonly int UnarmedDamageAmount = 113;
        public static readonly int SlashingDamagePercentage = 114;
        public static readonly int PiercingDamagePercentage = 115;
        public static readonly int BluntDamagePercentage = 116;
        public static readonly int UnarmedDamagePercentage = 117;

        // Melee Defense (Defense Types)
        public static readonly int SlashingDefenseAmount = 130;
        public static readonly int PiercingDefenseAmount = 131;
        public static readonly int BluntDefenseAmount = 132;
        public static readonly int UnarmedDefenseAmount = 133;
        public static readonly int SlashingDefensePercentage = 134;
        public static readonly int PiercingDefensePercentage = 135;
        public static readonly int BluntDefensePercentage = 136;
        public static readonly int UnarmedDefensePercentage = 137;

        // Melee Type Bonus (Damage/Defense Bonus)
        public static readonly int SlashingBonusAmount = 150;
        public static readonly int PiercingBonusAmount = 151;
        public static readonly int BluntBonusAmount = 152;
        public static readonly int UnarmedBonusAmount = 153;
        public static readonly int SlashingBonusPercentage = 154;
        public static readonly int PiercingBonusPercentage = 155;
        public static readonly int BluntBonusPercentage = 156;
        public static readonly int UnarmedBonusPercentage = 157;
        
        // Magic Damage (Raw Damage)
        public static readonly int FireDamageAmount = 170;
        public static readonly int IceDamageAmount = 171;
        public static readonly int EarthDamageAmount = 172;
        public static readonly int WindDamageAmount = 173;
        public static readonly int LightDamageAmount = 174;
        public static readonly int DarkDamageAmount = 175;
        public static readonly int FireDamagePercentage = 176;
        public static readonly int IceDamagePercentage = 177;
        public static readonly int EarthDamagePercentage = 178;
        public static readonly int WindDamagePercentage = 179;
        public static readonly int LightDamagePercentage = 180;
        public static readonly int DarkDamagePercentage = 181;

        // Magic Defense (Defense Bonus)
        public static readonly int FireDefenseAmount = 200;
        public static readonly int IceDefenseAmount = 201;
        public static readonly int EarthDefenseAmount = 202;
        public static readonly int WindDefenseAmount = 204;
        public static readonly int LightDefenseAmount = 205;
        public static readonly int DarkDefenseAmount = 206;
        public static readonly int FireDefensePercentage = 207;
        public static readonly int IceDefensePercentage = 208;
        public static readonly int EarthDefensePercentage = 209;
        public static readonly int WindDefensePercentage = 210;
        public static readonly int LightDefensePercentage = 211;
        public static readonly int DarkDefensePercentage = 212;
        
        // Magic Bonus (Damage Bonus)
        public static readonly int FireBonusAmount = 230;
        public static readonly int IceBonusAmount = 231;
        public static readonly int EarthBonusAmount = 232;
        public static readonly int WindBonusAmount = 233;
        public static readonly int LightBonusAmount = 234;
        public static readonly int DarkBonusAmount = 235;
        public static readonly int FireBonusPercentage = 236;
        public static readonly int IceBonusPercentage = 237;
        public static readonly int EarthBonusPercentage = 238;
        public static readonly int WindBonusPercentage = 239;
        public static readonly int LightBonusPercentage = 240;
        public static readonly int DarkBonusPercentage = 241;
    }
}