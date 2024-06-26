using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items
{
    public interface IItem : IHasTemplate<IItemTemplate>, IHasVariables<IItemVariables>, IHasModifications<IItemModificationTemplate>
    {

    }
}