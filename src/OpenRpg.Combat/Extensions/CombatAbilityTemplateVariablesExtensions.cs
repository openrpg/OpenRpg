using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatAbilityTemplateVariablesExtensions
    {
        public static bool HasCooldown(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.Cooldown);
        public static bool HasDamage(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.Damage);
        public static bool HasRange(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.Range);
        public static bool HasAttackSize(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.AttackSize);
        public static bool HasTargetType(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.TargetType);
        public static bool HasMultiHit(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.MultiHit);
        public static bool HasTargetCount(this AbilityTemplateVariables vars) => vars.ContainsKey(CombatAbilityTemplateVariableTypes.TargetCount);
        
        extension(AbilityTemplateVariables vars)
        {
            public float Cooldown
            {
                get => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.Cooldown, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.Cooldown] = value;
            }

            public Damage Damage
            {
                get => vars.GetAsOrDefault(CombatAbilityTemplateVariableTypes.Damage, () => new Damage(0, 0));
                set => vars[CombatAbilityTemplateVariableTypes.Damage] = value;
            }
            
            public float Range
            {
                get => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.Range, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.Range] = value;
            }
            
            public float AttackSize
            {
                get => vars.GetFloatOrDefault(CombatAbilityTemplateVariableTypes.AttackSize, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.AttackSize] = value;
            }
            
            public int TargetType
            {
                get => vars.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.TargetType] = value;
            }
            
            public int MultiHit
            {
                get => vars.GetIntOrDefault(CombatAbilityTemplateVariableTypes.MultiHit, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.MultiHit] = value;
            }
            
            public int TargetCount
            {
                get => vars.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 0);
                set =>  vars[CombatAbilityTemplateVariableTypes.TargetCount] = value;
            }
        }
    }
}