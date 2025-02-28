using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ClassVariableExtensions
    {
        public static int Experience(this ClassVariables vars) => vars.GetInt(CoreClassVariableTypes.Experience);
        public static void Experience(this ClassVariables vars, int experience) => vars[CoreClassVariableTypes.Experience] = experience;
        public static void AddExperience(this ClassVariables state, int change) => state.Experience(state.Experience() + change);
    }
}