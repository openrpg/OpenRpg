using System.Collections.Generic;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Providers;

public interface IPartyProvider
{
    List<BattleEntity> BuildParty();
}
