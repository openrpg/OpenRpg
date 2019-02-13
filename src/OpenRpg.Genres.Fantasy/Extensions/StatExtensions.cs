using System.Collections.Generic;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class StatExtensions
    {
        public static int Strength(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Strength];
        public static int Dexterity(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Dexterity];
        public static int Constitution(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Constitution];
        public static int Intelligence(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Intelligence];
        public static int Wisdom(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Wisdom];
        public static int Charisma(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Charisma];
        public static void Strength(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Strength] = value;
        public static void Dexterity(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Dexterity] = value;
        public static void Constitution(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Constitution] = value;
        public static void Intelligence(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Intelligence] = value;
        public static void Wisdom(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Wisdom] = value;
        public static void Charisma(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Charisma] = value;
        
        public static int Health(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Health];
        public static int MaxHealth(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.MaxHealth];
        public static int Magic(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.Magic];
        public static int MaxMagic(this IEntityStats entityStats) => (int)entityStats.Stats[StatsTypes.MaxMagic];
        public static void Health(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Health] = value;
        public static void MaxHealth(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.MaxHealth] = value;
        public static void Magic(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.Magic] = value;
        public static void MaxMagic(this IEntityStats entityStats, int value) => entityStats.Stats[StatsTypes.MaxMagic] = value;
        
        public static float IceDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.IceDamage];
        public static float FireDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.FireDamage];
        public static float WindDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.WindDamage];
        public static float EarthDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.EarthDamage];
        public static float LightDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.LightDamage];
        public static float DarkDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.DarkDamage];
        public static float SlashingDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.SlashingDamage];
        public static float BluntDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.BluntDamage];
        public static float PiercingDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.PiercingDamage];
        public static float UnarmedDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.UnarmedDamage];
        public static float PureDamage(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.PureDamage];
        public static void IceDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.IceDamage] = value;
        public static void FireDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.FireDamage] = value;
        public static void WindDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.WindDamage] = value;
        public static void EarthDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.EarthDamage] = value;
        public static void LightDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.LightDamage] = value;
        public static void DarkDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.DarkDamage] = value;
        public static void SlashingDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.SlashingDamage] = value;
        public static void BluntDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.BluntDamage] = value;
        public static void PiercingDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.PiercingDamage] = value;
        public static void UnarmedDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.UnarmedDamage] = value;
        public static void PureDamage(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.PureDamage] = value;
        
        public static float IceDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.IceDefense];
        public static float FireDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.FireDefense];
        public static float WindDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.WindDefense];
        public static float EarthDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.EarthDefense];
        public static float LightDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.LightDefense];
        public static float DarkDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.DarkDefense];
        public static float SlashingDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.SlashingDefense];
        public static float BluntDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.BluntDefense];
        public static float PiercingDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.PiercingDefense];
        public static float UnarmedDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.UnarmedDefense];
        public static float PureDefense(this IEntityStats entityStats) => entityStats.Stats[StatsTypes.PureDefense];
        public static void IceDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.IceDefense] = value;
        public static void FireDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.FireDefense] = value;
        public static void WindDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.WindDefense] = value;
        public static void EarthDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.EarthDefense] = value;
        public static void LightDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.LightDefense] = value;
        public static void DarkDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.DarkDefense] = value;
        public static void SlashingDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.SlashingDefense] = value;
        public static void BluntDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.BluntDefense] = value;
        public static void PiercingDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.PiercingDefense] = value;
        public static void UnarmedDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.UnarmedDefense] = value;
        public static void PureDefense(this IEntityStats entityStats, float value) => entityStats.Stats[StatsTypes.PureDefense] = value;
        
        public static ICollection<StatReference> GetDamageReferences(this IEntityStats entityStats)
        {
            return new StatReference[]
            {
                new StatReference(DamageTypes.IceDamage, entityStats.IceDamage()),
                new StatReference(DamageTypes.FireDamage, entityStats.FireDamage()),
                new StatReference(DamageTypes.WindDamage, entityStats.WindDamage()),
                new StatReference(DamageTypes.EarthDamage, entityStats.EarthDamage()),
                new StatReference(DamageTypes.LightDamage, entityStats.LightDamage()),
                new StatReference(DamageTypes.DarkDamage, entityStats.DarkDamage()),
                new StatReference(DamageTypes.SlashingDamage, entityStats.SlashingDamage()),
                new StatReference(DamageTypes.BluntDamage, entityStats.BluntDamage()),
                new StatReference(DamageTypes.PiercingDamage, entityStats.PiercingDamage()),
                new StatReference(DamageTypes.UnarmedDamage, entityStats.UnarmedDamage())
            };
        }
        
        public static ICollection<StatReference> GetDefenseReferences(this IEntityStats entityStats)
        {
            return new StatReference[]
            {
                new StatReference(DamageTypes.IceDamage, entityStats.IceDefense()),
                new StatReference(DamageTypes.FireDamage, entityStats.FireDefense()),
                new StatReference(DamageTypes.WindDamage, entityStats.WindDefense()),
                new StatReference(DamageTypes.EarthDamage, entityStats.EarthDefense()),
                new StatReference(DamageTypes.LightDamage, entityStats.LightDefense()),
                new StatReference(DamageTypes.DarkDamage, entityStats.DarkDefense()),
                new StatReference(DamageTypes.SlashingDamage, entityStats.SlashingDefense()),
                new StatReference(DamageTypes.BluntDamage, entityStats.BluntDefense()),
                new StatReference(DamageTypes.PiercingDamage, entityStats.PiercingDefense()),
                new StatReference(DamageTypes.UnarmedDamage, entityStats.UnarmedDefense())
            };
        }
    }
}