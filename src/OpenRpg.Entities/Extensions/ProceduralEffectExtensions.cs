using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Procedural.Effects;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ProceduralEffectExtensions
    {
        public static Association AssociateEffect(ProceduralEffects proceduralEffects, IRandomizer randomizer, GroupedEffect effect)
        {
            var effectIndex = proceduralEffects.Effects.IndexOf(effect);
            if (effect.ScalingType != CoreEffectScalingTypes.Value)
            { return new Association(effectIndex, 0); }
            
            var effectValue = effect.PotencyFunction.InputScale.Random(randomizer);
            return new Association(effectIndex, (int)effectValue);
        }
        
        public static IReadOnlyCollection<Association> GenerateProceduralEffectAssociations(this ProceduralEffects proceduralEffects, IRandomizer randomizer)
        {
            var takenEffects = new List<Association>();
            var effectsToTake = proceduralEffects.EffectAmount.Random(randomizer);

            var primaryEffects = proceduralEffects.Effects
                .Where(x => x.GroupType == CoreProceduralGroupTypes.Primary)
                .ToArray();

            if (primaryEffects.Any())
            {
                var primaryEffect = primaryEffects.TakeRandom(randomizer);
                takenEffects.Add(AssociateEffect(proceduralEffects, randomizer, primaryEffect));
            }

            var secondaryEffects = proceduralEffects.Effects
                .Where(x => x.GroupType == CoreProceduralGroupTypes.Optional)
                .TakeRandom(effectsToTake, randomizer)
                .Select(x => AssociateEffect(proceduralEffects, randomizer, x));

            takenEffects.AddRange(secondaryEffects);
            return takenEffects;
        }
        
        public static IReadOnlyCollection<IEffect> GenerateEffectsFrom(this ProceduralEffects proceduralEffects, int proceduralValue)
        {
            var primaryEffects = proceduralEffects.Effects
                .Where(x => x.GroupType == CoreProceduralGroupTypes.Primary);

            var resultingEffects = new List<IEffect>();
            foreach (var primaryEffect in primaryEffects)
            {
                if(primaryEffect.PotencyFunction.InputScale.IsOutsideRange(proceduralValue)) 
                { continue; }
                
                var effect = new StaticEffect
                {
                    EffectType = primaryEffect.EffectType,
                    Potency = primaryEffect.PotencyFunction.Plot(proceduralValue),
                    Requirements = primaryEffect.Requirements
                };
                resultingEffects.Add(effect);
            }

            return resultingEffects;
        }
    }
}