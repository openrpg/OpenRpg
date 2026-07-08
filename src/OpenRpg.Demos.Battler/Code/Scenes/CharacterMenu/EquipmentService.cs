using System.Collections.Generic;
using System.Linq;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.CharacterMenu;

/// <summary>
/// Handles equipment-related operations: equipping, unequipping, and candidate filtering.
/// Extracted from CharacterMenuScene for testability and separation of concerns.
/// </summary>
public class EquipmentService
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly IEquipmentSlotValidator _slotValidator;
    private readonly ICharacterPopulator _characterPopulator;
    private readonly IPersistentGameState _gameState;

    public EquipmentService(
        IDataSource dataSource,
        ILocaleDataSource localeDataSource,
        IEquipmentSlotValidator slotValidator,
        ICharacterPopulator characterPopulator,
        IPersistentGameState gameState)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _slotValidator = slotValidator;
        _characterPopulator = characterPopulator;
        _gameState = gameState;
    }

    public List<(ItemData Data, ItemTemplate Template)> GetCandidateItems(int slotType)
    {
        var candidates = new List<(ItemData, ItemTemplate)>();
        foreach (var itemData in _gameState.SharedInventory)
        {
            var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
            if (template == null) continue;
            if (_slotValidator.CanEquipItemType(slotType, template.ItemType))
                candidates.Add((itemData, template));
        }
        return candidates;
    }

    public void EquipItem(BattleEntity character, int slotType, ItemData newItem)
    {
        var slots = character.Entity.Variables.Equipment?.Slots;
        if (slots == null) return;

        var currentItem = slots.Get(slotType);

        // Remove new item from inventory
        _gameState.SharedInventory.Remove(newItem);

        // Add old item back to inventory if one was equipped
        if (currentItem != null)
            _gameState.SharedInventory.Add(currentItem);

        // Equip the new item
        slots[slotType] = newItem;
        _characterPopulator.Populate(character.Entity, refreshState: false);
    }

    public void UnequipItem(BattleEntity character, int slotType)
    {
        var slots = character.Entity.Variables.Equipment?.Slots;
        if (slots == null) return;

        var currentItem = slots.Get(slotType);
        if (currentItem == null) return;

        _gameState.SharedInventory.Add(currentItem);
        slots[slotType] = null;
        _characterPopulator.Populate(character.Entity, refreshState: false);
    }

    public string GetItemName(ItemData itemData)
    {
        var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
        return template != null
            ? _localeDataSource.Get("en-gb", template.NameLocaleId)
            : $"Item #{itemData.TemplateId}";
    }

    public string GetItemBonusText(ItemTemplate template)
    {
        var parts = new List<string>();
        if (template.Variables.Effects == null) return "";

        foreach (var effect in template.Variables.Effects)
        {
            if (effect is not OpenRpg.Core.Effects.StaticEffect se) continue;
            if (se.EffectType == Types.BattlerConstants.EffectTypes.DamageBonus)
                parts.Add($" ATK+{se.Potency}");
            else if (se.EffectType == Types.BattlerConstants.EffectTypes.DefenseBonus)
                parts.Add($" DEF+{se.Potency}");
            else if (se.EffectType == Types.BattlerConstants.EffectTypes.HealthBonus)
                parts.Add($" HP+{se.Potency}");
            else if (se.EffectType == Types.BattlerConstants.EffectTypes.MovementSpeedBonus)
                parts.Add($" SPD+{se.Potency}");
            else if (se.EffectType == Types.BattlerConstants.EffectTypes.UnarmedDamageBonus)
                parts.Add($" ATK+{se.Potency}");
        }
        return parts.Count > 0 ? string.Join("", parts) : "";
    }
}
