using OpenRpg.Combat.Abilities;
using OpenRpg.Core.Variables;

namespace OpenRpg.Combat.Types
{
    public interface CombatTemplateVariableTypes
    {
        // Unknown
        public static int Unknown = 0;
        
        // For adding the notion of procedural effects 
        [CollectionElementType(typeof(AbilityData))]
        public static int Abilities = 6000;
    }
}