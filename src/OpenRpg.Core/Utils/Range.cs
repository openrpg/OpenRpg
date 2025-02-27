using System;

namespace OpenRpg.Core.Utils
{
    public struct Range
    {
        public int Min { get; }
        public int Max { get; }
        
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}