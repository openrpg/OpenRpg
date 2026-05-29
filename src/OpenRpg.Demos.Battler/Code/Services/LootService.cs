using System.Collections.Generic;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Services;

public class LootService : ILootService
{
    private readonly IDataSource _dataSource;
    private readonly ILootTableProcessor _lootTableProcessor;

    public LootService(IDataSource dataSource, ILootTableProcessor lootTableProcessor)
    {
        _dataSource = dataSource;
        _lootTableProcessor = lootTableProcessor;
    }

    public List<ItemData> GenerateLoot(IReadOnlyList<BattleEntity> defeatedEnemies)
    {
        var loot = new List<ItemData>();

        foreach (var enemy in defeatedEnemies)
        {
            var template = _dataSource.Get<EntityTemplate>(enemy.Entity.TemplateId);
            if (template == null) continue;
            if (!template.Variables.HasLootTable()) continue;

            var lootData = template.Variables.LootTable;
            loot.AddRange(_lootTableProcessor.GetLoot(lootData));
        }

        return loot;
    }
}
