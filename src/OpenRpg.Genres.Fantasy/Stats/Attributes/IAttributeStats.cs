namespace OpenRpg.Genres.Fantasy.Stats.Attributes
{
    public interface IAttributeStats
    {
        int Strength { get; }
        int Dexterity { get; }
        int Constitution { get; }
        int Intelligence { get; }
        int Wisdom { get; }
        int Charisma { get; }
    }
}