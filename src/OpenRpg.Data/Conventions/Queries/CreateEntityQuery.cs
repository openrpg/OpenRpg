namespace OpenRpg.Data.Conventions.Queries
{
    public class CreateEntityQuery<T> : IQuery<T>
    {
        public T Entity { get; }
        public object Key { get; }

        public CreateEntityQuery(T entity, object key = null)
        {
            Entity = entity;
            Key = key;
        }

        public T Execute(IDataSource dataSource)
        {
            dataSource.Create(Entity, Key);
            return Entity;
        }
    }
}