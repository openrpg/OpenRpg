using System;
using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.AdviceEngine.Clampers
{
    public class DynamicClamper : IClamper
    {
        public RangeF Range => new RangeF(_minRangeAccessor(), _maxRangeAccessor());
        public bool Normalize { get; }

        private readonly Func<float> _minRangeAccessor;
        private readonly Func<float> _maxRangeAccessor;

        public DynamicClamper(Func<float> minRangeAccessor, Func<float> maxRangeAccessor, bool normalize = true)
        {
            _minRangeAccessor = minRangeAccessor;
            _maxRangeAccessor = maxRangeAccessor;
            Normalize = normalize;
        }

        public float Clamp(float input)
        {
            var result = input;
            if(input < Range.Min) { result = Range.Min; }
            if(input > Range.Max) { result = Range.Max; }
            return !Normalize ? result : result.NormalizeBetween(Range);
        }
    }
}