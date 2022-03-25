namespace OpenRpg.Data.Conventions.Queries
{
    public class UpdateEntityQuery<T> : IQuery<T>
    {
        public T Entity { get; }
        public object Id { get; }

        public UpdateEntityQuery(T entity, object id)
        {
            Entity = entity;
            Id = id;
        }

        public T Execute(IDataSource dataSource)
        {
            dataSource.Update(Entity, Id);
            return Entity;
        }
    }
}