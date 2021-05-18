namespace OpenRpg.Core.Utils
{
    public class DefaultRandomizer : IRandomizer
    {
        private System.Random _random;

        public DefaultRandomizer(System.Random random)
        { _random = random; }

        public int Random(int min, int max)
        { return _random.Next(min, max + 1); }

        public float Random(float min, float max)
        { return (float)_random.NextDouble() * (max - min) + min; }
    }
}