using OpenRpg.Data.Conventions.Queries;

namespace OpenRpg.Data.Conventions.Extensions
{
    public static class RepositoryExtensions
    {
        public static T Create<T>(this IRepository repository, T entity, object id = null)  where T : class
        { return repository.Query(new CreateEntityQuery<T>(entity, id)); }
        
        public static T Get<T>(this IRepository repository, object id) where T : class
        { return repository.Query(new GetEntityQuery<T>(id)); }      
        
        public static T Update<T>(this IRepository repository, T entity, object id) where T : class
        { return repository.Query(new UpdateEntityQuery<T>(entity, id)); }      
        
        public static bool Delete<T>(this IRepository repository, object id) where T : class
        { return repository.Query(new DeleteEntityQuery<T>(id)); }
        
        public static bool Exists<T>(this IRepository repository, object id) where T : class
        { return repository.Query(new EntityExistsQuery<T>(id)); }
    }
}