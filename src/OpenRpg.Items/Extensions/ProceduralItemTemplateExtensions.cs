using OpenRpg.Core.Utils;
using OpenRpg.Entities.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class ProceduralItemTemplateExtensions
    {
        public static ItemData GenerateProceduralInstance(this ItemTemplate template, IRandomizer randomizer)
        {
            if (!template.Variables.HasProceduralEffects())
            { return new ItemData() { TemplateId = template.Id }; }

            var takenEffects = template.Variables.ProceduralEffects().GenerateProceduralEffectAssociations(randomizer);
            var itemData = new ItemData { TemplateId = template.Id };
            itemData.Variables.ProceduralAssociation(takenEffects);
            return itemData;
        }
    }
}