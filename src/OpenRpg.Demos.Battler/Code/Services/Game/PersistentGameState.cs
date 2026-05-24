using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Builders;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Battler.Code.Services.Game;

public class PersistentGameState : IPersistentGameState
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly GameCharacterBuilder _characterBuilder;
    private List<BattleEntity> _party;
    private List<ItemData> _sharedInventory;

    public bool IsInitialized => _party != null;

    public PersistentGameState(IDataSource dataSource, ILocaleDataSource localeDataSource, GameCharacterBuilder characterBuilder)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _characterBuilder = characterBuilder;
    }

    public List<BattleEntity> Party
    {
        get
        {
            if (!IsInitialized) InitializeParty();
            return _party;
        }
    }

    public List<ItemData> SharedInventory
    {
        get
        {
            if (!IsInitialized) InitializeParty();
            return _sharedInventory;
        }
    }

    public BattleEntity GetCharacter(int index)
    {
        if (!IsInitialized) InitializeParty();
        return index >= 0 && index < _party.Count ? _party[index] : null;
    }

    public void InitializeParty()
    {
        // Idempotent — only initializes once. This preserves loot and party state
        // between battles. Call ReinitializeParty() to force a full reset.
        if (_party != null) return;

        var partyIds = ClassLookups.GetRandomPartyIds();
        BuildPartyFromIds(partyIds);
    }

    public void InitializeParty(int[] classIds)
    {
        // Allows the party creation scene to specify exact class composition.
        // Always resets — no idempotent guard, because this is called fresh from the creation scene.
        ResetParty();
        BuildPartyFromIds(classIds);
    }

    private void BuildPartyFromIds(int[] classIds)
    {
        var entities = new List<BattleEntity>();
        var slotIndex = 0;

        foreach (var classId in classIds)
        {
            var template = _dataSource.Get<ClassTemplate>(classId);
            if (template == null) continue;

            var name = _localeDataSource.Get("en-gb", template.NameLocaleId);

            var character = _characterBuilder
                .CreateNew()
                .WithRaceId(RaceLookups.Human)
                .WithClassId(classId, 1)
                .WithName(name)
                .Build();

            character.NameLocaleId = template.NameLocaleId;
            var assetCode = character.Variables.AssetCode;

            if (template.Variables.HasAbilities())
                character.Variables[CombatTemplateVariableTypes.Abilities] = template.Variables.Abilities.ToList();

            entities.Add(new BattleEntity
            {
                Entity = character,
                Name = name,
                AssetCode = assetCode,
                Team = Team.Player,
                Row = slotIndex < 2 ? 0 : 1,
                SlotInRow = slotIndex % 2
            });

            slotIndex++;
        }

        _party = entities;
        _sharedInventory = new List<ItemData>();
        AddStarterItems();
    }

    public void FullHealParty()
    {
        if (_party == null) return;
        foreach (var entity in _party)
        {
            entity.Hp = entity.MaxHp;
            entity.Entity.State.Mana = entity.MaxMana;
        }
    }

    public void ResetParty()
    {
        _party = null;
        _sharedInventory = null;
    }

    private void AddStarterItems()
    {
        // Give the party some starter items in shared inventory
        var potionTemplate = _dataSource.Get<ItemTemplate>(20); // Potion
        if (potionTemplate != null)
        {
            var potion = new ItemData { TemplateId = potionTemplate.Id };
            _sharedInventory.Add(potion);
            _sharedInventory.Add(potion);
            _sharedInventory.Add(potion);
        }

        var etherTemplate = _dataSource.Get<ItemTemplate>(22); // Ether
        if (etherTemplate != null)
        {
            var ether = new ItemData { TemplateId = etherTemplate.Id };
            _sharedInventory.Add(ether);
        }

        var pDownTemplate = _dataSource.Get<ItemTemplate>(23); // Phoenix Down
        if (pDownTemplate != null)
        {
            var pDown = new ItemData { TemplateId = pDownTemplate.Id };
            _sharedInventory.Add(pDown);
        }
    }
}
