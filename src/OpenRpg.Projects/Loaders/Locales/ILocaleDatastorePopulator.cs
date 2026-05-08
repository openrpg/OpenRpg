using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders.Locales;

public interface ILocaleDatastorePopulator
{
    Task PopulateDatastore(Project project, string projectReference, ILocaleDataSource dataSource);
}