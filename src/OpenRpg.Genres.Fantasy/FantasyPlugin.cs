using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Core.Genres;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Types;
using OpenRpg.Quests.Types;

namespace OpenRpg.Genres.Fantasy;

public class FantasyPlugin : IGenrePlugin
{
    public string Id => "fantasy";
    public string Name => "Fantasy RPG";
    public string Version => "1.0.0";
    public string Description => "Classic fantasy RPG types and mechanics";

    public IGenreTypesProvider TypesProvider { get; } = new FantasyTypesProvider();
    public IGenreRuntimeServices RuntimeServices { get; } = new FantasyRuntimeServices();

    public void Initialize(IServiceCollection services)
    {
        RuntimeServices.RegisterServices(services);
    }
}

public class FantasyTypesProvider : IGenreTypesProvider
{
    public string GenreId => "fantasy";
    public string GenreName => "Fantasy RPG";

    private static OptionData[] GetTypesFor(Type typesObject)
    {
        var optionData = new List<OptionData>();
        var fields = typesObject.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        foreach (var field in fields)
        {
            var value = (int)field.GetValue(null);
            optionData.Add(new OptionData(value, MakeReadable(field.Name)));
        }
        return optionData.ToArray();
    }

    private static string MakeReadable(string name)
    {
        var result = new List<char>();
        for (int i = 0; i < name.Length; i++)
        {
            var c = name[i];
            if (i > 0 && char.IsUpper(c) && !char.IsUpper(name[i - 1]))
            {
                result.Add(' ');
            }
            result.Add(c);
        }
        return new string(result.ToArray());
    }

    public OptionData[] ItemTypes => GetTypesFor(typeof(FantasyItemTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] ItemQualityTypes => GetTypesFor(typeof(FantasyItemQualityTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] RequirementTypes => GetTypesFor(typeof(FantasyRequirementTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] EffectTypes => GetTypesFor(typeof(FantasyEffectTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] RewardTypes => GetTypesFor(typeof(FantasyRewardTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] ModificationTypes => GetTypesFor(typeof(FantasyModificationTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] ObjectiveTypes => GetTypesFor(typeof(GenresObjectiveTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] EffectScalingType => GetTypesFor(typeof(OpenRpg.Entities.Types.CoreEffectScalingTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] StatTypes => GetTypesFor(typeof(FantasyEntityStatsVariableTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] StateTypes => GetTypesFor(typeof(FantasyEntityStateVariableTypes)).OrderBy(x => x.Id).ToArray();
    public OptionData[] CraftingSkillTypes => GetTypesFor(typeof(FantasyTradeSkillTypes)).Where(x => x.Id < 30).OrderBy(x => x.Id).ToArray();
    public OptionData[] GatheringSkillTypes => GetTypesFor(typeof(FantasyTradeSkillTypes)).Where(x => x.Id >= 30).OrderBy(x => x.Id).ToArray();
}

public class FantasyRuntimeServices : IGenreRuntimeServices
{
    public void RegisterServices(IServiceCollection services)
    {
    }
}
