namespace OpenRpg.Genres.Fantasy.Stats.Defense
{
    public class DefenseStats : IDefenseStats
    {
        public float PureDefense { get; set; }
        public float SlashingDefense { get; set; }
        public float BluntDefense { get; set; }
        public float PiercingDefense { get; set; }
        public float UnarmedDefense { get; set; }
        public float FireDefense { get; set; }
        public float IceDefense { get; set; }
        public float EarthDefense { get; set; }
        public float WindDefense { get; set; }
        public float LightDefense { get; set; }
        public float DarkDefense { get; set; }
        
        public static IDefenseStats Default { get; } = new DefenseStats();
    }
}