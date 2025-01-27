using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenRpg.Core.Templates;
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
    { return JsonConvert.SerializeObject(dataset, Formatting.Indented); }
    
    public static void DeserializeData(this EditorLocaleDatasource datasource, string data, bool replace = false)
    {
        var localeData = JsonConvert.DeserializeObject<LocaleDataset>(data);

        if (!datasource.LocaleDatasets.ContainsKey(localeData.LocaleCode))
        {
            datasource.LocaleDatasets[localeData.LocaleCode] = localeData;
            return;
        }
        
        if (replace)
        {
            datasource.LocaleDatasets[localeData.LocaleCode] = localeData;
            return;
        }

        var existingLocaleDataset = datasource.LocaleDatasets[localeData.LocaleCode];
        foreach (var dataItem in localeData.LocaleData)
        { existingLocaleDataset.LocaleData.Add(dataItem.Key, dataItem.Value); }
    }
}