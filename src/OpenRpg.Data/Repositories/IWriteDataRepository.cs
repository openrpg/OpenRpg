using OpenRpg.Core.Common;

namespace OpenRpg.Data.Repositories
{
    public interface IWriteDataRepository<in T> : IWriteRepository<T> where T : IHasDataId
    {}
}