using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Races.Templates;
using OpenRpg.Core.Templates;

namespace OpenRpg.Core.Extensions
{
    public static class ITemplateAccessorExtensions
    {
        public static ClassTemplate GetClassTemplate(this ITemplateAccessor templateAccessor, int classTemplateId)
        { return templateAccessor.Get<ClassTemplate>(classTemplateId); }
        
        public static RaceTemplate GetRaceTemplate(this ITemplateAccessor templateAccessor, int raceTemplateId)
        { return templateAccessor.Get<RaceTemplate>(raceTemplateId); }
        
        public static ClassView ToView(this ITemplateAccessor templateAccessor, Class classData)
        {
            var template = templateAccessor.Get<ClassTemplate>(classData.TemplateId);
            return new ClassView() { Instance = classData, Template = template };
        }
    }
}