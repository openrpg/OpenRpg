namespace OpenRpg.Core.Utils
{
    public interface IRandomizer
    {
        int Random(int min, int max);
        float Random(float min, float max);
    }
}