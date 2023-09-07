using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using OpenRpg.CurveFunctions.Scaling;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> enumerable, int sampleRate)
        { return enumerable.Where((_, i) => i % sampleRate == 0); }

        public static IEnumerable<float> Normalize(this IEnumerable<float> enumerable, int min, int max)
        { return enumerable.Select(x => x.NormalizeBetween(min, max)); }

        public static IEnumerable<float> PlotAgainst(this IEnumerable<float> enumerable, ICurveFunction curveFunction)
        { return enumerable.Select(curveFunction.Plot); }
        
        public static IEnumerable<float> PlotAgainst(this IEnumerable<float> enumerable, IScalingFunction scalingFunction)
        { return enumerable.Select(scalingFunction.Plot); }
    }
}