using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IHasModifications<out T> where T : ModificationTemplate
    {
        IEnumerable<T> Modifications { get; }
    }
}