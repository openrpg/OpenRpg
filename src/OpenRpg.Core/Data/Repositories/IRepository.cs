using OpenRpg.Core.Common;

namespace OpenRpg.Core.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IHasDataId
    {
        
    }
}