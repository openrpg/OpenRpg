using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillItemEntry : IHasVariables<TradeSkillItemEntryVariables>, IHasTemplateLink
    {
        public int TemplateId { get; set; }
        
        public TradeSkillItemEntryVariables Variables { get; set; } = new TradeSkillItemEntryVariables();
    }
}