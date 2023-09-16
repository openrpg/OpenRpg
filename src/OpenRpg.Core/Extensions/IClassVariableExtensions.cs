using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Types;

namespace OpenRpg.Core.Extensions
{
    public static class IClassVariableExtensions
    {
        public static int Level(this IClassVariables vars) => (int)vars.Get(ClassVariableTypes.Level);
        public static void Level(this IClassVariables vars, int level) => vars[ClassVariableTypes.Level] = level;
        
        public static int Experience(this IClassVariables vars) => (int)vars.Get(ClassVariableTypes.Experience);
        public static void Experience(this IClassVariables vars, int experience) => vars[ClassVariableTypes.Experience] = experience;
        public static void AddExperience(this IClassVariables state, int change) => state.Experience(state.Experience() + change);
    }
}