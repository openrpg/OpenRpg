using OpenRpg.Core.Common;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public interface IItemTemplateInstance : ITemplateInstance, IHasVariables<IItemVariables>, IHasModifications<IItemModificationTemplate>, IIsUnique
    {

    }
}