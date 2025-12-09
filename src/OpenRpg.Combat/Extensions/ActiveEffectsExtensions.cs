using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Extensions
{
    public static class ActiveEffectsExtensions
    {
        extension(ActiveEffect effect)
        {
            public bool IsPassiveEffect => effect.StaticEffect.IsPassiveEffect;
            public float StackedPotency => GetStackedPotency(effect);
            public int TicksSoFar => (int)(effect.ActiveTime / effect.StaticEffect.Frequency);
        }

        extension(TimedStaticEffect effect)
        {
            public bool IsPassiveEffect => effect.Frequency == 0;
        }
        
        public static float GetStackedPotency(ActiveEffect activeEffect)
        {
            var stacks = activeEffect.Stacks > 0 ? activeEffect.Stacks : 1;
            return activeEffect.StaticEffect.Potency * stacks;
        }
        
        public static StaticEffect ToEffect(this ActiveEffect activeEffect)
        { 
            return new StaticEffect()
            {
                EffectType = activeEffect.StaticEffect.EffectType,
                Potency = activeEffect.StackedPotency,
                Requirements = activeEffect.StaticEffect.Requirements
            }; 
        }
    }
}