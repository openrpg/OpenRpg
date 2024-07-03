using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Inventories
{
    public class InventoryTransaction : IInventoryTransaction
    {
        public Inventory Inventory { get; }
        public ITemplateAccessor TemplateAccessor { get; }

        public List<Item> Additions { get; } = new List<Item>();
        public List<Item> Removals { get; } = new List<Item>();

        public void AddItem(Item item) => Additions.Add(item);
        public void RemoveItem(Item item) => Removals.Add(item);

        public InventoryTransaction(Inventory inventory, ITemplateAccessor templateAccessor)
        {
            Inventory = inventory;
            TemplateAccessor = templateAccessor;
        }

        public void RevertRemovalsUpTo(int lastProcessedIndex)
        {
            for (var i = 0; i <= lastProcessedIndex; i++)
            {
                var itemToAddBack = Removals[i];
                Inventory.AttemptAddItem(itemToAddBack, TemplateAccessor.GetItemTemplate(itemToAddBack.TemplateId));
            }
        }
        
        public void RevertAdditionsUpTo(int lastProcessedIndex)
        {
            for (var i = 0; i <= lastProcessedIndex; i++)
            { Inventory.AttemptRemoveItem(Additions[i]); }
        }
        
        public bool ApplyChanges()
        {
            var hasAllItems = Removals.All(Inventory.HasItem);
            if (!hasAllItems) { return false; } 

            for (var i = 0; i < Removals.Count; i++)
            {
                var itemToRemove = Removals[i];
                if (!Inventory.AttemptRemoveItem(itemToRemove))
                { 
                    RevertRemovalsUpTo(i);
                    return false;
                }
            }

            for (var i = 0; i < Additions.Count; i++)
            {
                var itemToAdd = Additions[i];
                if (!Inventory.AttemptAddItem(itemToAdd, TemplateAccessor.GetItemTemplate(itemToAdd.TemplateId)))
                {
                    RevertAdditionsUpTo(i);
                    RevertRemovalsUpTo(Removals.Count-1);
                    return false;
                }
            }

            return true;
        }
    }
}