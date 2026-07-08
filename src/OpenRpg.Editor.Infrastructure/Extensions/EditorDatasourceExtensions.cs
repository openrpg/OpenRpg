using System;
using Newtonsoft.Json;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Projects.Json.Convertors;
using OpenRpg.Projects.Json.Resolvers;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class EditorDatasourceExtensions
{
    public static string SerializeData<T>(this EditorDatasource datasource) where T : ITemplate
    {
        var dataForType = datasource.GetAll<T>();
        return JsonConvert.SerializeObject(dataForType, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto, 
            Formatting = Formatting.Indented,
            Converters = { new VariablesConverter(), new ReadOnlyCollectionConverter() },
            ContractResolver = new NoTypeForReadOnlyCollectionsResolver(),
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
        });
    }
}