using System.Collections.Generic;
using OpenRpg.Localization.Data.Queries;
using OpenRpg.Localization.Data.Queries.Conventions;
using OpenRpg.Localization.Data.Repositories;

namespace OpenRpg.Localization.Data.Extensions
{
    public static class LocaleRepositoryExtensions
    {
        public static string Create(this ILocaleRepository repository, string id, string text)
        { return repository.Query(new CreateLocaleQuery(id, text)); }
        
        public static string Get(this ILocaleRepository repository, string id)
        { return repository.Query(new GetLocaleQuery(id)); }      
        
        public static IEnumerable<string> GetAll(this ILocaleRepository repository, params string[] ids)
        { return repository.Query(new GetAllLocalesQuery(ids)); }      
        
        public static string Update(this ILocaleRepository repository, string id, string text)
        { return repository.Query(new UpdateLocaleQuery(id, text)); }      
        
        public static bool Delete(this ILocaleRepository repository, string id)
        { return repository.Query(new DeleteLocaleQuery(id)); }
        
        public static bool Exists(this ILocaleRepository repository, string id)
        { return repository.Query(new LocaleExistsQuery(id)); }

        public static LocaleDataset GetDataset(this ILocaleRepository repository)
        { return repository.Query(new GetLocaleDatasetQuery()); }
        
        public static LocaleDataset GetDataset(this ILocaleRepository repository, string locale)
        { return repository.Query(new GetLocaleDatasetQuery(locale)); }
    }
}