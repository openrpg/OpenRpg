using System.Collections.Generic;
using OpenRpg.Items.Modifications;

namespace OpenRpg.Items
{
    public interface IItem
    {
        string NameOverride { get; }
        int Amount { get; }
        
        IItemTemplate ItemTemplate { get; }
        IEnumerable<IModification> Modifications { get; }
    }
}