using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Effects
{
    public static class EffectRelationships
    {
        public static EffectRelationship IceDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.IceDamageAmount,
            PercentageType = FantasyEffectTypes.IceDamagePercentage,
            BonusAmountType = FantasyEffectTypes.IceBonusAmount,
            BonusPercentageType = FantasyEffectTypes.IceBonusPercentage
        };
        
        public static EffectRelationship IceDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.IceDefenseAmount,
            PercentageType = FantasyEffectTypes.IceDefensePercentage,
            BonusAmountType = FantasyEffectTypes.IceBonusAmount,
            BonusPercentageType = FantasyEffectTypes.IceBonusPercentage
        };
        
        public static EffectRelationship FireDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.FireDamageAmount,
            PercentageType = FantasyEffectTypes.FireDamagePercentage,
            BonusAmountType = FantasyEffectTypes.FireBonusAmount,
            BonusPercentageType = FantasyEffectTypes.FireBonusPercentage
        };
        
        public static EffectRelationship FireDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.FireDefenseAmount,
            PercentageType = FantasyEffectTypes.FireDefensePercentage,
            BonusAmountType = FantasyEffectTypes.FireBonusAmount,
            BonusPercentageType = FantasyEffectTypes.FireBonusPercentage
        };
        
        public static EffectRelationship WindDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.WindDamageAmount,
            PercentageType = FantasyEffectTypes.WindDamagePercentage,
            BonusAmountType = FantasyEffectTypes.WindBonusAmount,
            BonusPercentageType = FantasyEffectTypes.WindBonusPercentage
        };
        
        public static EffectRelationship WindDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.WindDefenseAmount,
            PercentageType = FantasyEffectTypes.WindDefensePercentage,
            BonusAmountType = FantasyEffectTypes.WindBonusAmount,
            BonusPercentageType = FantasyEffectTypes.WindBonusPercentage
        };
        
        public static EffectRelationship EarthDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.EarthDamageAmount,
            PercentageType = FantasyEffectTypes.EarthDamagePercentage,
            BonusAmountType = FantasyEffectTypes.EarthBonusAmount,
            BonusPercentageType = FantasyEffectTypes.EarthBonusPercentage
        };
        
        public static EffectRelationship EarthDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.EarthDefenseAmount,
            PercentageType = FantasyEffectTypes.EarthDefensePercentage,
            BonusAmountType = FantasyEffectTypes.EarthBonusAmount,
            BonusPercentageType = FantasyEffectTypes.EarthBonusPercentage
        };
        
        public static EffectRelationship DarkDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.DarkDamageAmount,
            PercentageType = FantasyEffectTypes.DarkDamagePercentage,
            BonusAmountType = FantasyEffectTypes.DarkBonusAmount,
            BonusPercentageType = FantasyEffectTypes.DarkBonusPercentage
        };
        
        public static EffectRelationship DarkDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.DarkDefenseAmount,
            PercentageType = FantasyEffectTypes.DarkDefensePercentage,
            BonusAmountType = FantasyEffectTypes.DarkBonusAmount,
            BonusPercentageType = FantasyEffectTypes.DarkBonusPercentage
        };
        
        public static EffectRelationship LightDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.LightDamageAmount,
            PercentageType = FantasyEffectTypes.LightDamagePercentage,
            BonusAmountType = FantasyEffectTypes.LightBonusAmount,
            BonusPercentageType = FantasyEffectTypes.LightBonusPercentage
        };
        
        public static EffectRelationship LightDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.LightDefenseAmount,
            PercentageType = FantasyEffectTypes.LightDefensePercentage,
            BonusAmountType = FantasyEffectTypes.LightBonusAmount,
            BonusPercentageType = FantasyEffectTypes.LightBonusPercentage
        };
        
        public static EffectRelationship SlashingDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.SlashingDamageAmount,
            PercentageType = FantasyEffectTypes.SlashingDamagePercentage,
            BonusAmountType = FantasyEffectTypes.SlashingBonusAmount,
            BonusPercentageType = FantasyEffectTypes.SlashingBonusPercentage
        };
        
        public static EffectRelationship SlashingDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.SlashingDefenseAmount,
            PercentageType = FantasyEffectTypes.SlashingDefensePercentage,
            BonusAmountType = FantasyEffectTypes.SlashingBonusAmount,
            BonusPercentageType = FantasyEffectTypes.SlashingBonusPercentage
        };
        
        public static EffectRelationship BluntDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.BluntDamageAmount,
            PercentageType = FantasyEffectTypes.BluntDamagePercentage,
            BonusAmountType = FantasyEffectTypes.BluntBonusAmount,
            BonusPercentageType = FantasyEffectTypes.BluntBonusPercentage
        };
        
        public static EffectRelationship BluntDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.BluntDefenseAmount,
            PercentageType = FantasyEffectTypes.BluntDefensePercentage,
            BonusAmountType = FantasyEffectTypes.BluntBonusAmount,
            BonusPercentageType = FantasyEffectTypes.BluntBonusPercentage
        };
        
        public static EffectRelationship PiercingDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.PiercingDamageAmount,
            PercentageType = FantasyEffectTypes.PiercingDamagePercentage,
            BonusAmountType = FantasyEffectTypes.PiercingBonusAmount,
            BonusPercentageType = FantasyEffectTypes.PiercingBonusPercentage
        };
        
        public static EffectRelationship PiercingDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.PiercingDefenseAmount,
            PercentageType = FantasyEffectTypes.PiercingDefensePercentage,
            BonusAmountType = FantasyEffectTypes.PiercingBonusAmount,
            BonusPercentageType = FantasyEffectTypes.PiercingBonusPercentage
        };
        
        public static EffectRelationship UnarmedDamageRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.UnarmedDamageAmount,
            PercentageType = FantasyEffectTypes.UnarmedDamagePercentage,
            BonusAmountType = FantasyEffectTypes.UnarmedBonusAmount,
            BonusPercentageType = FantasyEffectTypes.UnarmedBonusPercentage
        };
        
        public static EffectRelationship UnarmedDefenseRelationship => new EffectRelationship
        {
            AmountType = FantasyEffectTypes.UnarmedDefenseAmount,
            PercentageType = FantasyEffectTypes.UnarmedDefensePercentage,
            BonusAmountType = FantasyEffectTypes.UnarmedBonusAmount,
            BonusPercentageType = FantasyEffectTypes.UnarmedBonusPercentage
        };
    }
}