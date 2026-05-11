using System.Collections.Generic;
using OpenRpg.Editor.Core.Genres;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Models;

public class EditorState
{
    public LoadedProject CurrentProject { get; set; }
    public IReadOnlyList<IGenrePlugin> AvailableGenres { get; set; } = new List<IGenrePlugin>();
    public IReadOnlyList<IGenrePlugin> EnabledGenres { get; set; } = new List<IGenrePlugin>();

    public bool HasGenresLoaded => AvailableGenres.Count > 0;
    public bool HasEnabledGenres => EnabledGenres.Count > 0;

    public IGenreTypesProvider CurrentTypesProvider { get; set; }

    public bool IsProjectLoaded => CurrentProject != null;
}
