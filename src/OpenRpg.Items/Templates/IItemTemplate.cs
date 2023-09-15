using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public interface IItemTemplate : IHasDataId, IHasLocaleDescription, IHasRequirements, IHasEffects, IAllowsModification, IHasVariables<IItemTemplateVariables>
    {
        int ItemType { get; }
    }
}