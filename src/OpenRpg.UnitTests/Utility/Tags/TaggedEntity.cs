using System.Collections.Generic;
using OpenRpg.Tags;

namespace OpenRpg.UnitTests.Utility.Tags;

public class TaggedEntity : ITagged
{
    public IReadOnlyCollection<int> Tags { get; set; }
    public TaggedEntity(params int[] tags) => Tags = tags;
}