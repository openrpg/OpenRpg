using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Factions
{
    public class DefaultFactionReputation : DefaultVariables<int>, IFactionReputation
    {
        public DefaultFactionReputation(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.FactionReputationVariables, internalVariables)
        {
        }
    }
}