using System.Collections.Generic;

namespace OpenRpg.Entities.Modifications
{
    public interface IHasModifications<out T> where T : ModificationTemplate
    {
        IEnumerable<T> Modifications { get; }
    }
}