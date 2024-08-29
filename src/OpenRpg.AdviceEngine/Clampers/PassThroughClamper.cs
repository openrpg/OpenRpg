namespace OpenRpg.AdviceEngine.Clampers
{
    public class PassThroughClamper : IClamper
    {
        public float MinRange { get;  }
        public float MaxRange { get;  }
        public bool Normalize { get; }
        
        public float Clamp(float input)
        { return input; }
    }
}