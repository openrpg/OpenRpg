using OpenRpg.Core.Common;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests.Factions
{
    public interface IFaction : IHasDataId, IHasLocaleDescription, IHasVariables<IFactionVariables>
    {
    }
}