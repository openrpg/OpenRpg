using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;

using BaseEntity = OpenRpg.Entities.Entity.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public class EffectProcessor : IEffectProcessor
    {
        public ITemplateAccessor TemplateAccessor { get; }

        public EffectProcessor(ITemplateAccessor templateAccessor)
        {
            TemplateAccessor = templateAccessor;
        }

        public virtual IEnumerable<StaticEffect> ComputeEffects(IHasEffects context, BaseEntity relatedEntity = null)
        {
            foreach (var effect in context.Effects)
            {
                if(effect is StaticEffect staticEffect) 
                { yield return staticEffect; }

                if (effect is ScaledEffect scaledEffect)
                { yield return ComputeScaledEffect(scaledEffect, context, relatedEntity); }
            }
        }

        public virtual IEnumerable<StaticEffect> ComputeEffects(BaseEntity entity)
        {
            var effects = new List<StaticEffect>();
            
            if (entity.Variables.HasRace())
            {
                var template = TemplateAccessor.GetRaceTemplate(entity.Variables.Race().TemplateId);
                var computedEffects = ComputeEffects(template, entity);
                effects.AddRange(computedEffects);
            }
            
            if (entity.Variables.HasClass())
            {
                var template = TemplateAccessor.GetClassTemplate(entity.Variables.Class().TemplateId);
                var computedEffects = ComputeEffects(template, entity);
                effects.AddRange(computedEffects);
            }

            return effects;
        }

        public virtual StaticEffect ComputeScaledEffect(ScaledEffect effect, IHasEffects context, BaseEntity relatedEntity = null)
        {
            if (relatedEntity == null)
            { return effect.Compute(1); }
            
            if (effect.ScalingType == CoreEffectScalingTypes.Level)
            {
                var level = relatedEntity?.Variables.Class()?.Variables.Level() ?? 1;
                return effect.Compute(level);
            }
            if (effect.ScalingType == CoreEffectScalingTypes.StateIndex)
            {
                var stateValue = relatedEntity?.State.Get(effect.ScalingIndex) ?? 1;
                return effect.Compute(stateValue);
            }
            if (effect.ScalingType == CoreEffectScalingTypes.StatIndex)
            {
                var statValue = relatedEntity?.Stats.Get(effect.ScalingIndex) ?? 1;
                return effect.Compute(statValue);
            }
            
            return effect.Compute(1); 
        }
    }
}