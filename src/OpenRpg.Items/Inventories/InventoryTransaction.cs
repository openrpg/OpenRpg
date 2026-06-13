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

        public List<ItemData> Additions { get; } = new List<ItemData>();
        public List<ItemData> Removals { get; } = new List<ItemData>();

        public void AddItem(ItemData itemData) => Additions.Add(itemData);
        public void RemoveItem(ItemData itemData) => Removals.Add(itemData);

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
                var itemInstance = new Item() { Data = itemToAddBack, Template = TemplateAccessor.GetItemTemplate(itemToAddBack.TemplateId) };
                Inventory.AttemptAddItem(itemInstance);
            }
        }
        
        public void RevertAdditionsUpTo(int lastProcessedIndex)
        {
            for (var i = 0; i <= lastProcessedIndex; i++)
            {
                var itemToRemove = Additions[i];
                if (Inventory.AttemptRemoveItem(itemToRemove))
                { continue; }

                if (!itemToRemove.Variables.HasAmount() && !itemToRemove.Variables.HasWeight())
                {
                    var matching = Inventory.Items.LastOrDefault(x => x.TemplateId == itemToRemove.TemplateId);
                    if (matching != null) { Inventory.Items.Remove(matching); }
                }
            }
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
                var itemInstance = new Item() { Data = itemToAdd, Template = TemplateAccessor.GetItemTemplate(itemToAdd.TemplateId) };
                if (!Inventory.AttemptAddItem(itemInstance))
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