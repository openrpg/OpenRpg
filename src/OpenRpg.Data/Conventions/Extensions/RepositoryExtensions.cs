using OpenRpg.Data.Conventions.Queries;

namespace OpenRpg.Data.Conventions.Extensions
{
    public static class RepositoryExtensions
    {
        public static T Create<T>(this IRepository repository, T entity, object key = null)  where T : class
        { return repository.Query(new CreateEntityQuery<T>(entity, key)); }
        
        public static T Get<T>(this IRepository repository, object key) where T : class
        { return repository.Query(new GetEntityQuery<T>(key)); }      
        
        public static T Update<T>(this IRepository repository, T entity, object key) where T : class
        { return repository.Query(new UpdateEntityQuery<T>(entity, key)); }      
        
        public static bool Delete<T>(this IRepository repository, object key) where T : class
        { return repository.Query(new DeleteEntityQuery<T>(key)); }
        
        public static bool Exists<T>(this IRepository repository, object key) where T : class
        { return repository.Query(new EntityExistsQuery<T>(key)); }
    }
}