namespace OpenRpg.Data.Conventions.Queries
{
    public class GetEntityQuery<T> : IQuery<T>
    {
        public object Id { get; }

        public GetEntityQuery(object id)
        { Id = id; }

        public T Execute(IDataSource dataSource)
        { return dataSource.Get<T>(Id); }
    }
}