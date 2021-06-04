using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Quests
{
    public class DefaultQuestVariables : DefaultVariables<object>, IQuestVariables
    {
        public DefaultQuestVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}