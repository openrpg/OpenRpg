namespace OpenRpg.Core.Utils
{
    public struct RangeF
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public RangeF(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}