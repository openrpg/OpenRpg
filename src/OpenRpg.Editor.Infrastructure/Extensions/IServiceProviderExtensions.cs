using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Races.Templates;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Items.Templates;
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
        
    }
}