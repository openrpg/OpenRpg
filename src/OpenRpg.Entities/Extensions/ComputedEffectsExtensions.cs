using System.Linq;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ComputedEffectsExtensions
    {
        public static void Add(this ComputedEffects computedEffects, int effectType, float potency)
        {
            if (!computedEffects.EffectResults.ContainsKey(effectType))
            {
                computedEffects.EffectResults.Add(effectType, potency);
                return;
            }

            computedEffects.EffectResults[effectType] += potency;
        }

        public static void AddDeferred(this ComputedEffects computedEffects, ScaledEffect effect, IHasEffects context)
        { computedEffects.DeferredEffects.Add(new DeferredEffect() { ScaledEffect = effect, Context = context }); }

        public static float Get(this ComputedEffects computedEffects, int effectType)
        {
            if (!computedEffects.EffectResults.ContainsKey(effectType))
            { return 0; }
            
            return computedEffects.EffectResults[effectType];
        }
        
        public static float Get(this ComputedEffects computedEffects, params int[] effectTypes)
        { return effectTypes.Select(computedEffects.Get).Sum(); }
        
        public static float CalculateTotalValueFor(this ComputedEffects computedEffects, int amountBonusType, int percentageBonusType, int miscBonus = 0)
        {
            var totalAmount = computedEffects.Get(amountBonusType) + miscBonus;
            if(totalAmount == 0) { return 0; }
            
            var percentageBonus = computedEffects.Get(percentageBonusType);
            var totalBonus = totalAmount * percentageBonus;
            return totalAmount + totalBonus;
        }
        
        public static float CalculateTotalValueFor(this ComputedEffects computedEffects, int[] amountBonusType, int[] percentageBonusType, int miscBonus = 0)
        {
            var totalAmount = computedEffects.Get(amountBonusType) + miscBonus;
            if(totalAmount == 0) { return 0; }
            
            var percentageBonus = computedEffects.Get(percentageBonusType);
            var totalBonus = totalAmount * percentageBonus;
            return totalAmount + totalBonus;
        }

        public static void ComputeDeferredEffects(this ComputedEffects computedEffects, Entity.Entity relatedEntity)
        {
            foreach (var deferredEffect in computedEffects.DeferredEffects)
            {
                var result = deferredEffect.ComputeDeferredEffect(relatedEntity);
                computedEffects.Add(result.EffectType, result.Potency);
            }
        }
        
        public static (int EffectType, float Potency) ComputeDeferredEffect(this DeferredEffect deferredEffect, Entity.Entity relatedEntity)
        {
            float relatedValue = 0;
            if (deferredEffect.ScaledEffect.ScalingType == CoreEffectScalingTypes.StateIndex)
            { relatedValue = relatedEntity.State.Get(deferredEffect.ScaledEffect.ScalingIndex); }
            if (deferredEffect.ScaledEffect.ScalingType == CoreEffectScalingTypes.StatIndex)
            { relatedValue = relatedEntity.Stats.Get(deferredEffect.ScaledEffect.ScalingIndex); }

            var potency= deferredEffect.ScaledEffect.PotencyFunction.Plot(relatedValue);
            return (deferredEffect.ScaledEffect.EffectType, potency);
        }

        public static void ProcessDeferredEffects(this ComputedEffects computedEffects, Entity.Entity relatedEntity)
        {
            foreach (var deferredEffect in computedEffects.DeferredEffects)
            {
                var deferredResult = deferredEffect.ComputeDeferredEffect(relatedEntity);
                computedEffects.Add(deferredResult.EffectType, deferredResult.Potency);
            }
            computedEffects.DeferredEffects.Clear();
        }
    }
}