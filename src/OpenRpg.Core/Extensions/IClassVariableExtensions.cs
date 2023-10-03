using System;
using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Types;

namespace OpenRpg.Core.Extensions
{
    public static class IClassVariableExtensions
    {
        public static int Level(this IClassVariables vars) => vars.GetIntOrDefault(ClassVariableTypes.Level, 1);
        public static void Level(this IClassVariables vars, int level) => vars[ClassVariableTypes.Level] = level;
        
        public static int Experience(this IClassVariables vars) => vars.GetInt(ClassVariableTypes.Experience);
        public static void Experience(this IClassVariables vars, int experience) => vars[ClassVariableTypes.Experience] = experience;
        public static void AddExperience(this IClassVariables state, int change) => state.Experience(state.Experience() + change);
    }
}