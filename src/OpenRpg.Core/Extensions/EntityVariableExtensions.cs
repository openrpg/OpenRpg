using OpenRpg.Core.Classes;
using OpenRpg.Core.Races;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.Entity;

namespace OpenRpg.Core.Extensions
{
    public static class EntityVariableExtensions
    {
        public static bool HasGender(this IEntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Gender); }
        
        public static byte Gender(this IEntityVariables vars)
        { return (byte)(vars[EntityVariableTypes.Gender] ?? 0); }

        public static void Gender(this IEntityVariables vars, byte gender)
        { vars[EntityVariableTypes.Gender] = gender; }
        
        public static bool HasRace(this IEntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Race); }
        
        public static IRaceTemplate Race(this IEntityVariables vars)
        { return vars[EntityVariableTypes.Race] as IRaceTemplate; }

        public static void Race(this IEntityVariables vars, IRaceTemplate race)
        { vars[EntityVariableTypes.Race] = race; }
        
        public static bool HasClass(this IEntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.Class); }
        
        public static IClass Class(this IEntityVariables vars)
        { return vars[EntityVariableTypes.Class] as IClass; }

        public static void Class(this IEntityVariables vars, IClass classToUse)
        { vars[EntityVariableTypes.Class] = classToUse; }
        
        public static bool HasMultiClass(this IEntityVariables vars) 
        { return vars.ContainsKey(EntityVariableTypes.MultiClasses); }
        
        public static IMultiClass MultiClass(this IEntityVariables vars)
        { return vars[EntityVariableTypes.MultiClasses] as IMultiClass; }

        public static void MultiClass(this IEntityVariables vars, IMultiClass multiClass)
        { vars[EntityVariableTypes.MultiClasses] = multiClass; }
    }
}