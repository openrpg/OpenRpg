using OpenRpg.Core.Associations;
using OpenRpg.Core.Variables;

namespace OpenRpg.Entities.Types
{
    // 4000 Range (wont conflict with template)
    public interface CoreTemplateDataVariableTypes
    {
        // Unknown
        public static int Unknown = 0;
        
        /// For adding the notion of levels to the object
        public static int Level = 4001;
        
        // For adding the procedural associations
        [CollectionElementType(typeof(Association))]
        public static int ProceduralAssociations = 4002;
    }
}