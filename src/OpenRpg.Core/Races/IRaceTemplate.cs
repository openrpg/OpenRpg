using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Races
{
    public interface IRaceTemplate : IHasDataId, IHasAssetCode, IHasEffects, IHasRequirements, IHasLocaleDescription
    {
        
    }
}