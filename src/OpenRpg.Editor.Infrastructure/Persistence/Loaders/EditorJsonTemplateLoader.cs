using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Services;

namespace OpenRpg.Editor.Infrastructure.Persistence.Loaders;

public class EditorJsonTemplateLoader : JsonTemplateLoader
{
    public EditorJsonTemplateLoader(IFileService fileService) : base(fileService)
    {
    }
}