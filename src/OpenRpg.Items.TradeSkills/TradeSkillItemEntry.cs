using OpenRpg.Core.Templates;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillItemEntry : ITemplateData<TradeSkillItemEntryVariables>
    {
        public int TemplateId { get; set; }
        
        public TradeSkillItemEntryVariables Variables { get; set; } = new TradeSkillItemEntryVariables();
    }
}