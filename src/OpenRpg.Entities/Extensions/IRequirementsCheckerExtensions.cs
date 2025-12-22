using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Entity;

namespace OpenRpg.Entities.Extensions;

public static class IRequirementsCheckerExtensions
{
    public static bool AreRequirementsMet<T>(this IRequirementChecker<T> requirementChecker, ITemplateVariables templateVariables, T context) 
        where T : EntityData
    {
        if (!templateVariables.HasRequirements())
        { return true; }

        var requirements = templateVariables.Requirements;
        return requirementChecker.AreRequirementsMet(context, requirements);
    }
}