namespace OpenRpg.Data.Repositories
{
    public interface IWriteRepository<in T>
    {
        void Create(T entry);
        void Update(T entry);
        void Delete(T entry);
    }
}