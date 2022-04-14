using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Tags.Data;

namespace OpenRpg.Tags.Extensions
{
    public static class TagRegistryExtensions
    {
        public static IEnumerable<TagWeighting> GetRelatedTags(this ITagRegistry registry, params int[] sourceTags)
            => GetRelatedTags(registry, (IReadOnlyCollection<int>)sourceTags);
        
        public static IEnumerable<TagWeighting> GetRelatedTags(this ITagRegistry registry, IEnumerable<int> sourceTags)
        {
            return sourceTags
                .SelectMany(registry.GetRelatedTags)
                .AverageWeights();
        }
        
        public static IEnumerable<TagWeighting> GetRelatedTags(this ITagRegistry registry, IEnumerable<int> sourceTags, int depth)
        {
            var allRelated = new List<TagWeighting>();

            var currentTags = sourceTags.ToArray();
            for (var i = 0; i < depth; i++)
            {
                var currentDepthTagWeights = currentTags.SelectMany(registry.GetRelatedTags).ToArray();
                allRelated.AddRange(currentDepthTagWeights);
                currentTags = currentDepthTagWeights.Select(x => x.Tag).ToArray();
            }

            return allRelated.AverageWeights();
        }

        public static IEnumerable<TaggedWithScore> GetTaggedScoresFor(this ITagRegistry registry, 
            IEnumerable<ITagged> taggedElements, params int[] sourceTags)
            => GetTaggedScoresFor(registry, taggedElements, (IReadOnlyCollection<int>)sourceTags);
        
        public static IEnumerable<TaggedWithScore> GetTaggedScoresFor(this ITagRegistry registry, 
            IEnumerable<ITagged> taggedElements, IReadOnlyCollection<int> sourceTags)
        {
            var relatedTags = GetRelatedTags(registry, sourceTags).ToArray();
            return taggedElements
                .ContainingTags(sourceTags)
                .Select(x => new TaggedWithScore(x, relatedTags.GetTotalWeightFor(x.Tags)));
        }
    }
}