using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IHasModifications
    {
        IEnumerable<IModificationTemplate> Modifications { get; }
    }
}