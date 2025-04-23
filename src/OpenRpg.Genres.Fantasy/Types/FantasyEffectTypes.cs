namespace OpenRpg.Genres.Fantasy.Types
{
    public interface FantasyEffectTypes : Genres.Types.GenreEffectTypes
    {
        // Generic Melee/Magic
        public static readonly int AllMeleeAttackBonusAmount = 200;
        public static readonly int AllMeleeDefenseBonusAmount = 201;
        public static readonly int AllElementDamageBonusAmount = 203;
        public static readonly int AllElementDefenseBonusAmount = 204;
        public static readonly int AllMeleeAttackBonusPercentage = 205;
        public static readonly int AllMeleeDefenseBonusPercentage = 206;
        public static readonly int AllElementDamageBonusPercentage = 207;
        public static readonly int AllElementDefenseBonusPercentage = 208;
            
        // General Attribute
        public static readonly int StrengthBonusAmount = 220;
        public static readonly int StrengthBonusPercentage = 221;
        public static readonly int DexterityBonusAmount = 222;
        public static readonly int DexterityBonusPercentage = 223;
        public static readonly int ConstitutionBonusAmount = 224;
        public static readonly int ConstitutionBonusPercentage = 225;
        public static readonly int IntelligenceBonusAmount = 226;
        public static readonly int IntelligenceBonusPercentage = 227;
        public static readonly int WisdomBonusAmount = 228;
        public static readonly int WisdomBonusPercentage = 229;
        public static readonly int CharismaBonusAmount = 230;
        public static readonly int CharismaBonusPercentage = 231;
        public static readonly int ManaRegenBonusAmount = 232;
        public static readonly int ManaRegenBonusPercentage = 233;
        public static readonly int ManaBonusAmount = 234;
        public static readonly int ManaBonusPercentage = 235;
        public static readonly int ManaRestoreAmount = 236;
        public static readonly int ManaRestorePercentage = 237;
        public static readonly int ManaRegenRateBonusAmount = 238;
        public static readonly int ManaRegenRateBonusPercentage = 239;

        // Melee Damage (Damage Types)
        public static readonly int SlashingDamageAmount = 260;
        public static readonly int PiercingDamageAmount = 261;
        public static readonly int BluntDamageAmount = 262;
        public static readonly int UnarmedDamageAmount = 263;
        public static readonly int SlashingDamagePercentage = 264;
        public static readonly int PiercingDamagePercentage = 265;
        public static readonly int BluntDamagePercentage = 266;
        public static readonly int UnarmedDamagePercentage = 267;

        // Melee Defense (Defense Types)
        public static readonly int SlashingDefenseAmount = 290;
        public static readonly int PiercingDefenseAmount = 291;
        public static readonly int BluntDefenseAmount = 292;
        public static readonly int UnarmedDefenseAmount = 293;
        public static readonly int SlashingDefensePercentage = 294;
        public static readonly int PiercingDefensePercentage = 295;
        public static readonly int BluntDefensePercentage = 296;
        public static readonly int UnarmedDefensePercentage = 297;

        // Melee Type Bonus (Damage/Defense Bonus)
        public static readonly int SlashingBonusAmount = 320;
        public static readonly int PiercingBonusAmount = 321;
        public static readonly int BluntBonusAmount = 322;
        public static readonly int UnarmedBonusAmount = 323;
        public static readonly int SlashingBonusPercentage = 324;
        public static readonly int PiercingBonusPercentage = 325;
        public static readonly int BluntBonusPercentage = 326;
        public static readonly int UnarmedBonusPercentage = 327;
        
        // Magic Damage (Raw Damage)
        public static readonly int FireDamageAmount = 350;
        public static readonly int IceDamageAmount = 351;
        public static readonly int EarthDamageAmount = 352;
        public static readonly int WindDamageAmount = 353;
        public static readonly int LightDamageAmount = 354;
        public static readonly int DarkDamageAmount = 355;
        public static readonly int FireDamagePercentage = 356;
        public static readonly int IceDamagePercentage = 357;
        public static readonly int EarthDamagePercentage = 358;
        public static readonly int WindDamagePercentage = 359;
        public static readonly int LightDamagePercentage = 360;
        public static readonly int DarkDamagePercentage = 361;

        // Magic Defense (Defense Bonus)
        public static readonly int FireDefenseAmount = 390;
        public static readonly int IceDefenseAmount = 391;
        public static readonly int EarthDefenseAmount = 392;
        public static readonly int WindDefenseAmount = 393;
        public static readonly int LightDefenseAmount = 394;
        public static readonly int DarkDefenseAmount = 395;
        public static readonly int FireDefensePercentage = 396;
        public static readonly int IceDefensePercentage = 397;
        public static readonly int EarthDefensePercentage = 398;
        public static readonly int WindDefensePercentage = 399;
        public static readonly int LightDefensePercentage = 400;
        public static readonly int DarkDefensePercentage = 401;
        
        // Magic Bonus (Damage Bonus)
        public static readonly int FireBonusAmount = 430;
        public static readonly int IceBonusAmount = 431;
        public static readonly int EarthBonusAmount = 432;
        public static readonly int WindBonusAmount = 433;
        public static readonly int LightBonusAmount = 434;
        public static readonly int DarkBonusAmount = 435;
        public static readonly int FireBonusPercentage = 436;
        public static readonly int IceBonusPercentage = 437;
        public static readonly int EarthBonusPercentage = 438;
        public static readonly int WindBonusPercentage = 439;
        public static readonly int LightBonusPercentage = 440;
        public static readonly int DarkBonusPercentage = 441;
    }
}