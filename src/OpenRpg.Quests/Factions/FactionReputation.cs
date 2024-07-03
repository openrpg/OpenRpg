using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Factions
{
    public class FactionReputation : Variables<int>
    {
        public FactionReputation(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.FactionReputationVariables, internalVariables)
        {
        }
    }
}