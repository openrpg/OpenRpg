using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Repositories
{
    public interface IWriteRepository<in T>
    {
        object Execute(IExecuteQuery query);
        
        void Create(T entry);
        void Update(T entry);
        void Delete(T entry);
    }
}