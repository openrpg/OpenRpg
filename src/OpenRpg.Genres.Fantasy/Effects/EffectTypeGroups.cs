using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Effects
{
    public static class EffectTypeGroups
    {
        public static int[] DamageEffectTypes =
        {
            FantasyEffectTypes.SlashingDamageAmount, FantasyEffectTypes.PiercingDamageAmount, FantasyEffectTypes.BluntDamageAmount, FantasyEffectTypes.UnarmedDamageAmount,
            FantasyEffectTypes.FireDamageAmount, FantasyEffectTypes.IceDamageAmount, FantasyEffectTypes.WindDamageAmount, FantasyEffectTypes.EarthDamageAmount,
            FantasyEffectTypes.LightDamageAmount, FantasyEffectTypes.DarkDamageAmount, FantasyEffectTypes.AllMeleeDefenseBonusAmount, FantasyEffectTypes.AllElementDamageBonusAmount
        };
        
        public static int[] DefenseEffectTypes =
        {
            FantasyEffectTypes.SlashingDefenseAmount, FantasyEffectTypes.PiercingDefenseAmount, FantasyEffectTypes.BluntDefenseAmount, FantasyEffectTypes.UnarmedDefenseAmount,
            FantasyEffectTypes.FireDefenseAmount, FantasyEffectTypes.IceDefenseAmount, FantasyEffectTypes.WindDefenseAmount, FantasyEffectTypes.EarthDefenseAmount,
            FantasyEffectTypes.LightDefenseAmount, FantasyEffectTypes.DarkDefenseAmount, FantasyEffectTypes.AllMeleeDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusAmount
        };
        
        public static int[] AttributeEffectTypes =
        {
            FantasyEffectTypes.StrengthBonusAmount, FantasyEffectTypes.DexterityBonusAmount, FantasyEffectTypes.ConstitutionBonusAmount, 
            FantasyEffectTypes.IntelligenceBonusAmount, FantasyEffectTypes.WisdomBonusAmount, FantasyEffectTypes.CharismaBonusAmount,
            FantasyEffectTypes.AllAttributeBonusAmount
        };
    }
}