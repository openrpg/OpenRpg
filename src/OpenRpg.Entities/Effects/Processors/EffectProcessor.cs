using OpenRpg.Core.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Requirements;
using OpenRpg.Entities.Types;

using BaseEntity = OpenRpg.Entities.Entity.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public class EffectProcessor<T> : IEffectProcessor<T> where T : BaseEntity
    {
        public ITemplateAccessor TemplateAccessor { get; }
        public IEntityRequirementChecker<T> RequirementChecker { get; }

        public EffectProcessor(ITemplateAccessor templateAccessor, IEntityRequirementChecker<T> requirementChecker)
        {
            TemplateAccessor = templateAccessor;
            RequirementChecker = requirementChecker;
        }

        public virtual ComputedEffects ComputeEffects(IHasEffects context, T relatedEntity)
        {
            var computedEffects = new ComputedEffects();
            ComputeEffects(context, relatedEntity, computedEffects);
            return computedEffects;
        }
        
        public virtual void ComputeEffects(IHasEffects context, T relatedEntity, ComputedEffects computedEffects)
        {
            foreach (var effect in context.Effects)
            {
                if(!RequirementChecker.AreRequirementsMet(relatedEntity, effect.Requirements))
                { continue; }
                
                if(effect is StaticEffect staticEffect) 
                { computedEffects.Add(staticEffect.EffectType, staticEffect.Potency); }

                if (effect is ScaledEffect scaledEffect)
                { ComputeScaledEffect(scaledEffect, context, computedEffects, relatedEntity); }
            }
        }

        public virtual ComputedEffects ComputeEffects(T entity)
        {
            var computedEffects = new ComputedEffects();
            
            if (entity.Variables.HasRace())
            {
                var template = TemplateAccessor.GetRaceTemplate(entity.Variables.Race().TemplateId);
                ComputeEffects(template, entity, computedEffects);
            }
            
            if (entity.Variables.HasClass())
            {
                var template = TemplateAccessor.GetClassTemplate(entity.Variables.Class().TemplateId);
                ComputeEffects(template, entity, computedEffects);
            }

            return computedEffects;
        }

        public virtual void ComputeScaledEffect(ScaledEffect effect, IHasEffects context, ComputedEffects computedEffects, BaseEntity relatedEntity)
        {
            if (effect.ScalingType == CoreEffectScalingTypes.Level)
            {
                var level = relatedEntity?.Variables.Class()?.Variables.Level() ?? 1;
                computedEffects.Add(effect.EffectType, effect.PotencyFunction.Plot(level));
                return;
            }

            if (effect.ScalingType == CoreEffectScalingTypes.StateIndex)
            {
                computedEffects.AddDeferred(effect, context);
                return;
            }

            if (effect.ScalingType == CoreEffectScalingTypes.StatIndex)
            {
                computedEffects.AddDeferred(effect, context);
                return;
            }
            
            computedEffects.Add(effect.EffectType, effect.PotencyFunction.Plot(effect.PotencyFunction.InputScale.Min));
        }
    }
}