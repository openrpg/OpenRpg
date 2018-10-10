using System.Collections.Generic;
using OpenRpg.Core.Modifications;
using OpenRpg.Items.Templates;

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