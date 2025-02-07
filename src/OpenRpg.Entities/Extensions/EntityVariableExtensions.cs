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
        { return vars.ContainsKey(EntityVariableTypes.Gender); }
        
        public static byte Gender(this EntityVariables vars)
        { return vars.GetByteOrDefault(EntityVariableTypes.Gender, 0); }

        public static void Gender(this EntityVariables vars, byte gender)
        { vars[EntityVariableTypes.Gender] = gender; }
        
        public static bool HasRace(this EntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Race); }
        
        public static RaceData Race(this EntityVariables vars)
        { return vars.GetAs<RaceData>(EntityVariableTypes.Race); }

        public static void Race(this EntityVariables vars, RaceData raceDataData)
        { vars[EntityVariableTypes.Race] = raceDataData; }
        
        public static bool HasClass(this EntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Class); }
        
        public static ClassData Class(this EntityVariables vars)
        { return vars.GetAs<ClassData>(EntityVariableTypes.Class); }

        public static void Class(this EntityVariables vars, ClassData classDataData)
        { vars[EntityVariableTypes.Class] = classDataData; }
        
        public static bool HasMultiClass(this EntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.MultiClasses); }
        
        public static MultiClasses MultiClass(this EntityVariables vars)
        { return vars.GetAs<MultiClasses>(EntityVariableTypes.MultiClasses); }

        public static void MultiClass(this EntityVariables vars, MultiClasses multiClasses)
        { vars[EntityVariableTypes.MultiClasses] = multiClasses; }
    }
}