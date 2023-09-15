using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Races.Variables;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races
{
    public interface IRaceTemplate : IHasDataId, IHasEffects, IHasRequirements, IHasLocaleDescription, IHasVariables<IRaceTemplateVariables>
    {
    }
}