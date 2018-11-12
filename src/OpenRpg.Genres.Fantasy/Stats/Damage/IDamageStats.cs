namespace OpenRpg.Genres.Fantasy.Stats.Damage
{
    public interface IDamageStats
    {
        float PureDamage { get; }
        
        float SlashingDamage { get; }
        float BluntDamage { get; }
        float PiercingDamage { get; }
        float UnarmedDamage { get; }
        
        float IceDamage { get; }
        float FireDamage { get; }
        float EarthDamage { get; }
        float WindDamage { get; }
        float LightDamage { get; }
        float DarkDamage { get; }
    }
}