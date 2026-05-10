using System;
using Newtonsoft.Json;
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
}