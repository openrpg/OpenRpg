using System.Collections.Generic;

namespace OpenRpg.Items.TradeSkills.Trading
{
    public interface ITrader
    {
        IReadOnlyList<ItemTradeEntry> Items { get; }
    }
}