using System.Collections.Generic;
using OpenRpg.Combat.Abilities;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;

public class PlayerAction
{
    public ActionType Type { get; set; }
    public AbilityTemplate Ability { get; set; }
    public List<BattleEntity> Targets { get; set; } = [];
}
