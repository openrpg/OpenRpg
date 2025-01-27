using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Common;
using OpenRpg.Demos.Infrastructure.Data;

namespace OpenRpg.Demos.Infrastructure.Extensions;

public static class IDataGeneratorExtensions
{
    public static Dictionary<object, object> GenerateDictionary<T>(this IDataGenerator<T> dataGenerator)
        where T : IHasDataId
    {
        return dataGenerator
            .GenerateData()
            .ToDictionary(x => (object)x.Id, x => (object)x);
    }
}