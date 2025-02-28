using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;
using Range = OpenRpg.Core.Utils.Range;

namespace OpenRpg.Items.Templates
{
    public static class ProceduralItemTemplateExtensions
    {
        public static Association AssociateEffect(this ProceduralItemTemplate template, IRandomizer randomizer,
            ProceduralEffect effect)
        {
            var effectValue = effect.PotencyFunction.InputScale.Random(randomizer);
            var effectIndex = template.ProceduralEffects.Effects.IndexOf(effect);
            return new Association(effectIndex, (int)effectValue);
        }
        
        public static ProceduralItemData GenerateItem(this ProceduralItemTemplate template, IRandomizer randomizer)
        {
            var takenEffects = new List<Association>();
            var effectsToTake = template.ProceduralEffects.EffectAmount.Random(randomizer);

            var primaryEffects = template.ProceduralEffects.Effects
                .Where(x => x.GroupType == ProceduralGroupTypes.Primary)
                .ToArray();

            if (primaryEffects.Any())
            {
                var primaryEffect = primaryEffects.TakeRandom(randomizer);
                takenEffects.Add(AssociateEffect(template, randomizer, primaryEffect));
            }

            var secondaryEffects = template.ProceduralEffects.Effects
                .Where(x => x.GroupType == ProceduralGroupTypes.Optional)
                .TakeRandom(effectsToTake, randomizer)
                .Select(x => AssociateEffect(template, randomizer, x));

            takenEffects.AddRange(secondaryEffects);

            return new ProceduralItemData
            {
                TemplateId = template.Id,
                ProceduralEffectAssociations = takenEffects
            };
        }
    }
    
    public interface ProceduralGroupTypes
    {
        public static readonly int Optional = 0;
        public static readonly int Primary = 1;
    }
    
    public class ProceduralEffects
    {
        public Range EffectAmount { get; set; }
        public IReadOnlyList<ProceduralEffect> Effects { get; set; } = Array.Empty<ProceduralEffect>();
    }

    public class ProceduralEffect : ScaledEffect
    {
        public int GroupType { get; set; } = 0;
    }
    
    public class ProceduralItemTemplate : ItemTemplate
    {
        public ProceduralEffects ProceduralEffects { get; set; } = new ProceduralEffects();
    }
}