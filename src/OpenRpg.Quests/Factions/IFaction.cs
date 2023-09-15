using OpenRpg.Core.Common;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests.Factions
{
    public interface IFaction : IHasDataId, IHasLocaleDescription
    {
        IFactionVariables Variables { get; }
    }
}