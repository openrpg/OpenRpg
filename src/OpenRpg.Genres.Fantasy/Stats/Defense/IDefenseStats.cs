namespace OpenRpg.Genres.Fantasy.Stats.Defense
{
    public interface IDefenseStats
    {
        float PureDefense { get; }
        
        float SlashingDefense { get; }
        float BluntDefense { get; }
        float PiercingDefense { get; }
        float UnarmedDefense { get; }
        
        float IceDefense { get; }
        float FireDefense { get; }
        float EarthDefense { get; }
        float WindDefense { get; }
        float LightDefense { get; }
        float DarkDefense { get; }
    }
}