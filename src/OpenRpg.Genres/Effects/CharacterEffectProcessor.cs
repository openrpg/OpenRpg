using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Requirements;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Effects
{
    public class CharacterEffectProcessor : EffectProcessor<Character>, ICharacterEffectProcessor
    {
        public CharacterEffectProcessor(ITemplateAccessor templateAccessor, ICharacterRequirementChecker requirementChecker) : base(templateAccessor, requirementChecker)
        {
        }
        
        public void ComputeEffects(ItemData itemData, Character relatedEntity, ComputedEffects computedEffects)
        {
            var itemTemplate = TemplateAccessor.GetItemTemplate(itemData.TemplateId);
            ComputeEffects(itemTemplate, relatedEntity, computedEffects);

            if (itemTemplate.Variables.HasProceduralEffects())
            {
                var proceduralEffects = itemTemplate.Variables.ProceduralEffects();
                var associatedEffects = itemData.Variables.ProceduralAssociation();
                ComputeProceduralEffects(proceduralEffects, associatedEffects, itemTemplate, computedEffects, relatedEntity);
            }

            if (!itemData.Modifications.Any())
            { return; }

            foreach (var modification in itemData.Modifications)
            {
                var modificationTemplate = TemplateAccessor.GetModificationTemplate<ItemModificationTemplate>(modification.TemplateId);
                ComputeEffects(modificationTemplate, relatedEntity, computedEffects);
            }
        }

        public override ComputedEffects ComputeEffects(Character entity)
        {
            var computedEffects = base.ComputeEffects(entity);
            
            if (entity.Variables.HasEquipment())
            {
                var equipment = entity.Variables.Equipment();
                var equippedItems = equipment.Slots.Values
                    .Where(x => x != null);

                foreach (var equippedItem in equippedItems)
                { ComputeEffects(equippedItem, entity, computedEffects); }
            }

            if (entity.Variables.HasActiveEffects())
            {
                var activeEffects = entity.Variables.ActiveEffects().ActiveEffects
                    .Where(x => x.IsPassiveEffect())
                    .Select(x => x.ToEffect());
                
                foreach(var effect in activeEffects)
                { computedEffects.Add(effect.EffectType, effect.Potency); }
            }

            return computedEffects;
        }
    }
}