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
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Editor.Infrastructure.Persistence.Loaders;

public class DynamicTemplateDatastorePopulator : ITemplateDatastorePopulator
{
    public IFileService FileService { get; }
    public ITemplateLoader TemplateLoader { get; }
    public GenreService GenreService { get; }

    public DynamicTemplateDatastorePopulator(IFileService fileService, ITemplateLoader templateLoader, GenreService genreService)
    {
        FileService = fileService;
        TemplateLoader = templateLoader;
        GenreService = genreService;
    }

    public async Task PopulateDatastore(Project project, string projectPath, IDataSource dataSource)
    {
        var templateFolderPath = project.TemplatesFolder;
        var absoluteTemplateFolderPath = Path.Combine(projectPath, templateFolderPath);
        var templatePathExists = await FileService.Exists(absoluteTemplateFolderPath);
        if (!templatePathExists) { throw new Exception($"Template folder [{absoluteTemplateFolderPath}] cannot be found"); }

        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        var templateTypes = templateTypeRegistry.GetTemplateTypes();
        var processMethod = typeof(DynamicTemplateDatastorePopulator).GetMethod(nameof(ProcessTemplates), BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var templateType in templateTypes)
        {
            try
            {
                var templateClassType = templateTypeRegistry.GetTemplateClassType(templateType);
                var genericMethod = processMethod.MakeGenericMethod(templateClassType);
                var task = (Task)genericMethod.Invoke(this, new object[] { project, absoluteTemplateFolderPath, dataSource });
                await task;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process template type '{templateType.Key}': {ex.Message}", ex);
            }
        }
    }

    protected async Task ProcessTemplates<T>(Project project, string templateFolderPath, IDataSource dataSource) where T : ITemplate
    {
        var templatePath = Path.Combine(templateFolderPath, $"{typeof(T).Name}.json");
        var templatePathExists = await FileService.Exists(templatePath);
        if (!templatePathExists) { return; }

        var templates = await TemplateLoader.LoadTemplates<T>(project, templatePath);
        foreach (var template in templates)
        {
            dataSource.Update(template, template.Id);
        }
    }
}