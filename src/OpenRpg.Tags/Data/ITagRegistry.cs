using System.Collections.Generic;

namespace OpenRpg.Tags.Data
{
    public interface ITagRegistry
    {
        void AddRelationship(int sourceTag, int destinationTag, float weighting);
        void RemoveTagRelationship(int sourceTag, int destinationTag);
        IEnumerable<TagWeighting> GetRelatedTags(int tag);
    }
}