namespace OpenRpg.Core.Utils
{
    public interface IRandomizer
    {
        int Random(int min, int max);
        float Random(float min = 0, float max = 1.0f);
    }
}