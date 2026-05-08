using OpenRpg.Core.Utils;

namespace OpenRpg.Core.Extensions;

public static class RangeExtensions
{
    extension(Range range)
    {
        public bool IsWithinRange(int value) => value >= range.Min && value <= range.Max;
        public bool IsOutsideRange(int value) => value > range.Max || value < range.Min;
    }
    
    extension(RangeF range)
    {
        public bool IsWithinRange(float value) => value >= range.Min && value <= range.Max;
        public bool IsOutsideRange(float value) => value > range.Max || value < range.Min;
        public bool IsWithinRange(int value) => value >= range.Min && value <= range.Max;
        public bool IsOutsideRange(int value) => value > range.Max || value < range.Min;
    }
}