using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Trading
{
    public class ItemTradeEntry
    {
        public int ItemTemplateId { get; set; }
        
        public float BuyRate { get; set; }
        public float SellRate { get; set; }

        private IItemTradeTradeEntryVariables Variables { get; set; } = new DefaultItemTradeEntryVariables();
    }
}