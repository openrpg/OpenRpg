using OpenRpg.Core.Utils;

namespace OpenRpg.AdviceEngine.Clampers
{
    public class PassThroughClamper : IClamper
    {
        public RangeF Range { get;  }
        public bool Normalize { get; }
        
        public float Clamp(float input)
        { return input; }
    }
}