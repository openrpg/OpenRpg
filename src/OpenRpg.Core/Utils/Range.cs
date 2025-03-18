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
        
        public static Range ZeroToOne => new Range(0, 1);
        public static Range ZeroToOneHundred => new Range(0, 100);
        
        public static implicit operator Range(RangeF range)
        { return new Range((int)range.Min, (int)range.Max); }
    }
}