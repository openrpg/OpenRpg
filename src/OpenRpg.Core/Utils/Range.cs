using System;

namespace OpenRpg.Core.Utils
{
    public struct Range
    {
        public int Min { get; set; }
        public int Max { get; set; }
        
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}