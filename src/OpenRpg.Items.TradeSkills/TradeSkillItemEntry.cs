using OpenRpg.Core.Common;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillItemEntry : IHasVariables<TradeSkillItemEntryVariables>, IHasTemplate<IItemTemplate>
    {
        public IItemTemplate Template { get; set; }

        public TradeSkillItemEntryVariables Variables { get; set; } = new DefaultTradeSkillItemEntryVariables();
    }
}