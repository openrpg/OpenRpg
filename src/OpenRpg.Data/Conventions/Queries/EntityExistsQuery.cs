namespace OpenRpg.Data.Conventions.Queries
{
    public class EntityExistsQuery<T> : IQuery<bool>
    {
        public object Key { get; }

        public EntityExistsQuery(object key)
        { Key = key; }

        public bool Execute(IDataSource dataSource)
        { return dataSource.Exists<T>(Key); }
    }
}