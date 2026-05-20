using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public interface IPartyProvider
{
    Task<List<BattleEntity>> BuildPartyAsync();
}
