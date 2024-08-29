using System;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Races;
using OpenRpg.Core.Races.Templates;
using OpenRpg.Core.Types;

namespace OpenRpg.Core.Extensions
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
        
        public static Race Race(this EntityVariables vars)
        { return vars.GetAs<Race>(EntityVariableTypes.Race); }

        public static void Race(this EntityVariables vars, Race raceData)
        { vars[EntityVariableTypes.Race] = raceData; }
        
        public static bool HasClass(this EntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Class); }
        
        public static Class Class(this EntityVariables vars)
        { return vars.GetAs<Class>(EntityVariableTypes.Class); }

        public static void Class(this EntityVariables vars, Class classData)
        { vars[EntityVariableTypes.Class] = classData; }
        
        public static bool HasMultiClass(this EntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.MultiClasses); }
        
        public static MultiClasses MultiClass(this EntityVariables vars)
        { return vars.GetAs<MultiClasses>(EntityVariableTypes.MultiClasses); }

        public static void MultiClass(this EntityVariables vars, MultiClasses multiClasses)
        { vars[EntityVariableTypes.MultiClasses] = multiClasses; }
    }
}