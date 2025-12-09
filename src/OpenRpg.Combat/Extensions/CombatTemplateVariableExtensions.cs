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

        extension(ITemplateVariables vars)
        {
            public IReadOnlyCollection<AbilityData> Abilities
            {
                get => vars.GetAsOrDefault(CombatTemplateVariableTypes.Abilities, Array.Empty<AbilityData>);
                set => vars[CombatTemplateVariableTypes.Abilities] = value;
                
            }
        }
        

    }
}