using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity;
using OpenRpg.Entities.Extensions;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Effects
{
    public class GenreEffectProcessor : EffectProcessor
    {
        public GenreEffectProcessor(ITemplateAccessor templateAccessor) : base(templateAccessor)
        {
        }

        public IEnumerable<StaticEffect> ComputeEffects(ItemData itemData, Entity relatedEntity = null)
        {
            var effects = new List<StaticEffect>();
            var template = TemplateAccessor.GetItemTemplate(itemData.TemplateId);
            effects.AddRange(ComputeEffects(template, relatedEntity));

            if (!itemData.Modifications.Any())
            { return effects; }

            foreach (var modification in itemData.Modifications)
            {
                var modificationTemplate = TemplateAccessor.GetModificationTemplate<ItemModificationTemplate>(modification.TemplateId);
                effects.AddRange(ComputeEffects(modificationTemplate, relatedEntity));
            }
            
            return effects;
        }

        public override IEnumerable<StaticEffect> ComputeEffects(Entity entity)
        {
            var effects = new List<StaticEffect>();
            effects.AddRange(base.ComputeEffects(entity));
            
            if (entity.Variables.HasEquipment())
            {
                var equipment = entity.Variables.Equipment();
                var equipmentEffects = equipment.Slots.Values
                    .Where(x => x != null)
                    .SelectMany(x => ComputeEffects(x, entity));
                
                effects.AddRange(equipmentEffects);
            }

            if (entity.Variables.HasActiveEffects())
            {
                var activeEffects = entity.Variables.ActiveEffects().ActiveEffects
                    .Where(x => x.IsPassiveEffect())
                    .Select(x => x.ToEffect());
                
                effects.AddRange(activeEffects);
            }
            
            return effects;
        }
    }
}