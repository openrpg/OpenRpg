using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatAbilityTemplateVariablesExtensions
    {
        public static float Cooldown(this AbilityTemplateVariables vars) => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.Cooldown, 0);
        public static void Cooldown(this AbilityTemplateVariables vars, int value) => vars[CombatAbilityTemplateVariableTypes.Cooldown] = value;
        
        public static Damage Damage(this AbilityTemplateVariables vars) => vars.GetAsOrDefault(CombatAbilityTemplateVariableTypes.Damage, () => new Damage(0, 0));
        public static void Damage(this AbilityTemplateVariables vars, Damage damage) => vars[CombatAbilityTemplateVariableTypes.Damage] = damage;
        
        public static float Range(this AbilityTemplateVariables vars) => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.Range, 0);
        public static void Range(this AbilityTemplateVariables vars, float value) => vars[CombatAbilityTemplateVariableTypes.Range] = value;
        
        public static float AttackSize(this AbilityTemplateVariables vars) => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.AttackSize, 0);
        public static void AttackSize(this AbilityTemplateVariables vars, float value) => vars[CombatAbilityTemplateVariableTypes.AttackSize] = value;
        
        public static int TargetType(this AbilityTemplateVariables vars) => vars.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 0);
        public static void TargetType(this AbilityTemplateVariables vars, int value) => vars[CombatAbilityTemplateVariableTypes.TargetType] = value;
    }
}