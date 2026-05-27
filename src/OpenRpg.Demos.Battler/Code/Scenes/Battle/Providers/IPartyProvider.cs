using System.Collections.Generic;
using System.Threading.Tasks;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Providers;

public interface IPartyProvider
{
    Task<List<BattleEntity>> BuildPartyAsync();
}
