using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests.Factions
{
    public class DefaultFaction : IFaction
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; } = string.Empty;
        public string DescriptionLocaleId { get; set; } = string.Empty;
        public IFactionVariables Variables { get; set; } = new DefaultFactionVariables();
    }
}