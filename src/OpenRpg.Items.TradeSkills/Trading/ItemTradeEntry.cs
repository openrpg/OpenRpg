using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Trading
{
    public class ItemTradeEntry : IHasVariables<ItemTradeEntryVariables>
    {
        public int ItemTemplateId { get; set; }
        
        public float BuyRate { get; set; }
        public float SellRate { get; set; }

        public ItemTradeEntryVariables Variables { get; set; } = new ItemTradeEntryVariables();
    }
}