using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Effects;

namespace OpenRpg.Combat.Extensions
{
    public static class ActiveEffectsExtensions
    {
        public static bool IsPassiveEffect(this ActiveEffect activeEffect)
        { return activeEffect.Effect.Frequency == 0; }
        
        public static float GetStackedPotency(this ActiveEffect activeEffect)
        {
            var stacks = activeEffect.Stacks > 0 ? activeEffect.Stacks : 1;
            return activeEffect.Effect.Potency * stacks;
        }
        
        public static int TicksSoFar(this ActiveEffect activeEffect)
        { return (int)(activeEffect.ActiveTime / activeEffect.Effect.Frequency); }
        
        public static float SumPotencyFor(this IEnumerable<ActiveEffect> activeEffects, int effectType)
        {
            return activeEffects
                .Where(x => x.Effect.EffectType == effectType)
                .Sum(x => x.GetStackedPotency());
        }
        
        public static bool AttemptToStackEffectFor(this IEnumerable<ActiveEffect> activeEffects, TimedEffect timedEffect)
        {
            if(timedEffect.MaxStack == 0) { return false; }
            var existingEffect = activeEffects.SingleOrDefault(x => x.Effect.Id == timedEffect.Id);
            if (existingEffect == null) { return false; }
            if (existingEffect.Stacks >= existingEffect.Effect.MaxStack) { return false; }
            
            existingEffect.Stacks++;
            existingEffect.ActiveTime = 0;
            return true;
        }
    }
}