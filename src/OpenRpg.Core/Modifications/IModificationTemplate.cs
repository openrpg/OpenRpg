using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Modifications
{
    public interface IModificationTemplate : IDataTemplate, IHasEffects
    {
        int ModificationType { get; }
    }
}