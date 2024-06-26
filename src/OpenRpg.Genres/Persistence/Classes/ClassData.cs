using System.Collections.Generic;

namespace OpenRpg.Genres.Persistence.Classes
{
    public class ClassData
    {
        public int ClassTemplateId { get; }
        public IReadOnlyDictionary<int, object> Variables { get; }
        
        public ClassData(int classTemplateId, IReadOnlyDictionary<int, object> variables = null)
        {
            ClassTemplateId = classTemplateId;
            Variables = variables ?? new Dictionary<int, object>();
        }
    }
}