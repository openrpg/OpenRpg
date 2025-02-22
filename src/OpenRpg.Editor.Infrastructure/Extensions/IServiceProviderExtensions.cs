using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Localization;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class IServiceProviderExtensions
{
    public static void SetupAppData(this IServiceProvider serviceProvider)
    {
        var dataSource = serviceProvider.GetService<EditorDatasource>();
        dataSource.Database.Add(typeof(ItemTemplate), new Dictionary<object, object>());
        dataSource.Database.Add(typeof(RaceTemplate), new Dictionary<object, object>());
        dataSource.Database.Add(typeof(ClassTemplate), new Dictionary<object, object>());
        dataSource.Database.Add(typeof(Quest), new Dictionary<object, object>());
        dataSource.Database.Add(typeof(ItemCraftingTemplate), new Dictionary<object, object>());
        dataSource.Database.Add(typeof(ItemGatheringTemplate), new Dictionary<object, object>());

        var localeDataSource = serviceProvider.GetService<EditorLocaleDatasource>();
        localeDataSource.LocaleDatasets.Add("en-gb", new LocaleDataset() { LocaleCode = "en-gb" });

    }
}