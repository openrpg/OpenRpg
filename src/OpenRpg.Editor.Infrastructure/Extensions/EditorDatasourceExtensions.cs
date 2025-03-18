using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Infrastructure.Data;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class EditorDatasourceExtensions
{
    public static string SerializeData<T>(this EditorDatasource datasource) where T : ITemplate
    {
        var type = typeof(T);
        if(!datasource.Database.ContainsKey(type))
        { throw new Exception($"Editor contains no template data for type [{type.Name}]"); }

        var dataForType = datasource.GetAll<T>();
        return JsonConvert.SerializeObject(dataForType, new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented });
    }
    
    public static void DeserializeData<T>(this EditorDatasource datasource, JArray jsonData, bool replaceData = false) where T : ITemplate
    {
        var type = typeof(T);
        if (!datasource.Database.ContainsKey(type))
        { datasource.Database[type] = new Dictionary<object, object>(); }

        var dataForType = jsonData.ToObject<T[]>(new JsonSerializer(){ TypeNameHandling = TypeNameHandling.Auto });

        if (replaceData)
        {
            datasource.Database[type] = dataForType.ToDictionary(x => (object)x.Id, x => (object)x);
            return;
        }

        var typeDatasource = datasource.Database[type];
        foreach (var dataItem in dataForType)
        { typeDatasource.Add(dataItem.Id, dataItem); }
    }
}