using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class CombatExtensions
    {
        public static string TypeToName(this Damage damage)
        {
            if (damage.Type == DamageTypes.IceDamage) return "Ice";
            if (damage.Type == DamageTypes.FireDamage) return "Fire";
            if (damage.Type == DamageTypes.WindDamage) return "Wind";
            if (damage.Type == DamageTypes.EarthDamage) return "Earth";
            if (damage.Type == DamageTypes.DarkDamage) return "Dark";
            if (damage.Type == DamageTypes.LightDamage) return "Light";
            
            if (damage.Type == DamageTypes.SlashingDamage) return "Slashing";
            if (damage.Type == DamageTypes.BluntDamage) return "Blunt";
            if (damage.Type == DamageTypes.PiercingDamage) return "Piercing";
            if (damage.Type == DamageTypes.UnarmedDamage) return "Unarmed";

            return "Unknown";
        }
    }
}