using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Utils;
using Range = OpenRpg.Core.Utils.Range;

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
        
        public static (T element, int index) TakeRandomWithIndexFrom<T>(this IRandomizer randomizer, IEnumerable<T> source)
        {
            var availableCount = source.Count();
            if(availableCount == 0) { throw new Exception("Unable to pick random number from empty list"); }

            var random = randomizer.Random(0, availableCount-1);
            var result = source.Skip(random).First();
            return (result, random);
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
        
        public static (T element, int index) TakeRandomWithIndex<T>(this IEnumerable<T> source, IRandomizer randomizer)
        { return TakeRandomWithIndexFrom(randomizer, source); }
        
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int count, IRandomizer randomizer)
        { return TakeRandomFrom(randomizer, source, count); }

        public static int Random(this Range range, IRandomizer randomizer)
        { return randomizer.Random(range.Min, range.Max); }
        
        public static float Random(this RangeF range, IRandomizer randomizer)
        { return randomizer.Random(range.Min, range.Max); }
        
        public static int Random(this IRandomizer randomizer, Range range)
        { return randomizer.Random(range.Min, range.Max); }
        
        public static float Random(this IRandomizer randomizer, RangeF range)
        { return randomizer.Random(range.Min, range.Max); }
    }
}