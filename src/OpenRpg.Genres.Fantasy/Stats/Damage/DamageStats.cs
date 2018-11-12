namespace OpenRpg.Genres.Fantasy.Stats.Damage
{
    public class DamageStats : IDamageStats
    {
        public float PureDamage { get; set; }
        public float SlashingDamage { get; set; }
        public float BluntDamage { get; set; }
        public float PiercingDamage { get; set; }
        public float UnarmedDamage { get; set; }
        public float FireDamage { get; set; }
        public float IceDamage { get; set; }
        public float EarthDamage { get; set; }
        public float WindDamage { get; set; }
        public float LightDamage { get; set; }
        public float DarkDamage { get; set; }
        
        public static IDamageStats Default { get; } = new DamageStats();
    }
}