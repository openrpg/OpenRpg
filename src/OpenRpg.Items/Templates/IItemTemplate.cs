using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Localization;
using OpenRpg.Core.Requirements;
using OpenRpg.Effects;
using OpenRpg.Items.Modifications;

namespace OpenRpg.Items
{
    public interface IItemTemplate : IHasDataId, IHasAssetCode, IHasLocaleDescription, IHasRequirements, IHasEffects
    {
        int Type { get; }
        int Value { get; }
        
        IEnumerable<IModificationAllowance> ModificationAllowances { get; }
    }
}