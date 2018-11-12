namespace OpenRpg.Genres.Fantasy.Stats.Attributes
{
    public interface IAttributeStats
    {
        byte Strength { get; }
        byte Dexterity { get; }
        byte Constitution { get; }
        byte Intelligence { get; }
        byte Wisdom { get; }
        byte Charisma { get; }
    }
}