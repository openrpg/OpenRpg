using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenRpg.Data;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Editor.Infrastructure.Persistence.Loaders;

public class DynamicTemplateDatastorePopulator : JsonTemplateDatastorePopulator
{
    private readonly ILogger<DynamicTemplateDatastorePopulator> _logger;

    public IFileService FileService { get; }
    public ITemplateLoader TemplateLoader { get; }
    public GenreService GenreService { get; }

    public DynamicTemplateDatastorePopulator(IFileService fileService, ITemplateLoader templateLoader, GenreService genreService, ILogger<DynamicTemplateDatastorePopulator> logger) : base(fileService, templateLoader)
    {
        FileService = fileService;
        TemplateLoader = templateLoader;
        GenreService = genreService;
        _logger = logger;
    }

    public override async Task ProcessTemplateTypes(Project project, string absoluteTemplateFolderPath, IDataSource dataSource)
    {
        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        var templateTypes = templateTypeRegistry.GetTemplateTypes();
        var processMethod = typeof(DynamicTemplateDatastorePopulator).GetMethod(nameof(ProcessTemplates), BindingFlags.NonPublic | BindingFlags.Instance);

        _logger.LogInformation("Processing {TemplateTypeCount} template type(s) from '{TemplateFolder}'", templateTypes.Count, absoluteTemplateFolderPath);

        foreach (var templateType in templateTypes)
        {
            try
            {
                var templateClassType = templateTypeRegistry.GetTemplateClassType(templateType);
                var typeFilePath = Path.Combine(absoluteTemplateFolderPath, $"{templateClassType.Name}.json");

                if (!await FileService.Exists(typeFilePath))
                {
                    _logger.LogDebug("No data file found for template type '{TemplateTypeKey}' (expected '{FilePath}'), skipping",
                        templateType.Key, typeFilePath);
                    continue;
                }

                _logger.LogInformation("Loading template data for type '{TemplateTypeKey}' ({ClassName}) from '{FilePath}'",
                    templateType.Key, templateClassType.Name, typeFilePath);

                var genericMethod = processMethod.MakeGenericMethod(templateClassType);
                var task = (Task)genericMethod.Invoke(this, [project, absoluteTemplateFolderPath, dataSource, false]);
                if(task == null) { throw new Exception($"Failed to invoke dynamic template process for type [{templateType.Key}]");  }
                await task;
            }
            catch (Exception ex)
            { throw new Exception($"Failed to process template type '{templateType.Key}': {ex.Message}", ex); }
        }

        _logger.LogInformation("Template type processing complete");
    }
}