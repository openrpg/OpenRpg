using System.Collections.Generic;

namespace OpenRpg.Entities.Modifications
{
    public interface IHasModifications<out T> where T : ModificationData
    {
        IEnumerable<T> Modifications { get; }
    }
}