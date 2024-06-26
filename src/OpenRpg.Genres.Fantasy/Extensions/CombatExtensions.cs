using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class CombatExtensions
    {
        public static string TypeToName(this Damage damage)
        {
            if (damage.Type == FantasyDamageTypes.IceDamage) return "Ice";
            if (damage.Type == FantasyDamageTypes.FireDamage) return "Fire";
            if (damage.Type == FantasyDamageTypes.WindDamage) return "Wind";
            if (damage.Type == FantasyDamageTypes.EarthDamage) return "Earth";
            if (damage.Type == FantasyDamageTypes.DarkDamage) return "Dark";
            if (damage.Type == FantasyDamageTypes.LightDamage) return "Light";
            
            if (damage.Type == FantasyDamageTypes.SlashingDamage) return "Slashing";
            if (damage.Type == FantasyDamageTypes.BluntDamage) return "Blunt";
            if (damage.Type == FantasyDamageTypes.PiercingDamage) return "Piercing";
            if (damage.Type == FantasyDamageTypes.UnarmedDamage) return "Unarmed";

            return "Unknown";
        }
    }
}