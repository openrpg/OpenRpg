namespace OpenRpg.Genres.Fantasy.Stats.Attributes
{
    public class AttributeStats : IAttributeStats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public static IAttributeStats Default { get; } = new AttributeStats
        {
            Strength = 10,
            Wisdom = 10,
            Dexterity = 10,
            Charisma = 10,
            Constitution = 10,
            Intelligence = 10
        };
    }
}