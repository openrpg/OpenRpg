using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes
{
    public interface IClassTemplate : IHasDataId, IHasEffects, IHasRequirements, IHasLocaleDescription, IHasVariables<IClassTemplateVariables>
    {
    }
}