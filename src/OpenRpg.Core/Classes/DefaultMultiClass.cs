using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Core.Classes
{
    public class DefaultMultiClass : IMultiClass
    {
        public Dictionary<int, IClass> InternalClasses { get; set; } = new Dictionary<int, IClass>();
        
        public IEnumerable<IClass> Classes => InternalClasses.Values;

        public void AddClass(IClass classToAdd)
        {
            if (!HasClass(classToAdd.Template.Id))
            { InternalClasses.Add(classToAdd.Template.Id, classToAdd); }
        }

        public void RemoveClass(int classTemplateId)
        {
            if (HasClass(classTemplateId))
            { InternalClasses.Remove(classTemplateId); }
        }

        public IClass GetClass(int classTemplateId)
        { return HasClass(classTemplateId) ? InternalClasses[classTemplateId] : null; }

        public bool HasClass(int classTemplateId)
        { return InternalClasses.ContainsKey(classTemplateId); }

        public DefaultMultiClass()
        {}

        public DefaultMultiClass(IEnumerable<IClass> classes)
        { InternalClasses = classes.ToDictionary(x => x.Template.Id, x => x); }
    }
}