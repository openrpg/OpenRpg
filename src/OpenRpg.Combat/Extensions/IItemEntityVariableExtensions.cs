using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables.Entity;

namespace OpenRpg.Combat.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add active effects onto them
    /// </summary>
    public static class CombatEntityVariableExtensions
    {
        public static bool HasActiveEffects(this IEntityVariables vars) 
        { return vars.ContainsKey(CombatEntityVariableTypes.ActiveEffects); }
        
        public static IActiveEffects ActiveEffects(this IEntityVariables vars)
        { return vars[CombatEntityVariableTypes.ActiveEffects] as IActiveEffects; }

        public static void ActiveEffects(this IEntityVariables vars, IActiveEffects activeEffects)
        { vars[CombatEntityVariableTypes.ActiveEffects] = activeEffects; }
    }
}