using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Tags.Extensions
{
    public static class TagExtensions
    {
        public static bool ContainsTags(this TagList sourceTagList, params int[] tags)
        { return ContainsTags(sourceTagList, (IEnumerable<int>)tags); }

        public static bool ContainsTags(this TagList sourceTagList, IEnumerable<int> tags)
        { return tags.All(sourceTagList.Contains); }

        public static bool DoesNotContainsTags(this TagList sourceTagList, params int[] tags)
        { return DoesNotContainsTags(sourceTagList, (IEnumerable<int>)tags); }

        public static bool DoesNotContainsTags(this TagList sourceTagList, IEnumerable<int> tags)
        { return !tags.Any(sourceTagList.Contains); }
        
        public static IEnumerable<ContextualTags> ContainingTags(this IEnumerable<ContextualTags> sourceTags, params int[] tags)
        { return ContainingTags(sourceTags, (IEnumerable<int>)tags); }

        public static IEnumerable<ContextualTags> ContainingTags(this IEnumerable<ContextualTags> sourceTags, IEnumerable<int> tags)
        { return sourceTags.Where(x => x.Tags.ContainsTags(tags)); }
    }
}