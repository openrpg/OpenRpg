namespace OpenRpg.AdviceEngine.Clampers
{
    public interface IClamper
    {
        float MinRange { get; }
        float MaxRange { get; }
        bool Normalize { get; }

        float Clamp(float input);
    }
}