using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenRpg.Core.Effects;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class PartyProvider : IPartyProvider
{
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;

    private static readonly int[] PartyClassIds = [1, 2, 4, 5];

    public PartyProvider(IDataSource dataSource, ILocaleDataSource localeDataSource)
    {
        _dataSource = dataSource;
        _localeDataSource = localeDataSource;
    }

    public Task<List<BattleEntity>> BuildPartyAsync()
    {
        var entities = new List<BattleEntity>();
        var slotIndex = 0;

        foreach (var classId in PartyClassIds)
        {
            var template = _dataSource.Get<ClassTemplate>(classId);
            if (template == null) continue;

            var name = _localeDataSource.Get("en-gb", template.NameLocaleId);
            var assetCode = template.Variables.TryGetValue(CoreAnyVariableTypes.AssetCode, out var code)
                ? code?.ToString() ?? "" : "";
            var hp = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 60) as StaticEffect)
                ?.Potency ?? 30;
            var initiative = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 44) as StaticEffect)
                ?.Potency ?? 1;
            var attackDamage = (int?)(template.Variables.Effects?.FirstOrDefault(e => e.EffectType == 1) as StaticEffect)
                ?.Potency ?? 10;
            var row = slotIndex < 2 ? 0 : 1;

            entities.Add(new BattleEntity
            {
                Name = name,
                AssetCode = assetCode,
                Team = Team.Player,
                Row = row,
                SlotInRow = slotIndex % 2,
                Hp = hp,
                MaxHp = hp,
                Initiative = initiative,
                AttackDamage = attackDamage
            });

            slotIndex++;
        }

        return Task.FromResult(entities);
    }
}
