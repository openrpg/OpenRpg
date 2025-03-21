using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class EntityCollectionExtensions
{
    public static IReadOnlyCollection<T> Add<T>(this IReadOnlyCollection<T> context, T newThing)
    {
        if (context is List<T> list)
        {
            list.Add(newThing);
            return list;
        }

        var newList = context.ToList();
        newList.Add(newThing);
        return newList;
    }
    
    public static IReadOnlyCollection<T> Remove<T>(this IReadOnlyCollection<T> context, T removeThing)
    {
        if (context is List<T> list)
        {
            list.Remove(removeThing);
            return list;
        }

        var newList = context.ToList();
        newList.Remove(removeThing);
        return newList;
    }

    public static List<T> AsList<T>(this IReadOnlyCollection<T> context)
    { return context as List<T> ?? new List<T>(context); }
}