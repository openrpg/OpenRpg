using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Utils;

namespace OpenRpg.Core.Extensions
{
    public static class RandomExtensions
    {
        public static T TakeRandomFrom<T>(this IRandomizer randomizer, IEnumerable<T> source)
        {
            var availableCount = source.Count();
            if(availableCount == 0) { throw new Exception("Unable to pick random number from empty list"); }

            var random = randomizer.Random(0, availableCount-1);
            return source.Skip(random).First();
        }

        public static IEnumerable<T> TakeRandomFrom<T>(this IRandomizer randomizer, IEnumerable<T> source, int count)
        {
            var availableCount = source.Count();
            if (availableCount == 0) { throw new Exception("Unable to pick random number from empty list"); }
            if (availableCount == count) { return source; }

            var randomElements = new List<T>();
            for(var i=0;i<count;i++)
            {
                var random = randomizer.Random(0, availableCount - 1);
                var randomElement = source.Skip(random).First();
                if(randomElements.Contains(randomElement))
                {
                    i--;
                    continue;
                }
                randomElements.Add(randomElement);
            }
            return randomElements;
        }
        
        public static T TakeRandom<T>(this IEnumerable<T> source, IRandomizer randomizer)
        { return TakeRandomFrom(randomizer, source); }
        
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int count, IRandomizer randomizer)
        { return TakeRandomFrom(randomizer, source, count); }
    }
}