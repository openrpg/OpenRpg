using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultFactionVariables : ObjectVariables, IFactionVariables
    {
        public DefaultFactionVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.FactionVariables, internalVariables)
        {
        }
    }
}