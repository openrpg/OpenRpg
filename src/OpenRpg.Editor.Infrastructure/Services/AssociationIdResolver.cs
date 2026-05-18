using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Core.Templates;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Infrastructure.Plugins;

namespace OpenRpg.Editor.Infrastructure.Services;

public class AssociationIdResolver : IAssociationIdResolver
{
    private readonly IDataSource _dataSource;
    private readonly ITemplateOptionsResolver _optionsResolver;
    private readonly GenreService _genreService;
    private static readonly MethodInfo _getAllMethodDef = typeof(IDataSource)
        .GetMethods()
        .FirstOrDefault(m => m.Name == nameof(IDataSource.GetAll) && m.IsGenericMethodDefinition);

    public AssociationIdResolver(IDataSource dataSource, ITemplateOptionsResolver optionsResolver, GenreService genreService)
    {
        _dataSource = dataSource;
        _optionsResolver = optionsResolver;
        _genreService = genreService;
    }

    public AssociationIdOptions GetOptionsFor(string context, int typeValue)
    {
        if (!_mappings.TryGetValue((context, typeValue), out var mapping))
            return new AssociationIdOptions();

        var result = mapping.Kind switch
        {
            MappingKind.Template => ResolveTemplateOptions(mapping.Target),
            MappingKind.TypeSource => ResolveTypeSourceOptions(mapping.Target),
            MappingKind.Disabled => new AssociationIdOptions { Disabled = true },
            _ => new AssociationIdOptions()
        };

        return result with { IsMapped = true };
    }

    private AssociationIdOptions ResolveTemplateOptions(string templateTypeKey)
    {
        var type = ResolveTemplateType(templateTypeKey);
        if (type == null)
            return new AssociationIdOptions { Options = Array.Empty<OptionData>() };

        if (_getAllMethodDef == null)
            return new AssociationIdOptions { Options = Array.Empty<OptionData>() };

        var genericGetAll = _getAllMethodDef.MakeGenericMethod(type);
        var items = (IEnumerable)genericGetAll.Invoke(_dataSource, null);

        var options = items
            .Cast<ITemplate>()
            .Select(t => new OptionData(t.Id, t.NameLocaleId ?? $"Unknown ({t.Id})"))
            .ToArray();

        return new AssociationIdOptions { Options = options };
    }

    private Type ResolveTemplateType(string templateTypeKey)
    {
        var registry = _genreService.GetTemplateTypeRegistry();
        var descriptor = registry.GetTemplateType(templateTypeKey);
        if (descriptor == null)
            return null;

        return registry.GetTemplateClassType(descriptor);
    }

    private AssociationIdOptions ResolveTypeSourceOptions(string typeSource)
    {
        var options = _optionsResolver.GetOptionsForType(typeSource);
        return new AssociationIdOptions { Options = options };
    }

    private enum MappingKind { Template, TypeSource, Disabled }

    private record MappingEntry(MappingKind Kind, string Target = null);

    private static readonly Dictionary<(string Context, int TypeValue), MappingEntry> _mappings = new()
    {
        // === Requirement Types ===
        { ("Requirement", 1), new MappingEntry(MappingKind.Template, "race") },
        { ("Requirement", 2), new MappingEntry(MappingKind.Template, "class") },
        { ("Requirement", 3), new MappingEntry(MappingKind.TypeSource, "genderTypes") },
        { ("Requirement", 20), new MappingEntry(MappingKind.Template, "item") },
        { ("Requirement", 21), new MappingEntry(MappingKind.Template, "item") },
        { ("Requirement", 40), new MappingEntry(MappingKind.TypeSource, "effectTypes") },
        { ("Requirement", 60), new MappingEntry(MappingKind.Template, "quest") },
        { ("Requirement", 61), new MappingEntry(MappingKind.Template, "quest") },
        { ("Requirement", 62), new MappingEntry(MappingKind.Template, "quest") },
        { ("Requirement", 80), new MappingEntry(MappingKind.TypeSource, "craftingSkillTypes") },
        { ("Requirement", 50), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 51), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 52), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 53), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 54), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 55), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 56), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 100), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 101), new MappingEntry(MappingKind.Disabled) },
        { ("Requirement", 103), new MappingEntry(MappingKind.Disabled) },

        // === Objective Types ===
        { ("Objective", 1), new MappingEntry(MappingKind.Template, "quest") },
        { ("Objective", 2), new MappingEntry(MappingKind.Template, "item") },
        { ("Objective", 3), new MappingEntry(MappingKind.Disabled) },
        { ("Objective", 4), new MappingEntry(MappingKind.Template, "class") },
        { ("Objective", 5), new MappingEntry(MappingKind.TypeSource, "effectTypes") },
        { ("Objective", 6), new MappingEntry(MappingKind.Template, "quest") },
        { ("Objective", 30), new MappingEntry(MappingKind.Disabled) },
        { ("Objective", 31), new MappingEntry(MappingKind.Template, "entity") },
        { ("Objective", 32), new MappingEntry(MappingKind.Template, "entity") },

        // === Reward Types ===
        { ("Reward", 1), new MappingEntry(MappingKind.Template, "item") },
        { ("Reward", 2), new MappingEntry(MappingKind.Template, "quest") },
        { ("Reward", 3), new MappingEntry(MappingKind.Template, "quest") },
        { ("Reward", 4), new MappingEntry(MappingKind.Disabled) },
    };
}
