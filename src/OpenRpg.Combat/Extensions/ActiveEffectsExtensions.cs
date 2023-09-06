using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;

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
        
        public static Effect ToEffect(this ActiveEffect activeEffect)
        { 
            return new Effect()
            {
                EffectType = activeEffect.Effect.EffectType,
                Potency = activeEffect.GetStackedPotency(),
                Requirements = activeEffect.Effect.Requirements
            }; 
        }
    }
}