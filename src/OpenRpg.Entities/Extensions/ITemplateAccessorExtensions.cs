using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Modifications;
using OpenRpg.Entities.Races;
using OpenRpg.Entities.Races.Templates;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateAccessorExtensions
    {
        public static ClassTemplate GetClassTemplate(this ITemplateAccessor templateAccessor, int classTemplateId)
        { return templateAccessor.Get<ClassTemplate>(classTemplateId); }
        
        public static RaceTemplate GetRaceTemplate(this ITemplateAccessor templateAccessor, int raceTemplateId)
        { return templateAccessor.Get<RaceTemplate>(raceTemplateId); }
        
        public static Class ToInstance(this ITemplateAccessor templateAccessor, ClassData classDataData)
        {
            var template = templateAccessor.Get<ClassTemplate>(classDataData.TemplateId);
            return new Class() { Data = classDataData, Template = template };
        }
        
        public static IReadOnlyCollection<Class> ToInstance(this ITemplateAccessor templateAccessor, IReadOnlyCollection<ClassData> classData)
        { return classData.Select(x => ToInstance(templateAccessor, x)).ToArray(); }
        
        public static Race ToInstance(this ITemplateAccessor templateAccessor, RaceData raceDataData)
        {
            var template = templateAccessor.Get<RaceTemplate>(raceDataData.TemplateId);
            return new Race() { Data = raceDataData, Template = template };
        }
                
        public static T GetModificationTemplate<T>(this ITemplateAccessor templateAccessor, int modificationTemplateId) where T : ModificationTemplate
        { return templateAccessor.Get<T>(modificationTemplateId); }
    }
}