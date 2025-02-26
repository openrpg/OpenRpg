using System;
using OpenRpg.Core.Utils;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class NumberExtensions
    {
        public static float SanitizeAndClamp(this float value)
        {
            if(float.IsInfinity(value)) { return 0.0f; }
            if(float.IsNaN(value)) { return 0.0f; }

            if(value < 0 ) { return 0.0f; }
            if(value > 1.0f ) { return 1.0f; }
            return value;
        }

        public static float NormalizeBetween(this float value, float min, float max)
        {
            var normalizedValue = (value - min) / (max - min);
            return SanitizeAndClamp(normalizedValue);
        }
        
        public static float NormalizeBetween(this float value, RangeF range)
        {
            var normalizedValue = (value - range.Min) / (range.Max - range.Min);
            return SanitizeAndClamp(normalizedValue);
        }
        
        public static float DenormalizeBetween(this float value, float min, float max)
        { return value * (max - min) + min; }
        
        public static float DenormalizeBetween(this float value, RangeF range)
        { return value * (range.Max - range.Min) + range.Min; }
    }
}