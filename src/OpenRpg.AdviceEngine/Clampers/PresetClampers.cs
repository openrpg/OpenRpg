namespace OpenRpg.AdviceEngine.Clampers
{
    public class PresetClampers
    {
        public static IClamper Passthrough = new PassThroughClamper();
        public static IClamper ZeroToTen = new Clamper(0, 10);
        public static IClamper ZeroToHundred = new Clamper(0, 100);
        public static IClamper ZeroToThousand = new Clamper(0, 1000);
    }
}