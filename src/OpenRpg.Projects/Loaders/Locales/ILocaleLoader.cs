using OpenRpg.Localization;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders.Locales;

public interface ILocaleLoader
{
    /// <summary>
    /// Loads the locale data from a given reference
    /// </summary>
    /// <param name="project">The loaded project</param>
    /// <param name="localeReference">The reference to the locale data i.e file path, asset name etc</param>
    Task<LocaleDataset>  LoadLocales(Project project, string localeReference);
}