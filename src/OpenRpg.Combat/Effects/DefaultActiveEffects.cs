using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Variables;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Effects
{
    public class DefaultActiveEffects : IActiveEffects
    {
        public event EventHandler<ActiveEffect> EffectAdded;
        public event EventHandler<ActiveEffect> EffectTriggered;
        public event EventHandler<ActiveEffect> EffectExpired;

        public Dictionary<int, ActiveEffect> InternalActiveEffects { get; set; } = new Dictionary<int, ActiveEffect>();
        public IActiveEffectsVariables Variables { get; set; } = new DefaultActiveEffectsVariables();

        public IReadOnlyCollection<ActiveEffect> ActiveEffects => InternalActiveEffects.Values;

        public IEnumerable<Effect> Effects => InternalActiveEffects.Values
                .Where(x => x.IsPassiveEffect())
                .Select(x => x.ToEffect());
        
        public bool AddEffect(TimedEffect effect)
        {
            var hasEffect = InternalActiveEffects.ContainsKey(effect.Id);
            var canBeStacked = effect.MaxStack > 1;
            
            if (hasEffect)
            {
                var activeEffect = InternalActiveEffects[effect.Id];
                activeEffect.ActiveTime = 0;
                
                if (canBeStacked && activeEffect.Stacks < effect.MaxStack)
                { activeEffect.Stacks++; }

                return true;
            }
            
            var newActiveEffect = new ActiveEffect(effect);
            InternalActiveEffects.Add(effect.Id, newActiveEffect); 
            EffectAdded?.Invoke(this, newActiveEffect);
            return true;
        }

        public void UpdateEffects(float elapsedTime)
        {
            var keys = InternalActiveEffects.Keys.ToArray();
            foreach (var key in keys)
            {
                var activeEffect = InternalActiveEffects[key];
           
                if (activeEffect.Effect.Frequency > 0)
                {
                    var elapsedTimeToApply = elapsedTime + activeEffect.ActiveTime <= activeEffect.Effect.Duration 
                        ? elapsedTime 
                        : activeEffect.Effect.Duration - activeEffect.ActiveTime;
                    
                    activeEffect.TimeSinceTick += elapsedTimeToApply;
                    while (activeEffect.TimeSinceTick >= activeEffect.Effect.Frequency)
                    {
                        EffectTriggered?.Invoke(this, activeEffect);
                        activeEffect.TimeSinceTick -= activeEffect.Effect.Frequency;
                    }
                }
                
                activeEffect.ActiveTime += elapsedTime;
                if (activeEffect.ActiveTime >= activeEffect.Effect.Duration)
                {
                    InternalActiveEffects.Remove(key);
                    EffectExpired?.Invoke(this, activeEffect);
                }
            }
        }
        
        public bool RemoveEffect(int timedEffectId)
        {
            if (!InternalActiveEffects.ContainsKey(timedEffectId)) 
            {return false;}
            
            var activeEffect = InternalActiveEffects[timedEffectId];
            InternalActiveEffects.Remove(timedEffectId);
            EffectExpired?.Invoke(this, activeEffect);
            return true;
        }

        public ActiveEffect GetEffect(int timedEffectId)
        { return InternalActiveEffects.TryGetValue(timedEffectId, out var effect) ? effect : null; }

        public bool HasEffect(int timedEffectId)
        { return InternalActiveEffects.ContainsKey(timedEffectId); }
    }
}