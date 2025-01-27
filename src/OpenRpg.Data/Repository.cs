namespace OpenRpg.Data
{
    public class Repository : IRepository
    {
        public IDataSource DataSource { get; }

        public Repository(IDataSource dataSource)
        { DataSource = dataSource; }

        public T Query<T>(IQuery<T> query) => query.Execute(DataSource);
    }
}