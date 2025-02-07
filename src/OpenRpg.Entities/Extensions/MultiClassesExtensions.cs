using System.Linq;
using OpenRpg.Entities.Classes;

namespace OpenRpg.Entities.Extensions
{
    public static class MultiClassesExtensions
    {
        public static bool HasClass(this MultiClasses multiClasses, int classTemplateId) => multiClasses.Classes.Any(x => x.TemplateId == classTemplateId);

        public static ClassData AddClass(this MultiClasses multiClasses, int classTemplateId)
        {
            var newClass = new ClassData { TemplateId = classTemplateId };
            return AddClass(multiClasses, newClass);
        }
        
        public static ClassData AddClass(this MultiClasses multiClasses, ClassData classDataToAdd)
        {
            if (multiClasses.HasClass(classDataToAdd.TemplateId))
            { return multiClasses.Classes[classDataToAdd.TemplateId]; }
            
            multiClasses.Classes.Add(classDataToAdd);
            return classDataToAdd;
        } 
        
        public static bool RemoveClass(this MultiClasses multiClasses, int classTemplateId)
        {
            var relatedClass = multiClasses.GetClass(classTemplateId);
            if(relatedClass == null) { return false; }
            
            multiClasses.Classes.Remove(relatedClass);
            return true;
        }
        
        public static ClassData GetClass(this MultiClasses multiClasses, int classTemplateId)
        { return multiClasses.HasClass(classTemplateId) ? multiClasses.Classes.Single(x => x.TemplateId == classTemplateId) : null; }
    }
}