using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Types;
using OpenRpg.Entities.Entity.Variables;

namespace OpenRpg.Combat.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add active effects onto them
    /// </summary>
    public static class CombatEntityVariableExtensions
    {
        public static bool HasActiveEffects(this EntityVariables vars) 
        { return vars.ContainsKey(CombatEntityVariableTypes.ActiveEffects); }
        
        public static IActiveEffects ActiveEffects(this EntityVariables vars)
        { return vars[CombatEntityVariableTypes.ActiveEffects] as IActiveEffects; }

        public static void ActiveEffects(this EntityVariables vars, IActiveEffects activeEffects)
        { vars[CombatEntityVariableTypes.ActiveEffects] = activeEffects; }
    }
}