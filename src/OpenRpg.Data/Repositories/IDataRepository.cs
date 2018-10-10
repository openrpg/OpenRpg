using OpenRpg.Core.Common;

namespace OpenRpg.Data.Repositories
{
    public interface IDataRepository<T> : IReadDataRepository<T>, IWriteDataRepository<T> where T : IHasDataId
    {}
}