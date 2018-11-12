namespace OpenRpg.Genres.Fantasy.Stats.Vitals
{
    public class VitalStats : IVitalStats
    {
        public int MaxHealth { get; set; }
        public int MaxMagic { get; set; }
        
        public static IVitalStats Default { get; } = new VitalStats();
    }
}