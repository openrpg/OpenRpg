using System.Collections.Generic;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public struct StatReference
    {
        public int StatType;
        public float StatValue;

        public StatReference(int statType, float statValue)
        {
            StatType = statType;
            StatValue = statValue;
        }
    }
    
    public static class StatExtensions
    {
        public static ICollection<StatReference> AsList(this IDamageStats damageStats)
        {
            return new StatReference[]
            {
                new StatReference(DamageTypes.IceDamage, damageStats.IceDamage),
                new StatReference(DamageTypes.FireDamage, damageStats.FireDamage),
                new StatReference(DamageTypes.WindDamage, damageStats.WindDamage),
                new StatReference(DamageTypes.EarthDamage, damageStats.EarthDamage),
                new StatReference(DamageTypes.LightDamage, damageStats.LightDamage),
                new StatReference(DamageTypes.DarkDamage, damageStats.DarkDamage),
                new StatReference(DamageTypes.SlashingDamage, damageStats.SlashingDamage),
                new StatReference(DamageTypes.BluntDamage, damageStats.BluntDamage),
                new StatReference(DamageTypes.PiercingDamage, damageStats.PiercingDamage),
                new StatReference(DamageTypes.UnarmedDamage, damageStats.UnarmedDamage)
            };
        }
        
        public static ICollection<StatReference> AsList(this IDefenseStats defenseStats)
        {
            return new StatReference[]
            {
                new StatReference(DamageTypes.IceDamage, defenseStats.IceDefense),
                new StatReference(DamageTypes.FireDamage, defenseStats.FireDefense),
                new StatReference(DamageTypes.WindDamage, defenseStats.WindDefense),
                new StatReference(DamageTypes.EarthDamage, defenseStats.EarthDefense),
                new StatReference(DamageTypes.LightDamage, defenseStats.LightDefense),
                new StatReference(DamageTypes.DarkDamage, defenseStats.DarkDefense),
                new StatReference(DamageTypes.SlashingDamage, defenseStats.SlashingDefense),
                new StatReference(DamageTypes.BluntDamage, defenseStats.BluntDefense),
                new StatReference(DamageTypes.PiercingDamage, defenseStats.PiercingDefense),
                new StatReference(DamageTypes.UnarmedDamage, defenseStats.UnarmedDefense)
            };
        }
    }
}