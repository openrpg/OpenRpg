using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Classes
{
    public interface IClassTemplate : IHasDataId, IHasEffects, IHasRequirements, IHasLocaleDescription
    {
        IClassTemplateVariables Variables { get; }
    }
}