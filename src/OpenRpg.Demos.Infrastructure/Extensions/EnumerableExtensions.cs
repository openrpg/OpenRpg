using System;
using System.Collections.Generic;

namespace OpenRpg.Demos.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TK, TV>(this IDictionary<TK, TV> dictionary, Action<TK, TV> action)
        {
            foreach(var pair in dictionary)
            { action(pair.Key, pair.Value); }
        }
    }
}