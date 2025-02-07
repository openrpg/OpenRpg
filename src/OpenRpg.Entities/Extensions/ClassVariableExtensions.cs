using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ClassVariableExtensions
    {
        public static int Level(this ClassVariables vars) => vars.GetIntOrDefault(ClassVariableTypes.Level, 1);
        public static void Level(this ClassVariables vars, int level) => vars[ClassVariableTypes.Level] = level;
        
        public static int Experience(this ClassVariables vars) => vars.GetInt(ClassVariableTypes.Experience);
        public static void Experience(this ClassVariables vars, int experience) => vars[ClassVariableTypes.Experience] = experience;
        public static void AddExperience(this ClassVariables state, int change) => state.Experience(state.Experience() + change);
    }
}