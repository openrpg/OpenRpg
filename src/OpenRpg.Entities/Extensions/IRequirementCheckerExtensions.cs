using System.Collections.Generic;
using System.Linq;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Extensions
{
    public static class IRequirementCheckerExtensions
    {
        public static bool AreRequirementsMet<T>(this IRequirementChecker<T> checker, T target, IEnumerable<Requirement> requirements)
        { return AreRequirementsMet(checker, target, requirements.ToArray()); }
        
        public static bool AreRequirementsMet<T>(this IRequirementChecker<T> checker, T target, IReadOnlyCollection<Requirement> requirements)
        { return requirements.Count == 0 || requirements.All(x => checker.IsRequirementMet(target, x)); }
    }
}