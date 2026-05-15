using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenRpg.Core.Templates;
using OpenRpg.Data;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Projects.Json.Extensions;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Editor.Infrastructure.Persistence.Loaders;

public class DynamicTemplateDatastorePopulator : JsonTemplateDatastorePopulator
{
    public IFileService FileService { get; }
    public ITemplateLoader TemplateLoader { get; }
    public GenreService GenreService { get; }

    public DynamicTemplateDatastorePopulator(IFileService fileService, ITemplateLoader templateLoader, GenreService genreService) : base(fileService, templateLoader)
    {
        FileService = fileService;
        TemplateLoader = templateLoader;
        GenreService = genreService;
    }

    public override async Task ProcessTemplateTypes(Project project, string absoluteTemplateFolderPath, IDataSource dataSource)
    {
        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        var templateTypes = templateTypeRegistry.GetTemplateTypes();
        var processMethod = typeof(DynamicTemplateDatastorePopulator).GetMethod(nameof(ProcessTemplates), BindingFlags.NonPublic | BindingFlags.Instance);
        
        foreach (var templateType in templateTypes)
        {
            try
            {
                var templateClassType = templateTypeRegistry.GetTemplateClassType(templateType);
                var typeFilePath = Path.Combine(absoluteTemplateFolderPath, $"{templateClassType.Name}.json");
                if (!await FileService.Exists(typeFilePath)) { continue; }

                var genericMethod = processMethod.MakeGenericMethod(templateClassType);
                var task = (Task)genericMethod.Invoke(this, [project, absoluteTemplateFolderPath, dataSource]);
                if(task == null) { throw new Exception($"Failed to invoke dynamic template process for type [{templateType.Key}]");  }
                await task;
            }
            catch (Exception ex)
            { throw new Exception($"Failed to process template type '{templateType.Key}': {ex.Message}", ex); }
        }
    }
}