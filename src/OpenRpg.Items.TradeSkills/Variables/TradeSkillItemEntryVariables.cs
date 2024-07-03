using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class TradeSkillItemEntryVariables : ObjectVariables
    {
        public TradeSkillItemEntryVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillItemEntryVariables, internalVariables)
        {
        }
    }
}