using System.Collections.Generic;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Services;

public interface ILootService
{
    /// <summary>
    /// Generates loot from a list of defeated enemies.
    /// Rolls each enemy's loot table and returns the items that dropped.
    /// </summary>
    List<ItemData> GenerateLoot(IReadOnlyList<BattleEntity> defeatedEnemies);
}
