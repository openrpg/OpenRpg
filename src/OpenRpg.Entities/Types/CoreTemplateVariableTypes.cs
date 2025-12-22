namespace OpenRpg.Entities.Types
{
    public interface CoreTemplateVariableTypes
    {
        // Unknown
        public static int Unknown = 0;
        
        // For adding the notion of procedural effects 
        public static int ProceduralEffects = 5001;
        
        // General
        public static int Effects = 5002; // replaces IHasEffects
        public static int Requirements = 5003; // replaces IHasRequirements
    }
}