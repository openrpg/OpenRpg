using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillItemEntry : IHasVariables<TradeSkillItemEntryVariables>
    {
        public int ItemTemplateId { get; set; }

        public TradeSkillItemEntryVariables Variables { get; set; } = new DefaultTradeSkillItemEntryVariables();
    }
}