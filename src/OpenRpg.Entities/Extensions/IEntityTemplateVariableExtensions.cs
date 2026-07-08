using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.Races;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions;

public static class IEntityTemplateVariableExtensions
{
    public static bool HasGender(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(CoreEntityTemplateVariableTypes.Gender); }

    extension(EntityTemplateVariables vars)
    {
        public byte Gender
        {
            get => vars.GetByteOrDefault(CoreEntityTemplateVariableTypes.Gender, 0);
            set => vars[CoreEntityTemplateVariableTypes.Gender] = value;
        }
    }
        
    public static bool HasRace(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(CoreEntityTemplateVariableTypes.Race); }
        
    extension(EntityTemplateVariables vars)
    {
        public RaceData Race
        {
            get => vars.GetAsOrDefaultAndSet(CoreEntityTemplateVariableTypes.Race, () => new RaceData());
            set => vars[CoreEntityTemplateVariableTypes.Race] = value;
        }
    }
        
    public static bool HasClass(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(CoreEntityTemplateVariableTypes.Class); }
        
    extension(EntityTemplateVariables vars)
    {
        public ClassData Class
        {
            get => vars.GetAsOrDefaultAndSet(CoreEntityTemplateVariableTypes.Class, () => new ClassData());
            set => vars[CoreEntityTemplateVariableTypes.Class] = value;
        }
    }
        
    public static bool HasMultiClass(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(CoreEntityTemplateVariableTypes.MultiClasses); }
        
    extension(EntityTemplateVariables vars)
    {
        public MultiClasses MultiClass
        {
            get => vars.GetAsOrDefaultAndSet(CoreEntityTemplateVariableTypes.MultiClasses,  () => new MultiClasses());
            set => vars[CoreEntityTemplateVariableTypes.MultiClasses] = value;
        }
    }
}