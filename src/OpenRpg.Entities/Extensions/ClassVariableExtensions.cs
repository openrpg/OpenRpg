using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ClassVariableExtensions
    {
        extension(ClassVariables variables)
        {
            public int Experience
            {
                get => variables.GetInt(CoreClassVariableTypes.Experience);
                set => variables[CoreClassVariableTypes.Experience] = value;
            }
        }
        
        public static void AddExperience(this ClassVariables state, int change) => state.Experience += change;
    }
}