using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Localization;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Characters
{
    public interface IRaceTemplate : IHasDataId, IHasAssetCode, IHasEffects, IHasRequirements, IHasLocaleDescription
    {
        
    }
}