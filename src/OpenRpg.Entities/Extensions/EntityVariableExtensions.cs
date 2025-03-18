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
        
        public static byte Gender(this EntityVariables vars)
        { return vars.GetByteOrDefault(CoreEntityVariableTypes.Gender, 0); }

        public static void Gender(this EntityVariables vars, byte gender)
        { vars[CoreEntityVariableTypes.Gender] = gender; }
        
        public static bool HasRace(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.Race); }
        
        public static RaceData Race(this EntityVariables vars)
        { return vars.GetAs<RaceData>(CoreEntityVariableTypes.Race); }

        public static void Race(this EntityVariables vars, RaceData raceDataData)
        { vars[CoreEntityVariableTypes.Race] = raceDataData; }
        
        public static bool HasClass(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.Class); }
        
        public static ClassData Class(this EntityVariables vars)
        { return vars.GetAs<ClassData>(CoreEntityVariableTypes.Class); }

        public static void Class(this EntityVariables vars, ClassData classDataData)
        { vars[CoreEntityVariableTypes.Class] = classDataData; }
        
        public static bool HasMultiClass(this EntityVariables vars) 
        { return vars.ContainsKey(CoreEntityVariableTypes.MultiClasses); }
        
        public static MultiClasses MultiClass(this EntityVariables vars)
        { return vars.GetAs<MultiClasses>(CoreEntityVariableTypes.MultiClasses); }

        public static void MultiClass(this EntityVariables vars, MultiClasses multiClasses)
        { vars[CoreEntityVariableTypes.MultiClasses] = multiClasses; }
    }
}