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
        public static int Effects = 4002; // replaces IHasEffects
        public static int Requirements = 4003; // replaces IHasRequirements
        
        // Patterns
        public static int PatternId = 4020;
    }
}