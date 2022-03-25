namespace OpenRpg.Data.Conventions.Queries
{
    public class DeleteEntityQuery<T> : IQuery<bool>
    {
        public object Id { get; }

        public DeleteEntityQuery(object id)
        { Id = id; }

        public bool Execute(IDataSource dataSource)
        { return dataSource.Delete<T>(Id); }
    }
}