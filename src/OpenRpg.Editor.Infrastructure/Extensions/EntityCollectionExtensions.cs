using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class EntityCollectionExtensions
{
    public static List<T> AsList<T>(this IReadOnlyCollection<T> context)
    { return context as List<T> ?? new List<T>(context); }
}
