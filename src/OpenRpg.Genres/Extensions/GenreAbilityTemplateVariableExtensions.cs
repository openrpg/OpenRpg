using System.Collections.Generic;
using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions;

public static class GenreAbilityTemplateVariableExtensions
{
    public static bool HasHealthCost(this AbilityTemplateVariables vars) => vars.ContainsKey(GenreAbilityTemplateVariableTypes.HealthCost);
    public static bool HasStaminaCost(this AbilityTemplateVariables vars) => vars.ContainsKey(GenreAbilityTemplateVariableTypes.StaminaCost);
    
    extension(AbilityTemplateVariables vars)
    {
        public int HealthCost
        {
            get => (int)vars.GetValueOrDefault(GenreAbilityTemplateVariableTypes.HealthCost, 0);
            set => vars[GenreAbilityTemplateVariableTypes.HealthCost] = value;
        }
        
        public int StaminaCost
        {
            get => (int)vars.GetValueOrDefault(GenreAbilityTemplateVariableTypes.StaminaCost, 0);
            set => vars[GenreAbilityTemplateVariableTypes.StaminaCost] = value;
        }
    }
}