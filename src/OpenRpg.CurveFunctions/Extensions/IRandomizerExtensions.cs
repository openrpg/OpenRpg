using System;
using OpenRpg.Core.Utils;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class IRandomizerExtensions
    {
        public static float Random(this IRandomizer randomizer, ICurveFunction curve)
        {
            var randomNumber = randomizer.Random();
            return curve.Plot(randomNumber);
        }
        
        public static float Random(this IRandomizer randomizer, ICurveFunction curve, float minValue, float maxValue)
        {
            var randomNumber = randomizer.Random(minValue, maxValue);
            var isNegative = minValue < 0;
            if (isNegative)
            {
                randomNumber = Math.Abs(randomNumber);
                maxValue = Math.Abs(minValue);
            }

            var result = curve.ScaledPlot(randomNumber, maxValue);
            return isNegative ? -result : result;
        }

        public static int Random(this IRandomizer randomizer, ICurveFunction curve, int minValue, int maxValue)
        {
            var result = Random(randomizer, curve, (float)minValue, (float)maxValue);
            return (int)Math.Round(result);
        }
    }
}