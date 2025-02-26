using OpenRpg.Core.Utils;

namespace OpenRpg.AdviceEngine.Clampers
{
    public interface IClamper
    {
        RangeF Range { get; }
        bool Normalize { get; }

        float Clamp(float input);
    }
}