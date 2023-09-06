using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Tags.Extensions
{
    public static class WeightedRelationshipExtensions
    {
        public static IEnumerable<TagWeighting> SumWeights(this IEnumerable<TagWeighting> tagWeightings)
        { 
            return tagWeightings
                .GroupBy(x => x.Tag)
                .Select(x => new TagWeighting(x.Key, x.Sum(y => y.Weight))); 
        }
        
        public static IEnumerable<TagWeighting> AverageWeights(this IEnumerable<TagWeighting> tagWeightings)
        { 
            return tagWeightings
                .GroupBy(x => x.Tag)
                .Select(x => new TagWeighting(x.Key, x.Average(y => y.Weight))); 
        }
        
        public static float GetTotalWeightFor(this IEnumerable<TagWeighting> tagWeightings, params int[] tags)
        { return GetTotalWeightFor(tagWeightings, (IReadOnlyCollection<int>)tags); }
        
        public static float GetTotalWeightFor(this IEnumerable<TagWeighting> tagWeightings, IEnumerable<int> tags)
        { return tagWeightings.Sum(x => tags.Contains(x.Tag) ? x.Weight : 0.0f); }
        
        public static IEnumerable<TagWeighting> WithWeightAbove(this IEnumerable<TagWeighting> tagWeightings, float minimumWeight)
        { return tagWeightings.Where(x => x.Weight > minimumWeight); }
        
        public static IEnumerable<TagWeighting> WithWeightBelow(this IEnumerable<TagWeighting> tagWeightings, float maximumWeight)
        { return tagWeightings.Where(x => x.Weight < maximumWeight); }

        public static IEnumerable<TagWeighting> WithPositiveRelationships(this IEnumerable<TagWeighting> tagWeightings)
        { return WithWeightAbove(tagWeightings, 0); }
        
        public static IEnumerable<TagWeighting> WithNegativeRelationships(this IEnumerable<TagWeighting> tagWeightings)
        { return WithWeightBelow(tagWeightings, 0); }
    }
}