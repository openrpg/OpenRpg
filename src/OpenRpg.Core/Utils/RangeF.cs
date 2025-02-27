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
    }
}