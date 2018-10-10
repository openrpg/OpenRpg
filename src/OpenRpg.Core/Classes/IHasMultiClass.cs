using System.Collections.Generic;

namespace OpenRpg.Core.Characters
{
    public interface IHasMultiClass
    {
        IEnumerable<IClass> Classes { get; }
    }
}