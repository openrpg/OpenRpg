using OpenRpg.Entities.Procedural.Patterns;

namespace OpenRpg.Demos.Web.Pages.Procedural.Patterns;

public class FixedNamePatternLocaleGenerator : IPatternLocaleGenerator
{
    public string NamePattern { get; set; } = "{0} {1}";
    public Dictionary<int, string> PatternNames { get; set; } = new();

    public string GenerateNameLocaleId(int patternId, string typeCode)
    {
        var patternIdName = PatternNames[patternId];
        return string.Format(NamePattern, patternIdName, typeCode);   
    }
}