using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.Races;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class EntityVariableExtensions
    {
        public static bool HasGender(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.Gender); }

        extension(EntityVariables vars)
        {
            public byte Gender
            {
                get => vars.GetByteOrDefault(CoreEntityVariableTypes.Gender, 0);
                set => vars[CoreEntityVariableTypes.Gender] = value;
            }
        }
        
        public static bool HasRace(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.Race); }
        
        extension(EntityVariables vars)
        {
            public RaceData Race
            {
                get => vars.GetAsOrDefaultAndSet(CoreEntityVariableTypes.Race, () => new RaceData());
                set => vars[CoreEntityVariableTypes.Race] = value;
            }
        }
        
        public static bool HasClass(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.Class); }
        
        extension(EntityVariables vars)
        {
            public ClassData Class
            {
                get => vars.GetAsOrDefaultAndSet(CoreEntityVariableTypes.Class, () => new ClassData());
                set => vars[CoreEntityVariableTypes.Class] = value;
            }
        }
        
        public static bool HasMultiClass(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.MultiClasses); }
        
        extension(EntityVariables vars)
        {
            public MultiClasses MultiClass
            {
                get => vars.GetAsOrDefaultAndSet(CoreEntityVariableTypes.MultiClasses,  () => new MultiClasses());
                set => vars[CoreEntityVariableTypes.MultiClasses] = value;
            }
        }
    }
}