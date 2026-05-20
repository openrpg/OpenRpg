using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenRpg.Core.Effects;
using OpenRpg.Data;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class EnemyFormationProvider : IEnemyFormationProvider
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly Random _random = new();

    public EnemyFormationProvider(IDataSource dataSource, ILocaleDataSource localeDataSource)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
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
            var assetCode = template.Variables.TryGetValue(CoreAnyVariableTypes.AssetCode, out var code)
                ? code?.ToString() ?? "" : "";
            var hp = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 60) as StaticEffect)
                ?.Potency ?? 20;
            var initiative = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 44) as StaticEffect)
                ?.Potency ?? 1;
            var attackDamage = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 1) as StaticEffect)
                ?.Potency ?? 10;
            var row = count <= 2 ? 0 : i / 2;

            entities.Add(new BattleEntity
            {
                Name = name,
                AssetCode = assetCode,
                Team = Team.Enemy,
                Row = row,
                SlotInRow = i % 2,
                Hp = hp,
                MaxHp = hp,
                Initiative = initiative,
                AttackDamage = attackDamage
            });
        }

        return Task.FromResult(entities);
    }
}
