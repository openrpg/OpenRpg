namespace OpenRpg.Core.Utils
{
    public class DefaultRandomizer : IRandomizer
    {
        public System.Random NativeRandomizer { get; }

        public DefaultRandomizer(System.Random random)
        { NativeRandomizer = random; }

        public int Random(int min, int max)
        { return NativeRandomizer.Next(min, max + 1); }

        public float Random(float min, float max)
        { return (float)NativeRandomizer.NextDouble() * (max - min) + min; }
    }
}