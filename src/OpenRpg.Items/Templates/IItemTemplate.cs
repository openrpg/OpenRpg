using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Localization;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Items.Templates
{
    public interface IItemTemplate : IHasDataId, IHasAssetCode, IHasLocaleDescription, IHasRequirements, IHasEffects, IAllowsModification
    {
        int Type { get; }
        int Value { get; }
    }
}