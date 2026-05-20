using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenRpg.Data;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class EnemyFormationProvider : IEnemyFormationProvider
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly ICharacterPopulator _characterPopulator;
    private readonly Random _random = new();

    public EnemyFormationProvider(IDataSource dataSource, ILocaleDataSource localeDataSource, ICharacterPopulator characterPopulator)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
        _characterPopulator = characterPopulator;
    }

    public Task<List<BattleEntity>> GenerateFormationAsync()
    {
        var allMonsters = _dataSource.GetAll<EntityTemplate>().ToList();
        var count = 6;
        var entities = new List<BattleEntity>();

        for (var i = 0; i < count; i++)
        {
            var template = allMonsters[_random.Next(allMonsters.Count)];
            var name = _localeDataSource.Get("en-gb", template.NameLocaleId);
            var assetCode = template.Variables.HasAssetCode() ? template.Variables.AssetCode : "";

            var character = new Character();
            character.TemplateId = template.Id;
            character.NameLocaleId = template.NameLocaleId;
            character.Variables.AssetCode = assetCode;
            _characterPopulator.Populate(character, refreshState: true);

            entities.Add(new BattleEntity
            {
                Entity = character,
                Name = name,
                AssetCode = assetCode,
                Team = Team.Enemy,
                Row = i / 2,
                SlotInRow = i % 2
            });
        }

        return Task.FromResult(entities);
    }
}
