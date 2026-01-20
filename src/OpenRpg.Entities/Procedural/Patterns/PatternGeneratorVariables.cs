using OpenRpg.Core.Variables.General;

namespace OpenRpg.Entities.Procedural.Patterns;

public class PatternGeneratorVariables : ObjectVariables
{
    public int PatternTypeId { get; set; }
    public int[] PatternIds { get; set; }
    
}