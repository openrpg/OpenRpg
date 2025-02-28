namespace OpenRpg.Core.Utils
{
    public struct RangeF
    {
        public float Min { get; }
        public float Max { get; }

        public RangeF(float min, float max)
        {
            Min = min;
            Max = max;
        }
        
        public static RangeF ZeroToOne => new RangeF(0, 1);
        public static RangeF ZeroToOneHundred => new RangeF(0, 100);
        
        public static implicit operator RangeF(Range range)
        { return new RangeF(range.Min, range.Max); }
    }
}