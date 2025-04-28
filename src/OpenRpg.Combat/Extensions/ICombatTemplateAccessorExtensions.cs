using OpenRpg.Combat.Abilities;
using OpenRpg.Core.Templates;

namespace OpenRpg.Combat.Extensions
{
    public static class ICombatTemplateAccessorExtensions
    {
        public static AbilityTemplate GetAbilityTemplate(this ITemplateAccessor templateAccessor, int abilityTemplateId)
        { return templateAccessor.Get<AbilityTemplate>(abilityTemplateId); }
        
        public static Ability ToInstance(this ITemplateAccessor templateAccessor, AbilityData abilityData)
        {
            var template = templateAccessor.Get<AbilityTemplate>(abilityData.TemplateId);
            return new Ability() { Data = abilityData, Template = template };
        }
    }
}