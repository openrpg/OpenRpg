using OpenRpg.Combat.Abilities;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates;
using OpenRpg.Data;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Projects.Json.Extensions;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;
using OpenRpg.Quests;

namespace OpenRpg.Projects.Json.Loaders.Templates;

public class JsonTemplateDatastorePopulator : ITemplateDatastorePopulator
{
    public IFileService FileService { get; }
    public ITemplateLoader TemplateLoader { get; }

    public JsonTemplateDatastorePopulator(IFileService fileService, ITemplateLoader templateLoader)
    {
        FileService = fileService;
        TemplateLoader = templateLoader;
    }

    public virtual async Task ProcessTemplateTypes(Project project, string absoluteTemplateFolderPath, IDataSource dataSource)
    {
        await ProcessTemplates<ItemTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<ClassTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<RaceTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<QuestTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<ItemCraftingTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<ItemGatheringTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<AbilityTemplate>(project, absoluteTemplateFolderPath, dataSource);
        await ProcessTemplates<EntityTemplate>(project, absoluteTemplateFolderPath, dataSource);
    }
    
    public async Task PopulateDatastore(ProjectContext projectContext, IDataSource dataSource)
    {
        var templateFolderPath = projectContext.Project.TemplatesFolder;
        var absoluteTemplateFolderPath = Path.Combine(projectContext.ProjectPath, templateFolderPath);
        var templatePathExists = await FileService.Exists(absoluteTemplateFolderPath);
        if(!templatePathExists) { throw new Exception($"Template folder [{absoluteTemplateFolderPath}] cannot be found"); }
        
        await ProcessTemplateTypes(projectContext.Project, absoluteTemplateFolderPath, dataSource);
    }

    protected async Task ProcessTemplates<T>(Project project, string templateFolderPath, IDataSource dataSource, bool throwOnMissing = false) where T : ITemplate
    {
        var itemTemplatePath = Path.Combine(templateFolderPath, $"{typeof(T).Name}.json");
        var templatePathExists = await FileService.Exists(itemTemplatePath);
        if (!templatePathExists)
        {
            if(throwOnMissing)
            { throw new Exception($"Template file [{itemTemplatePath}] cannot be found"); }
            return;
        }
        
        var templates = await TemplateLoader.LoadTemplates<T>(project, itemTemplatePath);
        templates.ForEach(x => dataSource.Update(x, x.Id));
    }
}