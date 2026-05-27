using System;
using System.Collections.Generic;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Services;

public class LootService : ILootService
{
    private readonly IDataSource _dataSource;
    private readonly Random _random = new();

    public LootService(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public List<ItemData> GenerateLoot(IReadOnlyList<BattleEntity> defeatedEnemies)
    {
        var loot = new List<ItemData>();

        foreach (var enemy in defeatedEnemies)
        {
            var template = _dataSource.Get<EntityTemplate>(enemy.Entity.TemplateId);
            if (template == null) continue;

            if (!template.Variables.ContainsKey(DemoEntityVariableTypes.LootTable)) 
                continue;

            var lootEntries = template.Variables[DemoEntityVariableTypes.LootTable] as List<SimpleLootEntry>;
            if (lootEntries == null || lootEntries.Count == 0) 
                continue;

            foreach (var entry in lootEntries)
            {
                if (_random.NextDouble() < entry.DropRate)
                {
                    var itemTemplate = _dataSource.Get<ItemTemplate>(entry.ItemTemplateId);
                    if (itemTemplate == null) continue;

                    loot.Add(new ItemData { TemplateId = entry.ItemTemplateId });
                }
            }
        }

        return loot;
    }
}
