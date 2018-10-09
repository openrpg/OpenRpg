using System.Collections.Generic;
using OpenRpg.Core.Common;

namespace OpenRpg.Core.Data.Queries
{
    public interface IFindQuery<out T> where T : IHasDataId
    {
        IEnumerable<T> Find(object dataSource);
    }
}