using OpenRpg.Core.Common;

namespace OpenRpg.Data.Queries
{
    public interface IFindDataQuery<out T> : IFindQuery<T> where T : IHasDataId
    {}
}