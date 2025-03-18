using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.AdviceEngine.Clampers
{
    public class Clamper : IClamper
    {
        public RangeF Range { get; }
        public bool Normalize { get; }

        public Clamper(float minRange, float maxRange, bool normalize = true) 
            : this(new RangeF(minRange, maxRange), normalize)
        {}

        public Clamper(RangeF range, bool normalize = true)
        {
            Normalize = normalize;
            Range = range;
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