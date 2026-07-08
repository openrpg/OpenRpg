using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class QuestTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public QuestTemplateVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.QuestTemplateVariables, internalVariables)
        {
        }
    }
}
