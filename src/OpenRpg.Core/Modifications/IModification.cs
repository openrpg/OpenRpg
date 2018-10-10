using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Localization;

namespace OpenRpg.Core.Modifications
{
    public interface IModification : IHasDataId, IHasEffects, IHasLocaleDescription
    {
        int ModificationType { get; }
    }
}