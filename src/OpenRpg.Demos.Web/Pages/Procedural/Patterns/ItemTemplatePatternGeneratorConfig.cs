using OpenRpg.Entities.Procedural.Effects;
using OpenRpg.Entities.Procedural.Patterns;

namespace OpenRpg.Demos.Web.Pages.Procedural.Patterns;

public class ItemTemplatePatternGeneratorConfig : PatternGeneratorVariables
{
    public int StartingId { get; set; }
    public int ItemType { get; set; }
    public ProceduralEffects Effects { get; set; }
}