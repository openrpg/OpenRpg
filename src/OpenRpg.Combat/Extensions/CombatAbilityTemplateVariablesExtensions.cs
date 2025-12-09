using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatAbilityTemplateVariablesExtensions
    {
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
        }
    }
}