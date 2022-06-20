namespace OpenRpg.Core.Types
{
    public interface CoreVariableTypes
    {
        // Unknown
        public static int Unknown = 0;
        
        // Base Types
        public static int StatsVariables = 1;
        public static int RaceTemplateVariables = 2;
        public static int ClassTemplateVariables = 3;
        public static int ClassVariables = 4;
        
        // Computed Types
        public static int ComputedStatsVariables = 1000;
    }
}