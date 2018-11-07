using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Items.Templates
{
    public interface IItemTemplate : IHasDataId, IHasAssetCode, IHasLocaleDescription, IHasRequirements, IHasEffects, IAllowsModification
    {
        int ItemType { get; }
        int ItemValue { get; }
    }
}