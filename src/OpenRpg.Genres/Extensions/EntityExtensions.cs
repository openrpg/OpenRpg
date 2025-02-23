using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Entity;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Races;
using OpenRpg.Entities.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityExtensions
    {
        public static StaticEffect ComputeScalingType(this ScaledEffect scaledEffect, Entity relatedEntity = null)
        {
            if (relatedEntity == null)
            { return scaledEffect.Compute(1); }
            
            if (scaledEffect.ScalingType == CoreEffectScalingTypes.Level)
            {
                var level = relatedEntity?.Variables.Class()?.Variables.Level() ?? 1;
                return scaledEffect.Compute(level);
            }
            if (scaledEffect.ScalingType == CoreEffectScalingTypes.StateIndex)
            {
                var stateValue = relatedEntity?.State.Get(scaledEffect.ScalingIndex) ?? 1;
                return scaledEffect.Compute(stateValue);
            }
            if (scaledEffect.ScalingType == CoreEffectScalingTypes.StatIndex)
            {
                var stateValue = relatedEntity?.Stats.Get(scaledEffect.ScalingIndex) ?? 1;
                return scaledEffect.Compute(stateValue);
            }
            
            return scaledEffect.Compute(1); 
        }
        
        public static IEnumerable<StaticEffect> ComputeEffects(this RaceData raceData, ITemplateAccessor templateAccessor, Entity relatedEntity = null)
        {
            var template = templateAccessor.GetRaceTemplate(raceData.TemplateId);
            foreach (var effect in template.Effects)
            {
                if(effect is StaticEffect staticEffect) 
                { yield return staticEffect; }

                if (effect is ScaledEffect scaledEffect)
                { yield return scaledEffect.ComputeScalingType(relatedEntity); }
            }
        }
        
        public static IEnumerable<StaticEffect> ComputeEffects(this ClassData classData, ITemplateAccessor templateAccessor, Entity relatedEntity = null)
        {
            var template = templateAccessor.GetClassTemplate(classData.TemplateId);
            foreach (var effect in template.Effects)
            {
                if(effect is StaticEffect staticEffect) 
                { yield return staticEffect; }

                if (effect is ScaledEffect scaledEffect)
                { yield return scaledEffect.ComputeScalingType(relatedEntity); }
            }
        }
        
        public static IEnumerable<StaticEffect> ComputeEffects(this ItemModificationData modificationData, ITemplateAccessor templateAccessor, Entity relatedEntity)
        {
            var template = templateAccessor.GetModificationTemplate<ItemModificationTemplate>(modificationData.TemplateId);
            foreach (var effect in template.Effects)
            {
                if(effect is StaticEffect staticEffect) 
                { yield return staticEffect; }

                if (effect is ScaledEffect scaledEffect)
                { yield return scaledEffect.ComputeScalingType(relatedEntity); }
            }
        }
        
        public static IEnumerable<StaticEffect> ComputeEffects(this ItemData itemData, ITemplateAccessor templateAccessor, Entity relatedEntity = null)
        {
            var template = templateAccessor.GetItemTemplate(itemData.TemplateId);
            foreach (var effect in template.Effects)
            {
                if(effect is StaticEffect staticEffect) 
                { yield return staticEffect; }

                if (effect is ScaledEffect scaledEffect)
                { yield return scaledEffect.ComputeScalingType(relatedEntity); }
            }

            if (!itemData.Modifications.Any())
            { yield break; }

            foreach (var modification in itemData.Modifications)
            {
                var modificationEffects = modification.ComputeEffects(templateAccessor, relatedEntity);
                foreach(var modificationEffect in modificationEffects)
                { yield return modificationEffect; }
            }
        }
        
        public static IEnumerable<StaticEffect> ComputeEffects(this Equipment equipment, ITemplateAccessor templateAccessor, Entity relatedEntity = null)
        {
            return equipment.Slots.Values
                .Where(x => x != null)
                .SelectMany(x => x.ComputeEffects(templateAccessor, relatedEntity));
        }
        
        /// <summary>
        /// Gets all basic effects from known sources on an entity
        /// </summary>
        /// <param name="entity">The entity to process</param>
        /// <returns>All known effects that are applied to the entity</returns>
        /// <remarks>Multiclass are not included as that's likely to need complex mapping logic</remarks>
        public static IReadOnlyCollection<StaticEffect> ComputeEffects(this Entity entity, ITemplateAccessor templateAccessor, Entity relatedEntity = null)
        {
            var effects = new List<StaticEffect>();

            if (entity.Variables.HasRace())
            {
                var computedEffects = entity.Variables.Race().ComputeEffects(templateAccessor, relatedEntity);
                effects.AddRange(computedEffects);
            }

            if (entity.Variables.HasClass())
            {
                var computedEffects = entity.Variables.Class().ComputeEffects(templateAccessor, relatedEntity);
                effects.AddRange(computedEffects);
            }

            if (entity.Variables.HasEquipment())
            {
                var computedEffects = entity.Variables.Equipment().ComputeEffects(templateAccessor, relatedEntity);
                effects.AddRange(computedEffects);
            }

            if (entity.Variables.HasActiveEffects())
            { effects.AddRange(entity.Variables.ActiveEffects().ActiveEffects.Where(x => x.IsPassiveEffect()).Select(x => x.ToEffect())); }
            
            return effects;
        }
    }
}