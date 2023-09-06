using System.Collections.Generic;

namespace OpenRpg.Tags
{
    public interface ITagged
    {
        IReadOnlyList<int> Tags { get; }
    }
}