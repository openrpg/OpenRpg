namespace OpenRpg.Entities.Procedural.Patterns;

public interface IPatternLocaleGenerator
{
    string GenerateNameLocaleId(int patternId, string typeCode);
}