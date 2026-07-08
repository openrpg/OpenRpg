using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables;

namespace OpenRpg.Entities.Types
{
    // 4000 range
    public interface CoreTemplateVariableTypes
    {
        // Unknown
        public static int Unknown = 0;
        
        // For adding the notion of procedural effects 
        public static int ProceduralEffects = 4001;
        
        // General
        [CollectionElementType(typeof(IEffect))]
        public static int Effects = 4002; // replaces IHasEffects

        [CollectionElementType(typeof(Requirement))]
        public static int Requirements = 4003; // replaces IHasRequirements
        
        // Patterns
        public static int PatternTypeId = 4020;
        public static int PatternId = 4021;
    }
}