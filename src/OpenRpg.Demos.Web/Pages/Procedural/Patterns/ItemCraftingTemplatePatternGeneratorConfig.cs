using OpenRpg.CurveFunctions.Scaling;
using OpenRpg.Entities.Procedural.Patterns;

namespace OpenRpg.Demos.Web.Pages.Procedural.Patterns;

public class ItemCraftingTemplatePatternGeneratorConfig : PatternGeneratorVariables
{
    public int StartingId { get; set; }
    public int SkillType { get; set; }
    public ScalingFunction SkillScore { get; set; }
    public int InputPatternTypeId { get; set; }
    public int OutputPatternTypeId { get; set; }
    public int AmountRequired { get; set; }
    public int AmountOutput { get; set; }
}