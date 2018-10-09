using OpenRpg.Core.Common;
using OpenRpg.Core.Localization;
using OpenRpg.Effects;

namespace OpenRpg.Items.Modifications
{
    public interface IModification : IHasDataId, IHasEffects, IHasLocaleDescription
    {
        int ModificationType { get; }
    }
}