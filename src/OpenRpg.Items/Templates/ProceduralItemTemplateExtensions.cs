using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;

namespace OpenRpg.Items.Templates
{
    public static class ProceduralItemTemplateExtensions
    {
        public static Association AssociateEffect(ItemTemplate template, IRandomizer randomizer, GroupedEffect effect)
        {
            var effectValue = effect.PotencyFunction.InputScale.Random(randomizer);
            var effectIndex = template.Variables.ProceduralEffects().Effects.IndexOf(effect);
            return new Association(effectIndex, (int)effectValue);
        }
        
        public static ItemData GenerateItem(this ItemTemplate template, IRandomizer randomizer)
        {
            if (!template.Variables.HasProceduralEffects())
            { return new ItemData() { TemplateId = template.Id }; }
            
            var takenEffects = new List<Association>();
            var effectsToTake = template.Variables.ProceduralEffects().EffectAmount.Random(randomizer);

            var primaryEffects = template.Variables.ProceduralEffects().Effects
                .Where(x => x.GroupType == CoreProceduralGroupTypes.Primary)
                .ToArray();

            if (primaryEffects.Any())
            {
                var primaryEffect = primaryEffects.TakeRandom(randomizer);
                takenEffects.Add(AssociateEffect(template, randomizer, primaryEffect));
            }

            var secondaryEffects = template.Variables.ProceduralEffects().Effects
                .Where(x => x.GroupType == CoreProceduralGroupTypes.Optional)
                .TakeRandom(effectsToTake, randomizer)
                .Select(x => AssociateEffect(template, randomizer, x));

            takenEffects.AddRange(secondaryEffects);

            var itemData = new ItemData
            {
                TemplateId = template.Id
            };
            itemData.Variables.ProceduralAssociation(takenEffects);
            return itemData;
        }
    }
}