using System.Collections.Generic;

namespace OpenRpg.Genres.Persistence.Classes
{
    public class MultiClassData
    {
        public IReadOnlyCollection<ClassData> Classes { get; }
        
        public MultiClassData(IReadOnlyCollection<ClassData> classes)
        {
            Classes = classes;
        }
    }
}