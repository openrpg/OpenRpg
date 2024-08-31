using System;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class NumberExtensions
    {
        public static float SanitizeAndClamp(this float value)
        {
            if(float.IsInfinity(value)) { return 0.0f; }
            if(float.IsNaN(value)) { return 0.0f; }
            
            var sanitizedNumber = MathF.Round(value, 6);
            if(sanitizedNumber < 0 ) { return 0.0f; }
            if(sanitizedNumber > 1.0f ) { return 1.0f; }
            return sanitizedNumber;
        }

        public static float NormalizeBetween(this float value, float min, float max)
        {
            var normalizedValue = (value - min) / (max - min);
            return SanitizeAndClamp(normalizedValue);
        }
        
        public static float DenormalizeBetween(this float value, float min, float max)
        { return value * (max - min) + min; }
    }
}