using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;

namespace OpenRpg.Core.Modifications
{
    public interface IModificationTemplate : IDataTemplate, IHasEffects
    {
        int ModificationType { get; }
    }
}