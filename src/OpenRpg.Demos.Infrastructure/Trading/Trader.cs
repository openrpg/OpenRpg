using System.Collections.Generic;
using OpenRpg.Items.TradeSkills.Trading;

namespace OpenRpg.Demos.Infrastructure.Trading
{
    public class Trader : ITrader
    {
        public IReadOnlyList<ItemTradeEntry> Items { get; }
        public string Name { get; }

        public Trader(string name, IReadOnlyList<ItemTradeEntry> items)
        {
            Name = name;
            Items = items;
        }
    }
}
