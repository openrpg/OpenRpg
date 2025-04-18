using OpenRpg.Combat.Abilities;
using OpenRpg.Core.Templates;

namespace OpenRpg.Combat.Extensions
{
    public static class ICombatTemplateAccessorExtensions
    {
        public static AbilityTemplate GetAbilityTemplate(this ITemplateAccessor templateAccessor, int abilityTemplateId)
        { return templateAccessor.Get<AbilityTemplate>(abilityTemplateId); }
    }
}