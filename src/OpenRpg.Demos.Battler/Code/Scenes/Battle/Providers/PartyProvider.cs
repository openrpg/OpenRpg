using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Builders;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Providers;

public class PartyProvider : IPartyProvider
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly GameCharacterBuilder _characterBuilder;

    public PartyProvider(IDataSource dataSource, ILocaleDataSource localeDataSource, GameCharacterBuilder characterBuilder)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _characterBuilder = characterBuilder;
    }

    public Task<List<BattleEntity>> BuildPartyAsync()
    {
        var entities = new List<BattleEntity>();
        var slotIndex = 0;
        var partyIds = ClassLookups.GetRandomPartyIds();

        foreach (var classId in partyIds)
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

            var row = slotIndex < 2 ? 0 : 1;

            entities.Add(new BattleEntity
            {
                Entity = character,
                Name = name,
                AssetCode = assetCode,
                Team = Team.Player,
                Row = row,
                SlotInRow = slotIndex % 2
            });

            slotIndex++;
        }

        return Task.FromResult(entities);
    }
}
