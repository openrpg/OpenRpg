namespace OpenRpg.Data.Conventions.Queries
{
    public class EntityExistsQuery<T> : IQuery<bool>
    {
        public object Id { get; }

        public EntityExistsQuery(object id)
        { Id = id; }

        public bool Execute(IDataSource dataSource)
        { return dataSource.Exists<T>(Id); }
    }
}