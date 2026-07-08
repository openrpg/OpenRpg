using OpenRpg.Core.Templates;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders.Templates;

public interface ITemplateLoader
{
    /// <summary>
    /// Loads all templates from a given reference
    /// </summary>
    /// <param name="project">The loaded project</param>
    /// <param name="templateReference">The reference to the template data i.e file path, asset name etc</param>
    /// <typeparam name="T">The template type</typeparam>
    Task<IReadOnlyCollection<T>> LoadTemplates<T>(Project project, string templateReference) where T : ITemplate;
}