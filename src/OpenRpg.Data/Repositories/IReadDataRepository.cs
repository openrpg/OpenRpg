using OpenRpg.Core.Common;

namespace OpenRpg.Data.Repositories
{
    public interface IReadDataRepository<T> : IReadRepository<T, int> where T : IHasDataId
    {}
}