using System.Collections.Generic;
using OpenRpg.Combat.Abilities;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

/// <summary>
/// Context for showing the command menu during battle. Consolidates the 8 parameters
/// that were previously passed to Show() individually.
/// </summary>
public record CommandMenuContext
{
    public required BattleEntity Attacker { get; init; }
    public required List<BattleEntity> AliveEnemies { get; init; }
    public required List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> Abilities { get; init; }
    public required ILocaleDataSource LocaleDataSource { get; init; }
    public List<ItemData> InventoryItems { get; init; }
    public List<BattleEntity> AliveParty { get; init; }
    public IDataSource DataSource { get; init; }
    public List<BattleEntity> AllParty { get; init; }
}
