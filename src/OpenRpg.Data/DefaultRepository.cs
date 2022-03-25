namespace OpenRpg.Data
{
    public class DefaultRepository : IRepository
    {
        public IDataSource DataSource { get; }

        public DefaultRepository(IDataSource dataSource)
        { DataSource = dataSource; }

        public T Query<T>(IQuery<T> query) => query.Execute(DataSource);
    }
}