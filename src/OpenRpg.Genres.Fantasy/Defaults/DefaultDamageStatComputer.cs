using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDamageStatComputer : IDamageStatComputer
    {
        public float ComputeDamage(float statModifier, EffectRelationship damageRelationship, ICollection<Effect> effects)
        {
            var totalDamage = effects.CalculateTotal(damageRelationship);
            if(totalDamage == 0) { return 0; }

            var modifierBonus = totalDamage * statModifier;
            return totalDamage + modifierBonus;
        }
        
        public float ComputeIceDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.IceDamageRelationship, effects); }
        
        public float ComputeFireDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.FireDamageRelationship, effects); }
        
        public float ComputeWindDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.WindDamageRelationship, effects); }
        
        public float ComputeEarthDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.EarthDamageRelationship, effects); }
        
        public float ComputeLightDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.LightDamageRelationship, effects); }
        
        public float ComputeDarkDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Intelligence / 100, EffectRelationships.DarkDamageRelationship, effects); }

        public float ComputeSlashingDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength / 200;
            var dexterityBonus = baseAttributeStats.Dexterity / 200;
            return ComputeDamage(strengthBonus + dexterityBonus, EffectRelationships.SlashingDamageRelationship, effects);
        }
        
        public float ComputeBluntDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Strength / 100, EffectRelationships.BluntDamageRelationship, effects); }
        
        public float ComputePiercingDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseAttributeStats.Dexterity / 100, EffectRelationships.PiercingDamageRelationship, effects); }
        
        public float ComputeUnarmedDamage(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength / 200;
            var dexterityBonus = baseAttributeStats.Dexterity / 200;
            return ComputeDamage(strengthBonus + dexterityBonus, EffectRelationships.UnarmedDamageRelationship, effects);
        }
        
        public IDamageStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var iceDamage = ComputeIceDamage(baseAttributeStats, effects);
            var fireDamage = ComputeFireDamage(baseAttributeStats, effects);
            var windDamage = ComputeWindDamage(baseAttributeStats, effects);
            var earthDamage = ComputeEarthDamage(baseAttributeStats, effects);
            var lightDamage = ComputeLightDamage(baseAttributeStats, effects);
            var darkDamage = ComputeDarkDamage(baseAttributeStats, effects);
            var slashingDamage = ComputeSlashingDamage(baseAttributeStats, effects);
            var bluntDamage = ComputeBluntDamage(baseAttributeStats, effects);
            var piercingDamage = ComputePiercingDamage(baseAttributeStats, effects);
            var unarmedDamage = ComputeUnarmedDamage(baseAttributeStats, effects);

            return new DamageStats
            {
                SlashingDamage = slashingDamage,
                BluntDamage = bluntDamage,
                PiercingDamage = piercingDamage,
                UnarmedDamage = unarmedDamage,
                IceDamage = iceDamage,
                FireDamage = fireDamage,
                WindDamage = windDamage,
                EarthDamage = earthDamage,
                LightDamage = lightDamage,
                DarkDamage = darkDamage
            };
        }
    }
}