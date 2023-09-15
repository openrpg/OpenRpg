using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillItemEntry
    {
        public int ItemTemplateId { get; set; }

        public TradeSkillItemEntryVariables Variables { get; set; } = new DefaultTradeSkillItemEntryVariables();
    }
}