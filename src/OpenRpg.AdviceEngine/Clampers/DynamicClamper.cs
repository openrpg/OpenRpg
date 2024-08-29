using System;

namespace OpenRpg.AdviceEngine.Clampers
{
    public class DynamicClamper : IClamper
    {
        public float MinRange => _minRangeAccessor();
        public float MaxRange => _maxRangeAccessor();
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
            if(input < MinRange) { result = MinRange; }
            if(input > MaxRange) { result = MaxRange; }
            if(!Normalize){ return result; }
            return (result - MinRange) / (MaxRange - MinRange);
        }
    }
}