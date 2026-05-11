using System.Collections.Generic;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Models;

public class EditorState
{
    public LoadedProject CurrentProject { get; set; }
    public IReadOnlyList<EditorPluginInfo> AvailableGenres { get; set; } = new List<EditorPluginInfo>();
    public IReadOnlyList<EditorPluginInfo> EnabledGenres { get; set; } = new List<EditorPluginInfo>();

    public bool HasGenresLoaded => AvailableGenres.Count > 0;
    public bool HasEnabledGenres => EnabledGenres.Count > 0;

    public IGenreTypesProvider CurrentTypesProvider { get; set; }

    public bool IsProjectLoaded => CurrentProject != null;
}
