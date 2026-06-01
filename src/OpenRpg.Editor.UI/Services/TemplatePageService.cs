using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Infrastructure.Helpers;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Editor.Infrastructure.Services;
using OpenRpg.Editor.UI.Components.Editors;
using OpenRpg.Entities.Extensions;
using OpenRpg.Localization.Data.Repositories;

namespace OpenRpg.Editor.UI.Services;

public class TemplatePageService
{
    private readonly GenreService _genreService;
    private readonly IRepository _repository;
    private readonly ILocaleRepository _localeRepository;
    private readonly ICloner _cloner;
    private readonly ILogger<TemplatePageService> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public TemplatePageService(
        GenreService genreService,
        IRepository repository,
        ILocaleRepository localeRepository,
        ICloner cloner,
        ILogger<TemplatePageService> logger,
        ILoggerFactory loggerFactory)
    {
        _genreService = genreService;
        _repository = repository;
        _localeRepository = localeRepository;
        _cloner = cloner;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public DynamicTemplateHelper CreateHelper(string templateTypeKey, out ITemplateTypeDescriptor descriptor)
    {
        var registry = _genreService.GetTemplateTypeRegistry();
        descriptor = registry.GetTemplateType(templateTypeKey);

        if (descriptor == null)
        {
            _logger.LogWarning("Template type key '{TemplateTypeKey}' not found in registry", templateTypeKey);
            return null;
        }

        var templateType = Type.GetType(descriptor.TemplateTypeName);
        if (templateType == null)
        {
            _logger.LogWarning("CLR type '{TemplateTypeName}' could not be resolved for template '{TemplateTypeKey}'",
                descriptor.TemplateTypeName, templateTypeKey);
            return null;
        }

        _logger.LogInformation("Loaded template type '{TemplateTypeKey}' ({TemplateTypeName})",
            templateTypeKey, descriptor.TemplateTypeName);

        var helperLogger = _loggerFactory.CreateLogger<DynamicTemplateHelper>();
        return new DynamicTemplateHelper(templateType, _repository, helperLogger);
    }

    public ITemplate CreateTemplate(DynamicTemplateHelper helper, string assetCodePrefix)
    {
        var newId = helper.GetNewId();
        var newTemplate = Activator.CreateInstance(helper.TemplateType) as ITemplate;
        if (newTemplate == null)
        {
            _logger.LogError("Failed to create instance of {TemplateType}", helper.TemplateType.Name);
            return null;
        }

        var variables = helper.GetVariables(newTemplate);
        variables.AssetCode = $"{assetCodePrefix}-{newId}";

        helper.SetId(newTemplate, newId);
        helper.ListifyProperties(newTemplate);
        helper.GenerateLocaleCodes(newTemplate);

        var localeCode = _localeRepository.CurrentLocaleCode;
        EnsureLocaleEntry(localeCode, newTemplate.NameLocaleId);
        EnsureLocaleEntry(localeCode, newTemplate.DescriptionLocaleId);

        helper.Create(newTemplate, newId);

        _logger.LogInformation("Created new {TemplateType} with Id {Id}, asset code '{AssetCode}'",
            helper.TemplateType.Name, newId, variables.AssetCode);

        return newTemplate;
    }

    public ITemplate CloneTemplate(DynamicTemplateHelper helper, ITemplate source)
    {
        var rawClone = _cloner.Clone(source);
        var clone = rawClone as ITemplate;
        if (clone == null)
        {
            _logger.LogError("Failed to clone {TemplateType} Id {Id}", helper.TemplateType.Name, source.Id);
            return null;
        }

        var newId = helper.GetNewId();

        var oldNameLocaleId = clone.NameLocaleId;
        var oldDescriptionLocaleId = clone.DescriptionLocaleId;

        var variables = helper.GetVariables(clone);
        var oldCode = variables.AssetCode;
        variables.AssetCode = $"{oldCode}-clone";

        helper.SetId(clone, newId);
        helper.ListifyProperties(clone);
        helper.GenerateLocaleCodes(clone);

        var localeCode = _localeRepository.CurrentLocaleCode;
        CopyLocaleEntry(localeCode, oldNameLocaleId, clone.NameLocaleId);
        CopyLocaleEntry(localeCode, oldDescriptionLocaleId, clone.DescriptionLocaleId);

        helper.Create(clone, newId);

        _logger.LogInformation(
            "Cloned {TemplateType} from '{OldCode}' (Id {OldId}) to '{NewCode}' (Id {NewId})",
            helper.TemplateType.Name, oldCode, source.Id, variables.AssetCode, newId);

        return clone;
    }

    public void DeleteTemplate(DynamicTemplateHelper helper, ITemplate template)
    {
        var variables = helper.GetVariables(template);
        var assetCode = variables?.AssetCode ?? "(unknown)";

        helper.Delete(template);

        _logger.LogInformation("Deleted {TemplateType} with Id {Id}, asset code '{AssetCode}'",
            helper.TemplateType.Name, template.Id, assetCode);
    }

    public string GetAssetCode(ITemplate template, string assetCodePrefix)
    {
        var id = template.GetType().GetProperty("Id")?.GetValue(template) as int? ?? 0;
        var variables = template.GetType().GetProperty("Variables")?.GetValue(template) as IVariables<object>;
        if (variables?.HasAssetCode() == true)
        {
            return variables.AssetCode;
        }

        var fallback = $"{assetCodePrefix ?? "template"}-{id}";
        _logger.LogWarning("No asset code found for {TemplateType} Id {Id}, using fallback '{Fallback}'",
            template.GetType().Name, id, fallback);
        return fallback;
    }

    public List<TemplateEditorGroup> GetEditorGroups(ITemplateTypeDescriptor descriptor)
    {
        if (descriptor?.Groups == null)
        {
            return new List<TemplateEditorGroup>();
        }

        return descriptor.Groups.Select(g => new TemplateEditorGroup
        {
            Title = g.Title,
            EditorComponent = g.EditorComponent,
            Options = g.Options ?? new Dictionary<string, string>(),
            Fields = g.Fields?.Select(f => new TemplateEditorField
            {
                Property = f.Property,
                Name = f.Name,
                EditorType = f.EditorType,
                EditorComponent = f.EditorComponent,
                Options = f.Options ?? new Dictionary<string, string>()
            }).ToList() ?? new List<TemplateEditorField>()
        }).ToList();
    }

    private void EnsureLocaleEntry(string localeCode, string localeId)
    {
        if (string.IsNullOrEmpty(localeId)) return;
        if (!_localeRepository.DataSource.Exists(localeCode, localeId))
        {
            _localeRepository.DataSource.Create(localeCode, localeId, "");
        }
    }

    private void CopyLocaleEntry(string localeCode, string oldLocaleId, string newLocaleId)
    {
        if (string.IsNullOrEmpty(oldLocaleId) || string.IsNullOrEmpty(newLocaleId))
            return;

        if (!_localeRepository.DataSource.Exists(localeCode, oldLocaleId))
            return;

        var text = _localeRepository.DataSource.Get(localeCode, oldLocaleId);
        if (!_localeRepository.DataSource.Exists(localeCode, newLocaleId))
        {
            _localeRepository.DataSource.Create(localeCode, newLocaleId, text);
        }
    }
}
