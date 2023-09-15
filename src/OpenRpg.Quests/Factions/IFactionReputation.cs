namespace OpenRpg.Quests.Factions
{
    public interface IFactionReputation
    {
        int GetReputation(int factionId);
        int ChangeReputation(int factionId, int amount);
    }
}