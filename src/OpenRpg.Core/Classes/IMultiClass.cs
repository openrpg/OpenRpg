using System.Collections.Generic;
using OpenRpg.Core.Classes.Templates;

namespace OpenRpg.Core.Classes
{
    public interface IMultiClass
    {
        IEnumerable<IClass> Classes { get; }

        void AddClass(IClass classToAdd);
        void RemoveClass(int classIdToRemove);
        IClass GetClass(int classIdGet);
        bool HasClass(int classTemplateId);
    }
}