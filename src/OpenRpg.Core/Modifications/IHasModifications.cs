using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IHasModifications<out T> where T : IModificationTemplate
    {
        IEnumerable<T> Modifications { get; }
    }
}