namespace OpenRpg.Data
{
    public interface IRepository
    {
        IDataSource DataSource { get; }
        
        T Query<T>(IQuery<T> query);
    }
}