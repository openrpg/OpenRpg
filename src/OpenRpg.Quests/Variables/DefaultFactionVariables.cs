using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultFactionVariables : DefaultVariables<object>, IFactionVariables
    {
        public DefaultFactionVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.FactionVariables, internalVariables)
        {
        }
    }
}