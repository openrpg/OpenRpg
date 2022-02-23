namespace OpenRpg.Data.Conventions.Queries
{
    public class UpdateEntityQuery<T> : IQuery<T>
    {
        public T Entity { get; }
        public object Key { get; }

        public UpdateEntityQuery(T entity, object key)
        {
            Entity = entity;
            Key = key;
        }

        public T Execute(IDataSource dataSource)
        {
            dataSource.Update(Entity, Key);
            return Entity;
        }
    }
}