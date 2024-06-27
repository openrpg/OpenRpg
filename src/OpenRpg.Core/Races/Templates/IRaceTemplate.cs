using OpenRpg.Core.Effects;
using OpenRpg.Core.Races.Variables;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races.Templates
{
    public interface IRaceTemplate : ITemplate, IHasEffects, IHasRequirements, IHasVariables<IRaceTemplateVariables>
    {
    }
}