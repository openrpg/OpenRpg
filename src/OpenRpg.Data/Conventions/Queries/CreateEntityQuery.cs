namespace OpenRpg.Data.Conventions.Queries
{
    public class CreateEntityQuery<T> : IQuery<T>
    {
        public T Entity { get; }
        public object Id { get; }

        public CreateEntityQuery(T entity, object id = null)
        {
            Entity = entity;
            Id = id;
        }

        public T Execute(IDataSource dataSource)
        {
            dataSource.Create(Entity, Id);
            return Entity;
        }
    }
}