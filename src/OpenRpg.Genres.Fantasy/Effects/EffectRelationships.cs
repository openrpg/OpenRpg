using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Effects
{
    public static class EffectRelationships
    {
        public static EffectRelationship IceDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.IceDamageAmount,
            PercentageType = EffectTypes.IceDamagePercentage,
            BonusAmountType = EffectTypes.IceBonusAmount,
            BonusPercentageType = EffectTypes.IceBonusPercentage
        };
        
        public static EffectRelationship IceDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.IceDefenseAmount,
            PercentageType = EffectTypes.IceDefensePercentage,
            BonusAmountType = EffectTypes.IceBonusAmount,
            BonusPercentageType = EffectTypes.IceBonusPercentage
        };
        
        public static EffectRelationship FireDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.FireDamageAmount,
            PercentageType = EffectTypes.FireDamagePercentage,
            BonusAmountType = EffectTypes.FireBonusAmount,
            BonusPercentageType = EffectTypes.FireBonusPercentage
        };
        
        public static EffectRelationship FireDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.FireDefenseAmount,
            PercentageType = EffectTypes.FireDefensePercentage,
            BonusAmountType = EffectTypes.FireBonusAmount,
            BonusPercentageType = EffectTypes.FireBonusPercentage
        };
        
        public static EffectRelationship WindDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.WindDamageAmount,
            PercentageType = EffectTypes.WindDamagePercentage,
            BonusAmountType = EffectTypes.WindBonusAmount,
            BonusPercentageType = EffectTypes.WindBonusPercentage
        };
        
        public static EffectRelationship WindDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.WindDefenseAmount,
            PercentageType = EffectTypes.WindDefensePercentage,
            BonusAmountType = EffectTypes.WindBonusAmount,
            BonusPercentageType = EffectTypes.WindBonusPercentage
        };
        
        public static EffectRelationship EarthDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.EarthDamageAmount,
            PercentageType = EffectTypes.EarthDamagePercentage,
            BonusAmountType = EffectTypes.EarthBonusAmount,
            BonusPercentageType = EffectTypes.EarthBonusPercentage
        };
        
        public static EffectRelationship EarthDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.EarthDefenseAmount,
            PercentageType = EffectTypes.EarthDefensePercentage,
            BonusAmountType = EffectTypes.EarthBonusAmount,
            BonusPercentageType = EffectTypes.EarthBonusPercentage
        };
        
        public static EffectRelationship DarkDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.DarkDamageAmount,
            PercentageType = EffectTypes.DarkDamagePercentage,
            BonusAmountType = EffectTypes.DarkBonusAmount,
            BonusPercentageType = EffectTypes.DarkBonusPercentage
        };
        
        public static EffectRelationship DarkDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.DarkDefenseAmount,
            PercentageType = EffectTypes.DarkDefensePercentage,
            BonusAmountType = EffectTypes.DarkBonusAmount,
            BonusPercentageType = EffectTypes.DarkBonusPercentage
        };
        
        public static EffectRelationship LightDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.LightDamageAmount,
            PercentageType = EffectTypes.LightDamagePercentage,
            BonusAmountType = EffectTypes.LightBonusAmount,
            BonusPercentageType = EffectTypes.LightBonusPercentage
        };
        
        public static EffectRelationship LightDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.LightDefenseAmount,
            PercentageType = EffectTypes.LightDefensePercentage,
            BonusAmountType = EffectTypes.LightBonusAmount,
            BonusPercentageType = EffectTypes.LightBonusPercentage
        };
        
        public static EffectRelationship SlashingDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.SlashingDamageAmount,
            PercentageType = EffectTypes.SlashingDamagePercentage,
            BonusAmountType = EffectTypes.SlashingBonusAmount,
            BonusPercentageType = EffectTypes.SlashingBonusPercentage
        };
        
        public static EffectRelationship SlashingDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.SlashingDefenseAmount,
            PercentageType = EffectTypes.SlashingDefensePercentage,
            BonusAmountType = EffectTypes.SlashingBonusAmount,
            BonusPercentageType = EffectTypes.SlashingBonusPercentage
        };
        
        public static EffectRelationship BluntDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.BluntDamageAmount,
            PercentageType = EffectTypes.BluntDamagePercentage,
            BonusAmountType = EffectTypes.BluntBonusAmount,
            BonusPercentageType = EffectTypes.BluntBonusPercentage
        };
        
        public static EffectRelationship BluntDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.BluntDefenseAmount,
            PercentageType = EffectTypes.BluntDefensePercentage,
            BonusAmountType = EffectTypes.BluntBonusAmount,
            BonusPercentageType = EffectTypes.BluntBonusPercentage
        };
        
        public static EffectRelationship PiercingDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.PiercingDamageAmount,
            PercentageType = EffectTypes.PiercingDamagePercentage,
            BonusAmountType = EffectTypes.PiercingBonusAmount,
            BonusPercentageType = EffectTypes.PiercingBonusPercentage
        };
        
        public static EffectRelationship PiercingDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.PiercingDefenseAmount,
            PercentageType = EffectTypes.PiercingDefensePercentage,
            BonusAmountType = EffectTypes.PiercingBonusAmount,
            BonusPercentageType = EffectTypes.PiercingBonusPercentage
        };
        
        public static EffectRelationship UnarmedDamageRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.UnarmedDamageAmount,
            PercentageType = EffectTypes.UnarmedDamagePercentage,
            BonusAmountType = EffectTypes.UnarmedBonusAmount,
            BonusPercentageType = EffectTypes.UnarmedBonusPercentage
        };
        
        public static EffectRelationship UnarmedDefenseRelationship => new EffectRelationship
        {
            AmountType = EffectTypes.UnarmedDefenseAmount,
            PercentageType = EffectTypes.UnarmedDefensePercentage,
            BonusAmountType = EffectTypes.UnarmedBonusAmount,
            BonusPercentageType = EffectTypes.UnarmedBonusPercentage
        };
    }
}