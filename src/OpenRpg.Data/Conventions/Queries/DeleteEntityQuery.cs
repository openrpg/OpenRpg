namespace OpenRpg.Data.Conventions.Queries
{
    public class DeleteEntityQuery<T> : IQuery<bool>
    {
        public object Key { get; }

        public DeleteEntityQuery(object key)
        { Key = key; }

        public bool Execute(IDataSource dataSource)
        { return dataSource.Delete<T>(Key); }
    }
}