namespace OpenRpg.Data.Conventions.Queries
{
    public class GetEntityQuery<T> : IQuery<T>
    {
        public object Key { get; }

        public GetEntityQuery(object key)
        { Key = key; }

        public T Execute(IDataSource dataSource)
        { return dataSource.Get<T>(Key); }
    }
}