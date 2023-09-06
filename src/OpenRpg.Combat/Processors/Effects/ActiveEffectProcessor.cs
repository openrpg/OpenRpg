using System;
using System.Collections.Generic;
using OpenRpg.Combat.Effects;

namespace OpenRpg.Combat.Processors.Effects
{
    public class ActiveEffectProcessor : IActiveEffectProcessor
    {
        public event EventHandler<ActiveEffect> EffectTriggered;
        public event EventHandler<ActiveEffect> EffectExpired;
      
        public void Process(IReadOnlyList<ActiveEffect> activeEffects, float elapsedTime)
        {
            for (var i = activeEffects.Count - 1; i >= 0; i--)
            {
                var activeEffect = activeEffects[i];
                if (activeEffect.ActiveTime >= activeEffect.Effect.Duration)
                {
                    EffectExpired?.Invoke(this, activeEffect);
                    continue;
                }
            
                activeEffect.ActiveTime += elapsedTime;
                activeEffect.TimeSinceTick += elapsedTime;
                if (activeEffect.Effect.Frequency == 0) { continue; }
                
                while (activeEffect.TimeSinceTick >= activeEffect.Effect.Frequency)
                {
                    EffectTriggered?.Invoke(this, activeEffect);
                    activeEffect.TimeSinceTick -= activeEffect.Effect.Frequency;
                }
            }
        }
    }
}