using OpenRpg.Core.Common;

namespace OpenRpg.Core.Data.Repositories
{
    public interface IWriteRepository<in T> where T : IHasDataId
    {
        void Create(T entry);
        void Update(T entry);
        void Delete(T entry);
    }
}