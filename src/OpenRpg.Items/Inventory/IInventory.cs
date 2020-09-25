using System.Collections.Generic;

namespace OpenRpg.Items.Inventory
{
    public interface IInventory
    {
        /// <summary>
        /// This will attempt to add the item to the inventory in the best way possible, i.e adding to existing stacks
        /// </summary>
        /// <param name="itemToAdd">The item you want to add</param>
        /// <returns>true if the item could be added/stacked false if not</returns>
        /// <remarks>
        /// The item passed in should be seen as immutable for the case of this method, in most cases the item
        /// would be cloned internally and assigned (depending on implementation) but the idea is that you can
        /// add items without fear of the source item being modified in any way (i.e quest rewards, loot drops)
        /// as you wouldnt want the source data modified.
        ///
        /// Depending on the implementation different metadata would be factored in by the default version supports
        /// amount, max stacking, max slots, weight, max weight scenarios.
        /// </remarks>
        bool AddItem(IItem itemToAdd);
        
        /// <summary>
        /// Attempts to remove the item from the inventory in the best way possible, i.e removing from existing stacks
        /// </summary>
        /// <param name="itemToRemove">The item you want to remove</param>
        /// <returns>true if the item could be removed false if not (i.e it isnt in the inventory)</returns>
        bool RemoveItem(IItem itemToRemove);
        
        /// <summary>
        /// Gets the item by its index in the inventory
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>The item instance or null</returns>
        IItem GetItem(int index);
        
        IReadOnlyCollection<IItem> Items { get; }
        IInventoryVariables Variables { get; }
    }
}