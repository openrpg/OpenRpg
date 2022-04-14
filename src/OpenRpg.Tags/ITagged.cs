using System.Collections.Generic;

namespace OpenRpg.Tags
{
    public interface ITagged
    {
        IReadOnlyCollection<int> Tags { get; }
    }
}