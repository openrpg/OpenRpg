using System.Collections.Generic;

namespace OpenRpg.Tags
{
    /// <summary>
    /// Represents a basic object that just stores tags
    /// </summary>
    public class TagList : List<int>
    {
        public TagList()
        {
        }

        public TagList(IEnumerable<int> collection) : base(collection)
        {
        }
        
        public TagList(params int[] collection) : base(collection)
        {
        }
    }
}