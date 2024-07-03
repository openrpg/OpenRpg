using System.Collections.Generic;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class IInventoryTransactionExtensions
    {
        public static IInventoryTransaction AddItems(this IInventoryTransaction transaction, IEnumerable<Item> items)
        {
            foreach (var item in items)
            { transaction.AddItem(item); }

            return transaction;
        }
        
        public static IInventoryTransaction RemoveItems(this IInventoryTransaction transaction, IEnumerable<Item> items)
        {
            foreach (var item in items)
            { transaction.RemoveItem(item); }

            return transaction;
        }
    }
}