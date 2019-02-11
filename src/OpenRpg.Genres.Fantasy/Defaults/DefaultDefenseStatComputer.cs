using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDefenseStatComputer : IDefenseStatComputer
    {
        public float ComputeDefense(float statModifier, EffectRelationship damageRelationship, ICollection<Effect> effects)
        {
            var totalDamage = effects.CalculateTotal(damageRelationship);
            if(totalDamage == 0) { return 0; }

            var modifierBonus = totalDamage * statModifier;
            return totalDamage + modifierBonus;
        }
        
        public float ComputeIceDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.IceDefenseRelationship, effects); }
        
        public float ComputeFireDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.FireDefenseRelationship, effects); }
        
        public float ComputeWindDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.WindDefenseRelationship, effects); }
        
        public float ComputeEarthDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.EarthDefenseRelationship, effects); }
        
        public float ComputeLightDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.LightDefenseRelationship, effects); }
        
        public float ComputeDarkDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence / 100, EffectRelationships.DarkDefenseRelationship, effects); }

        public float ComputeSlashingDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength / 200;
            var dexterityBonus = baseAttributeStats.Dexterity / 200;
            return ComputeDefense(strengthBonus + dexterityBonus, EffectRelationships.SlashingDefenseRelationship, effects);
        }
        
        public float ComputeBluntDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Strength / 100, EffectRelationships.BluntDefenseRelationship, effects); }
        
        public float ComputePiercingDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Dexterity / 100, EffectRelationships.PiercingDefenseRelationship, effects); }
        
        public float ComputeUnarmedDefense(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength / 200;
            var dexterityBonus = baseAttributeStats.Dexterity / 200;
            return ComputeDefense(strengthBonus + dexterityBonus, EffectRelationships.UnarmedDefenseRelationship, effects);
        }
        
        public IDefenseStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var iceDefense = ComputeIceDefense(baseAttributeStats, effects);
            var fireDefense = ComputeFireDefense(baseAttributeStats, effects);
            var windDefense = ComputeWindDefense(baseAttributeStats, effects);
            var earthDefense = ComputeEarthDefense(baseAttributeStats, effects);
            var lightDefense = ComputeLightDefense(baseAttributeStats, effects);
            var darkDefense = ComputeDarkDefense(baseAttributeStats, effects);
            var slashingDefense = ComputeSlashingDefense(baseAttributeStats, effects);
            var bluntDefense = ComputeBluntDefense(baseAttributeStats, effects);
            var piercingDefense = ComputePiercingDefense(baseAttributeStats, effects);
            var unarmedDefense = ComputeUnarmedDefense(baseAttributeStats, effects);

            return new DefenseStats
            {
                SlashingDefense = slashingDefense,
                BluntDefense = bluntDefense,
                PiercingDefense = piercingDefense,
                UnarmedDefense = unarmedDefense,
                IceDefense = iceDefense,
                FireDefense = fireDefense,
                WindDefense = windDefense,
                EarthDefense = earthDefense,
                LightDefense = lightDefense,
                DarkDefense = darkDefense
            };
        }
    }
}