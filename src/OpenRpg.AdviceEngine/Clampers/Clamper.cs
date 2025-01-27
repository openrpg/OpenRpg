namespace OpenRpg.AdviceEngine.Clampers
{
    public class Clamper : IClamper
    {
        public float MinRange { get; }
        public float MaxRange { get; }
        public bool Normalize { get; }

        public Clamper(float minRange, float maxRange, bool normalize = true)
        {
            MinRange = minRange;
            MaxRange = maxRange;
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