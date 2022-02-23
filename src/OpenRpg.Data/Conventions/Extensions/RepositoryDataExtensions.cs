using OpenRpg.Core.Common;
using OpenRpg.Data.Conventions.Queries;

namespace OpenRpg.Data.Conventions.Extensions
{
    public static class RepositoryDataExtensions
    {
        public static T Create<T>(this IRepository repository, T entity) where T : class, IHasDataId
        { return repository.Query(new CreateEntityQuery<T>(entity, entity.Id)); }   
        
        public static T Update<T>(this IRepository repository, T entity) where T : class, IHasDataId
        { return repository.Query(new UpdateEntityQuery<T>(entity, entity.Id)); }    
        
        public static bool Delete<T>(this IRepository repository, T entity) where T : class, IHasDataId
        { return repository.Query(new DeleteEntityQuery<T>(entity.Id)); }
        
        public static bool Exists<T>(this IRepository repository, T entity) where T : class, IHasDataId
        { return repository.Query(new EntityExistsQuery<T>(entity.Id)); }
    }
}