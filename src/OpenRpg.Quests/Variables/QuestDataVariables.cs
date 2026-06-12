using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Quests.Variables
{
    public class QuestDataVariables : ObjectVariables, ITemplateDataVariables
    {
        public QuestDataVariables(IDictionary<int, object> internalVariables = null)
            : base(0, internalVariables)
        {
        }
    }
}
