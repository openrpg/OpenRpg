using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
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

        extension(EntityVariables vars)
        {
            public IActiveEffects ActiveEffects
            {
                get => vars.GetAsOrDefault(CombatEntityVariableTypes.ActiveEffects, () => new DefaultActiveEffects());
                set => vars[CombatEntityVariableTypes.ActiveEffects] = value;
            }
        }
    }
}