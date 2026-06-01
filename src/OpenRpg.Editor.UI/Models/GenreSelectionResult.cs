using System.Collections.Generic;

namespace OpenRpg.Editor.UI.Models;

public record GenreSelectionResult(List<string> EnabledGenreIds, bool GenerateTypeFiles);
