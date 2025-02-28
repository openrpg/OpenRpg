using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class EffectExtensions
    {
        public static IEnumerable<IEffect> GetEffects(this ItemData itemData, ITemplateAccessor templateAccessor)
        {
            var template = templateAccessor.GetItemTemplate(itemData.TemplateId);
            
            var effects = new List<IEffect>(template.Effects);
            if (itemData.Variables.HasProceduralAssociation())
            {
                var proceduralEffects = template.Variables.ProceduralEffects();
                foreach (var proceduralEffect in itemData.Variables.ProceduralAssociation())
                {
                    var effect = proceduralEffects.Effects[proceduralEffect.AssociatedId];
                    if (effect.ScalingType == CoreEffectScalingTypes.Value)
                    {
                        var computedEffect = effect.Compute(proceduralEffect.AssociatedValue);
                        effects.Add(computedEffect);
                    }
                    else
                    { effects.Add(effect); }
                }
            }
            
            foreach (var modification in itemData.Modifications)
            {
                var modEffects = modification.GetEffects(templateAccessor);
                effects.AddRange(modEffects);
            }

            return effects;
        }
        
        public static IEnumerable<IEffect> GetEffects(this ItemModificationData modificationData, ITemplateAccessor templateAccessor)
        {
            var template = templateAccessor.GetItemModificationTemplate(modificationData.TemplateId);
            return template.Effects;
        }

        public static IEnumerable<IEffect> GetEffects(this Equipment equipment, ITemplateAccessor templateAccessor)
        {
            return equipment.Slots.Values
                .Where(x => x != null)
                .SelectMany(x => x.GetEffects(templateAccessor));
        }
    }
}