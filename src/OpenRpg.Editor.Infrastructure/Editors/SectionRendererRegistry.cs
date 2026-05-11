using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Editors;

public class SectionRendererRegistry
{
    private readonly List<ITemplateSectionRenderer> _renderers = new();

    public SectionRendererRegistry()
    {
        RegisterBuiltInRenderers();
    }

    private void RegisterBuiltInRenderers()
    {
        _renderers.Add(new ExplicitEditorRenderer());
        _renderers.Add(new EnumDropdownRenderer());
        _renderers.Add(new CollectionRenderer());
        _renderers.Add(new ScalarFieldRenderer());
    }

    public void Register(ITemplateSectionRenderer renderer)
    {
        _renderers.Add(renderer);
        _renderers.Sort((a, b) => b.Priority.CompareTo(a.Priority));
    }

    public ITemplateSectionRenderer GetRenderer(SectionDefinition section, PropertyInfo property, Type templateType)
    {
        return _renderers.FirstOrDefault(r => r.CanHandle(section, property, templateType));
    }

    public IEnumerable<ITemplateSectionRenderer> GetAllRenderers() => _renderers;

    public RenderFragment RenderSection(object template, PropertyInfo property, SectionDefinition section, Type templateType)
    {
        var renderer = GetRenderer(section, property, templateType);
        return renderer?.Render(template, property, section) ?? RenderFallback(property);
    }

    private RenderFragment RenderFallback(PropertyInfo property)
    {
        return builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "notification is-warning");
            builder.AddContent(2, $"No renderer found for property: {property.Name}");
            builder.CloseElement();
        };
    }
}

public class ExplicitEditorRenderer : ITemplateSectionRenderer
{
    public string SectionType => "explicit";
    public int Priority => 100;

    public bool CanHandle(SectionDefinition section, PropertyInfo property, Type templateType)
    {
        return !string.IsNullOrEmpty(section.EditorComponent);
    }

    public RenderFragment Render(object template, PropertyInfo property, SectionDefinition section)
    {
        return builder =>
        {
            builder.OpenComponent(0, Type.GetType($"OpenRpg.Editor.UI.Components.Editors.List.{section.EditorComponent}")
                ?? throw new InvalidOperationException($"Editor component {section.EditorComponent} not found"));
            builder.AddAttribute(1, "Template", template);
            builder.CloseComponent();
        };
    }
}

public class EnumDropdownRenderer : ITemplateSectionRenderer
{
    public string SectionType => "enumDropdown";
    public int Priority => 40;

    public bool CanHandle(SectionDefinition section, PropertyInfo property, Type templateType)
    {
        var editorType = section.EditorType ?? TemplateEditorConventions.GetEditorType(property.Name);
        return editorType == "enumDropdown" || (property.PropertyType == typeof(int) && !TemplateEditorConventions.IsCollection(property));
    }

    public RenderFragment Render(object template, PropertyInfo property, SectionDefinition section)
    {
        return builder =>
        {
            var typeSource = section.Options.GetValueOrDefault("typeSource") 
                ?? TemplateEditorConventions.GetTypeSource(property.Name)
                ?? "itemTypes";

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "field");
            
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "control");
            builder.OpenElement(4, "div");
            builder.AddAttribute(5, "class", "select is-fullwidth");
            
            builder.OpenElement(6, "select");
            builder.AddAttribute(7, "value", property.GetValue(template));
            builder.AddAttribute(8, "onchange", EventCallback.Factory.Create<ChangeEventArgs>(template, e =>
            {
                property.SetValue(template, int.Parse(e.Value?.ToString() ?? "0"));
            }));

            builder.AddContent(9, $"<!-- {typeSource} options will be injected via GenreTypesService -->");
            
            builder.CloseElement();
            builder.CloseElement();
            builder.CloseElement();
            builder.CloseElement();
        };
    }
}

public class CollectionRenderer : ITemplateSectionRenderer
{
    public string SectionType => "collection";
    public int Priority => 30;

    public bool CanHandle(SectionDefinition section, PropertyInfo property, Type templateType)
    {
        var editorType = section.EditorType ?? TemplateEditorConventions.GetEditorType(property.Name);
        return editorType == "collection" || TemplateEditorConventions.IsCollection(property);
    }

    public RenderFragment Render(object template, PropertyInfo property, SectionDefinition section)
    {
        var componentName = section.EditorComponent;
        if (string.IsNullOrEmpty(componentName))
        {
            var elementType = TemplateEditorConventions.GetCollectionElementType(property);
            componentName = TemplateEditorConventions.GetCollectionComponent(elementType);
        }

        return builder =>
        {
            if (!string.IsNullOrEmpty(componentName))
            {
                var componentType = Type.GetType($"OpenRpg.Editor.UI.Components.Editors.List.{componentName}");
                if (componentType != null)
                {
                    builder.OpenComponent(0, componentType);
                    
                    var collectionValue = property.GetValue(template) as System.Collections.IEnumerable;
                    var genericType = TemplateEditorConventions.GetCollectionElementType(property);
                    var listType = typeof(List<>).MakeGenericType(genericType);
                    var listValue = collectionValue as System.Collections.IList ?? 
                        (collectionValue != null ? System.Linq.Enumerable.ToList(collectionValue.Cast<object>()) : null);
                    
                    var prop = componentType.GetProperty(property.Name);
                    if (prop != null)
                    {
                        builder.AddAttribute(1, property.Name, listValue ?? Activator.CreateInstance(listType));
                    }
                    builder.CloseComponent();
                }
                else
                {
                    builder.AddContent(2, $"[Collection Editor: {componentName} not found]");
                }
            }
            else
            {
                builder.AddContent(3, $"[Collection Editor: {property.Name}]");
            }
        };
    }
}

public class ScalarFieldRenderer : ITemplateSectionRenderer
{
    public string SectionType => "scalar";
    public int Priority => 10;

    public bool CanHandle(SectionDefinition section, PropertyInfo property, Type templateType)
    {
        var editorType = section.EditorType ?? TemplateEditorConventions.GetEditorType(property.Name);
        return editorType == "scalar" || 
               (!string.IsNullOrEmpty(editorType) && editorType != "enumDropdown" && editorType != "collection");
    }

    public RenderFragment Render(object template, PropertyInfo property, SectionDefinition section)
    {
        return builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "field");
            builder.AddContent(2, $"[{property.Name}: {property.PropertyType.Name}]");
            builder.CloseElement();
        };
    }
}