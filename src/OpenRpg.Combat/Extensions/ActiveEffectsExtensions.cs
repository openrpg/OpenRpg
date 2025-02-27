using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Combat.Extensions
{
    public static class ActiveEffectsExtensions
    {
        public static bool IsPassiveEffect(this ActiveEffect activeEffect)
        { return IsPassiveEffect(activeEffect.StaticEffect); }
        
        public static bool IsPassiveEffect(this TimedStaticEffect staticEffect)
        { return staticEffect.Frequency == 0; }
        
        public static float GetStackedPotency(this ActiveEffect activeEffect)
        {
            var stacks = activeEffect.Stacks > 0 ? activeEffect.Stacks : 1;
            return activeEffect.StaticEffect.Potency * stacks;
        }
        
        public static int TicksSoFar(this ActiveEffect activeEffect)
        { return (int)(activeEffect.ActiveTime / activeEffect.StaticEffect.Frequency); }
        
        public static StaticEffect ToEffect(this ActiveEffect activeEffect)
        { 
            return new StaticEffect()
            {
                EffectType = activeEffect.StaticEffect.EffectType,
                Potency = activeEffect.GetStackedPotency(),
                Requirements = activeEffect.StaticEffect.Requirements
            }; 
        }
    }
}