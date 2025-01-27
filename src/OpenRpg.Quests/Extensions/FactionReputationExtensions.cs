using OpenRpg.Core.Extensions;
using OpenRpg.Quests.Factions;

namespace OpenRpg.Quests.Extensions
{
    public static class FactionReputationExtensions
    {
        public static int AddReputation(this FactionReputation factionRep, int factionId, int amountToAdd)
        { return factionRep.AddValue(factionId, amountToAdd); }
    }
}