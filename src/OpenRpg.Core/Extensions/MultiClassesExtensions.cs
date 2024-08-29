using System.Linq;
using OpenRpg.Core.Classes;

namespace OpenRpg.Core.Extensions
{
    public static class MultiClassesExtensions
    {
        public static bool HasClass(this MultiClasses multiClasses, int classTemplateId) => multiClasses.Classes.Any(x => x.TemplateId == classTemplateId);

        public static Class AddClass(this MultiClasses multiClasses, int classTemplateId)
        {
            var newClass = new Class { TemplateId = classTemplateId };
            return AddClass(multiClasses, newClass);
        }
        
        public static Class AddClass(this MultiClasses multiClasses, Class classToAdd)
        {
            if (multiClasses.HasClass(classToAdd.TemplateId))
            { return multiClasses.Classes[classToAdd.TemplateId]; }
            
            multiClasses.Classes.Add(classToAdd);
            return classToAdd;
        } 
        
        public static bool RemoveClass(this MultiClasses multiClasses, int classTemplateId)
        {
            var relatedClass = multiClasses.GetClass(classTemplateId);
            if(relatedClass == null) { return false; }
            
            multiClasses.Classes.Remove(relatedClass);
            return true;
        }
        
        public static Class GetClass(this MultiClasses multiClasses, int classTemplateId)
        { return multiClasses.HasClass(classTemplateId) ? multiClasses.Classes.Single(x => x.TemplateId == classTemplateId) : null; }
    }
}