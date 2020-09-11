using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Classes
{
    public interface IClassTemplate : IHasDataId, IHasAssetCode, IHasEffects, IHasRequirements, IHasLocaleDescription
    {
        IClassTemplateVariables Variables { get; }
    }
}