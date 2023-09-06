using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Tags.Extensions
{
    public static class ITaggedExtensions
    {
        public static bool ContainsTags(this ITagged taggedEntity, params int[] tags) 
            => ContainsTags(taggedEntity, (IEnumerable<int>)tags);
        
        public static bool ContainsTags(this ITagged taggedEntity, IEnumerable<int> tags)
        { return tags.All(x => taggedEntity.Tags.Contains(x)); }

        public static bool DoesNotContainsTags(this ITagged taggedEntity, params int[] tags)
            => DoesNotContainsTags(taggedEntity, (IEnumerable<int>)tags);
        
        public static bool DoesNotContainsTags(this ITagged taggedEntity, IEnumerable<int> tags)
        { return !tags.Any(x => taggedEntity.Tags.Contains(x)); }
        
        public static IEnumerable<T> ContainingTags<T>(this IEnumerable<T> taggedEntities, 
            params int[] tags) where T : ITagged => ContainingTags(taggedEntities, (IEnumerable<int>)tags);

        public static IEnumerable<T> ContainingTags<T>(this IEnumerable<T> taggedEntities, IEnumerable<int> tags) where T : ITagged
        { return taggedEntities.Where(x => x.ContainsTags(tags)); }
    }
}