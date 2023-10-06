using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Modifications;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items
{
    public interface IItem : IHasTemplate<IItemTemplate>
    {
        IEnumerable<IModificationTemplate> Modifications { get; }
        IItemVariables Variables { get; }
    }
}