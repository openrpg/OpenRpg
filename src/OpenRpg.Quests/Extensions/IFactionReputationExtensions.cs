using OpenRpg.Core.Extensions;
using OpenRpg.Quests.Factions;

namespace OpenRpg.Quests.Extensions
{
    public static class IFactionReputationExtensions
    {
        public static int AddReputation(this IFactionReputation factionRep, int factionId, int amountToAdd)
        { return factionRep.AddValue(factionId, amountToAdd); }
    }
}