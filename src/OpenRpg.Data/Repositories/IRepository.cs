namespace OpenRpg.Data.Repositories
{
    public interface IRepository<T,K> : IReadRepository<T,K>, IWriteRepository<T>
    {}
}