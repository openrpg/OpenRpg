using System.Collections.Generic;
using System.Linq;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Services;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

/// <summary>
/// Handles post-battle results: loot generation, victory/defeat routing, and results message building.
/// Extracted from BattleScene for separation of concerns.
/// </summary>
public class BattleResultsHandler
{
    private readonly ILootService _lootService;
    private readonly IPersistentGameState _gameState;
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;

    public List<ItemData> LootItems { get; private set; } = [];
    public string VictoryMessage { get; private set; } = "";

    public BattleResultsHandler(
        ILootService lootService,
        IPersistentGameState gameState,
        IDataSource dataSource,
        ILocaleDataSource localeDataSource)
    {
        _lootService = lootService;
        _gameState = gameState;
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
    }

    public void CollectLoot(List<BattleEntity> deadEnemies)
    {
        LootItems = _lootService.GenerateLoot(deadEnemies);
        _gameState.SharedInventory.AddRange(LootItems);
        VictoryMessage = BuildLootMessage();
    }

    public void Reset()
    {
        LootItems = [];
        VictoryMessage = "";
    }

    private string BuildLootMessage()
    {
        if (LootItems.Count == 0)
            return "No items were dropped.";

        var itemNames = new List<string>();
        foreach (var item in LootItems)
        {
            var template = _dataSource.Get<ItemTemplate>(item.TemplateId);
            var name = template != null
                ? _localeDataSource.Get("en-gb", template.NameLocaleId)
                : $"Item #{item.TemplateId}";
            itemNames.Add(name);
        }

        return $"Loot collected: {string.Join(", ", itemNames)}";
    }
}
