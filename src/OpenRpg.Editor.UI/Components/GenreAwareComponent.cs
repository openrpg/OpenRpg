using Microsoft.AspNetCore.Components;
using OpenRpg.Editor.UI.Services;

namespace OpenRpg.Editor.UI.Components;

public class GenreAwareComponent : ComponentBase
{
    [Inject]
    public GenreTypesService GenreTypes { get; set; }
}
