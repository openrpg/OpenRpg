using System;
using Newtonsoft.Json;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Localization;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class EditorLocaleDatasourceExtensions
{
    public static string SerializeData(this EditorLocaleDatasource datasource, string locale)
    {
        if(!datasource.LocaleDatasets.ContainsKey(locale))
        { throw new Exception($"Editor contains no locale data for type [{locale}]"); }

        var localeDataset = datasource.GetLocaleDataset(locale);
        return localeDataset.SerializeData();
    }
    
    public static string SerializeData(this LocaleDataset dataset)
    { return JsonConvert.SerializeObject(dataset,  new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented }); }
}