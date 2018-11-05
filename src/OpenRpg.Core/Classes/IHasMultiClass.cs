using System.Collections.Generic;

namespace OpenRpg.Core.Classes
{
    public interface IHasMultiClass
    {
        IEnumerable<IClass> Classes { get; }
    }
}