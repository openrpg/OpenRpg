using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Editors;

public interface ITemplateSectionRenderer
{
    string SectionType { get; }
    int Priority { get; }
    bool CanHandle(SectionDefinition section, PropertyInfo property, Type templateType);
    RenderFragment Render(object template, PropertyInfo property, SectionDefinition section);
}