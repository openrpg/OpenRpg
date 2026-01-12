using System.Collections.Generic;
using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions;

public static class FantasyAbilityTemplateVariableExtensions
{
    public static bool HasManaCost(this AbilityTemplateVariables vars) => vars.ContainsKey(FantasyAbilityTemplateVariableTypes.ManaCost);
    
    extension(AbilityTemplateVariables vars)
    {
        public int ManaCost
        {
            get => (int)vars.GetValueOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);
            set => vars[FantasyAbilityTemplateVariableTypes.ManaCost] = value;
        }
    }
}