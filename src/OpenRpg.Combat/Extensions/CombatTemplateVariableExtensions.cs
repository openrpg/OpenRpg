using System;
using System.Collections.Generic;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates.Variables;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatTemplateVariableExtensions
    {
        public static bool HasAbilities(this ITemplateVariables vars) =>
            vars.ContainsKey(CombatTemplateVariableTypes.Abilities);
        
        public static IReadOnlyCollection<AbilityData> Abilities(this ITemplateVariables vars) =>
            vars.GetAsOrDefault(CombatTemplateVariableTypes.Abilities, Array.Empty<AbilityData>);
        
        public static void Abilities(this ITemplateVariables vars, IReadOnlyCollection<AbilityData> abilities) =>
            vars[CombatTemplateVariableTypes.Abilities] = abilities;
    }
}