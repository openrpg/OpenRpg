using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Modifications
{
    public interface IModification : IHasDataId, IHasEffects, IHasLocaleDescription
    {
        int ModificationType { get; }
    }
}