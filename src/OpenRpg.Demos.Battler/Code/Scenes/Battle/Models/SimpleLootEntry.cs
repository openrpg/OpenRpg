namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;

/// <summary>
/// A simple loot entry for monster drop tables.
/// Stored in EntityTemplate Variables at key DemoEntityVariableTypes.LootTable (100).
/// </summary>
public class SimpleLootEntry
{
    /// <summary>
    /// The ItemTemplate ID to drop.
    /// </summary>
    public int ItemTemplateId { get; set; }

    /// <summary>
    /// Drop rate from 0.0 (0%) to 1.0 (100%).
    /// </summary>
    public float DropRate { get; set; }
}
