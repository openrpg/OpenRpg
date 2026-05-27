using System.Collections.Generic;

namespace OpenRpg.Entities.Procedural.Patterns;

public class FixedPatternLocaleGenerator : IPatternLocaleGenerator
{
    public string NamePattern { get; set; } = "{0}-{1}-name";
    public string DescriptionPattern { get; set; } = "{0}-{1}-description";
    
    public Dictionary<int, string> PatternNames { get; set; } = new();

    public string GenerateNameLocaleId(int patternId, string typeCode)
    {
        var patternIdName = PatternNames[patternId];
        return string.Format(NamePattern, patternIdName, typeCode);   
    }

    public string GenerateDescriptionLocaleId(int patternId, string typeCode)
    {
        var patternIdName = PatternNames[patternId];
        return string.Format(DescriptionPattern, patternIdName, typeCode);   
    }
}