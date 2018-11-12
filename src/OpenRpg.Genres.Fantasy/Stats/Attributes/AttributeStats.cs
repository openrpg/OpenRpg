namespace OpenRpg.Genres.Fantasy.Stats.Attributes
{
    public class AttributeStats : IAttributeStats
    {
        public byte Strength { get; set; }
        public byte Dexterity { get; set; }
        public byte Constitution { get; set; }
        public byte Intelligence { get; set; }
        public byte Wisdom { get; set; }
        public byte Charisma { get; set; }

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