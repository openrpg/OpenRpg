using System.Collections.Generic;
using OpenRpg.Localization.Data.Queries.Conventions;
using OpenRpg.Localization.Data.Repositories;

namespace OpenRpg.Localization.Data.Extensions
{
    public static class LocaleRepositoryExtensions
    {
        public static string Create(this ILocaleRepository repository, string text, string id)
        { return repository.Query(new CreateLocaleQuery(text, id)); }
        
        public static string Get(this ILocaleRepository repository, string id)
        { return repository.Query(new GetLocaleQuery(id)); }      
        
        public static IEnumerable<string> GetAll(this ILocaleRepository repository, params string[] ids)
        { return repository.Query(new GetAllLocalesQuery(ids)); }      
        
        public static string Update(this ILocaleRepository repository, string text, string id)
        { return repository.Query(new UpdateLocaleQuery(text, id)); }      
        
        public static bool Delete(this ILocaleRepository repository, string id)
        { return repository.Query(new DeleteLocaleQuery(id)); }
        
        public static bool Exists(this ILocaleRepository repository, string id)
        { return repository.Query(new LocaleExistsQuery(id)); }
    }
}