using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Tags.Data
{
    public class TagRegistry : ITagRegistry
    {
        public IDictionary<int, ICollection<TagWeighting>> TagRelations { get; }

        public TagRegistry() : this(new Dictionary<int, ICollection<TagWeighting>>())
        {}
        
        public TagRegistry(IDictionary<int, ICollection<TagWeighting>> tagRelations)
        { TagRelations = tagRelations; }

        public void AddRelationship(int sourceTag, int destinationTag, float weighting)
        {
            if (!TagRelations.ContainsKey(sourceTag))
            { TagRelations.Add(sourceTag, new List<TagWeighting>()); }

            TagRelations[sourceTag].Add(new TagWeighting(destinationTag, weighting));
        }
        
        public void RemoveTagRelationship(int sourceTag, int destinationTag)
        {
            if (!TagRelations.ContainsKey(sourceTag)) { return; }

            var weightings = TagRelations[sourceTag];
            
            var matchingWeightings = weightings
                .Where(x => x.Tag == destinationTag)
                .ToArray();

            foreach (var weighing in matchingWeightings)
            { weightings.Remove(weighing); }
        }

        public IEnumerable<TagWeighting> GetRelatedTags(int tag)
        { return TagRelations.ContainsKey(tag) ? TagRelations[tag] : Array.Empty<TagWeighting>(); }
    }
}