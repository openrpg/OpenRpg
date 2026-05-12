namespace OpenRpg.Editor.UI.Components.Editors;

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

public interface IDynamicTemplateEditor
{
    object Template { get; set; }
    string AssetCodePrefix { get; set; }
    IReadOnlyList<TemplateEditorSection> Sections { get; set; }

    RenderFragment PrimaryDetailsContent { get; set; }
    RenderFragment SecondaryDetailsContent { get; set; }
}

public class TemplateEditorSection
{
    public string Title { get; set; }
    public string Property { get; set; }
    public string EditorType { get; set; }
    public string EditorComponent { get; set; }
    public Dictionary<string, string> Options { get; set; } = new();
    public int Priority { get; set; }
}