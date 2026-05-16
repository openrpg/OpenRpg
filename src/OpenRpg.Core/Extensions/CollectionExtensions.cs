using System;
using System.Collections.Generic;

namespace OpenRpg.Core.Extensions
{
    public static class CollectionExtensions
    {
        extension<T>(IReadOnlyCollection<T>)
        {
            public static IReadOnlyCollection<T> Empty() => Array.Empty<T>();
        }
        
        public static void ForEach<TK, TV>(this IDictionary<TK, TV> dictionary, Action<TK, TV> action)
        {
            foreach(var pair in dictionary)
            { action(pair.Key, pair.Value); }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(var element in enumerable)
            { action(element); }
        }
    
        public static int IndexOf<T>( this IReadOnlyList<T> collection, T elementToFind )
        {
            if(elementToFind == null) { return -1; }
        
            var i = 0;
            foreach(var element in collection )
            {
                if(element == null) { continue; }
                if(element.Equals(elementToFind))
                { return i; }
            
                i++;
            }
            return -1;
        }
        
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}