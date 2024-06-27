using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Templates
{
    public interface IClassTemplate : ITemplate, IHasEffects, IHasRequirements, IHasVariables<IClassTemplateVariables>
    {
    }
}