using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class CombatExtensions
    {
        public static string TypeToName(this Damage damage)
        {
            if (damage.Type == DamageTypes.IceDamage) return "Ice";
            if (damage.Type == DamageTypes.FireDamage) return "Fire";
            if (damage.Type == DamageTypes.WindDamage) return "Wind";
            if (damage.Type == DamageTypes.EarthDamage) return "Earth";
            if (damage.Type == DamageTypes.DarkDamage) return "Dark";
            if (damage.Type == DamageTypes.LightDamage) return "Light";
            
            if (damage.Type == DamageTypes.SlashingDamage) return "Slashing";
            if (damage.Type == DamageTypes.BluntDamage) return "Blunt";
            if (damage.Type == DamageTypes.PiercingDamage) return "Piercing";
            if (damage.Type == DamageTypes.UnarmedDamage) return "Unarmed";

            return "Unknown";
        }
        
        public static float CalculateTotalAmount(this IEnumerable<Effect> effects, int effectAmountType, int bonusAmountType)
        { return effects.GetPotencyFor(effectAmountType, bonusAmountType); }
        
        public static float CalculateTotalPercentage(this IEnumerable<Effect> effects, int effectPercentageType, int bonusPercentageType)
        { return effects.GetPotencyFor(effectPercentageType, bonusPercentageType); }
        
        public static float CalculateTotal(this ICollection<Effect> effects, int effectAmountType, int effectPercentageType, int bonusAmountType, int bonusPercentageType)
        {
            var baseValue = effects.CalculateTotalAmount(effectAmountType, bonusAmountType);
            if(baseValue == 0) { return 0; }
            
            var percentageBonus = effects.CalculateTotalPercentage(effectPercentageType, bonusPercentageType);
            if(percentageBonus != 0) { percentageBonus /= 100;}

            var percentageValue = baseValue * percentageBonus;
            return baseValue + percentageValue;
        }
        
        public static float CalculateTotal(this ICollection<Effect> effects, EffectRelationship damageRelationship)
        {
            return effects.CalculateTotal(damageRelationship.AmountType, damageRelationship.PercentageType,
                damageRelationship.BonusAmountType, damageRelationship.BonusPercentageType);
        }
    }
}