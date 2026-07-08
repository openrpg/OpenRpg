namespace OpenRpg.Entities.Procedural.Patterns;

public interface IPatternLocaleGenerator
{
    string GenerateNameLocaleId(int patternId, string typeCode);
    string GenerateDescriptionLocaleId(int patternId, string typeCode);
}