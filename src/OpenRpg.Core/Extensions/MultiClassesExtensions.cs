using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;

namespace OpenRpg.Core.Extensions
{
    public static class MultiClassesExtensions
    {
        public static bool HasClass(this MultiClasses multiClasses, int classTemplateId) => multiClasses.Classes.ContainsKey(classTemplateId);

        public static Class AddClass(this MultiClasses multiClasses, int classTemplateId)
        {
            var newClass = new Class { TemplateId = classTemplateId };
            return AddClass(multiClasses, newClass);
        }
        
        public static Class AddClass(this MultiClasses multiClasses, Class classToAdd)
        {
            if (multiClasses.Classes.ContainsKey(classToAdd.TemplateId))
            { return multiClasses.Classes[classToAdd.TemplateId]; }
            
            multiClasses.Classes.Add(classToAdd.TemplateId, classToAdd);
            return classToAdd;
        } 
        
        public static bool RemoveClass(this MultiClasses multiClasses, int classTemplateId)
        {
            if (!multiClasses.Classes.ContainsKey(classTemplateId))
            { return false; }
            
            multiClasses.Classes.Remove(classTemplateId);
            return true;
        }
        
        public static Class GetClass(this MultiClasses multiClasses, int classTemplateId)
        { return multiClasses.Classes.ContainsKey(classTemplateId) ? multiClasses.Classes[classTemplateId] : null; }
    }
}