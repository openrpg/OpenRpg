using System.Collections.Generic;

namespace OpenRpg.Quests.Factions
{
    public class FactionReputation : IFactionReputation
    {
        public Dictionary<int, int> InternalFactionReputation { get; set; } = new Dictionary<int, int>();

        public int GetReputation(int factionId)
        { return InternalFactionReputation.TryGetValue(factionId, out var value) ? value : 0; }

        public int ChangeReputation(int factionId, int amount)
        {
            if (!InternalFactionReputation.ContainsKey(factionId))
            {
                InternalFactionReputation.Add(factionId, amount);
                return amount;
            }

            var current = InternalFactionReputation[factionId];
            var newTotal = current + amount;
            InternalFactionReputation[factionId] = newTotal;
            return newTotal;
        }
    }
}