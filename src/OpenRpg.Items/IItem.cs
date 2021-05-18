using System.Collections.Generic;
using OpenRpg.Core.Modifications;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items
{
    public interface IItem
    {
        IItemTemplate ItemTemplate { get; }
        IEnumerable<IModification> Modifications { get; }
        IItemVariables Variables { get; }
    }
}