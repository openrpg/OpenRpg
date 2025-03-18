using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Modifications;
using OpenRpg.Entities.Races;

namespace OpenRpg.Entities.Extensions
{
    public static class EffectExtensions
    {
        // TODO: This is a crutch until the IEffect stuff is fully mapped over
        public static IReadOnlyCollection<StaticEffect> GetStaticEffects(this IReadOnlyCollection<IEffect> effects) => 
            effects.Where(x => x is StaticEffect).Cast<StaticEffect>().ToArray();

        public static IEnumerable<IEffect> GetEffects(this RaceData raceData, ITemplateAccessor templateAccessor)
        {
            var template = templateAccessor.GetRaceTemplate(raceData.TemplateId);
            return template.Effects;
        }
        
        public static IEnumerable<IEffect> GetEffects(this ClassData classData, ITemplateAccessor templateAccessor)
        {
            var template = templateAccessor.GetClassTemplate(classData.TemplateId);
            return template.Effects;
        }
    }
}