using OpenRpg.Data;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders.Templates;

public interface ITemplateDatastorePopulator
{
    Task PopulateDatastore(Project project, string projectReference, IDataSource dataSource);
}