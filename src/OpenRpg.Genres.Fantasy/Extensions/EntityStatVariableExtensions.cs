using OpenRpg.Core.Stats.Entity;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EntityStatVariableExtensions
    {
        public static int Strength(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Strength);
        public static int Dexterity(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Dexterity);
        public static int Constitution(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Constitution);
        public static int Intelligence(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Intelligence);
        public static int Wisdom(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Wisdom);
        public static int Charisma(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Charisma);
        public static void Strength(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Strength] = value;
        public static void Dexterity(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Dexterity] = value;
        public static void Constitution(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Constitution] = value;
        public static void Intelligence(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Intelligence] = value;
        public static void Wisdom(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Wisdom] = value;
        public static void Charisma(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Charisma] = value;

        public static void MaxMagic(this IEntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.MaxMagic] = value;
        public static int MaxMagic(this IEntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.MaxMagic);
        
        public static float IceDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.IceDamage);
        public static float FireDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.FireDamage);
        public static float WindDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.WindDamage);
        public static float EarthDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.EarthDamage);
        public static float LightDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.LightDamage);
        public static float DarkDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.DarkDamage);
        public static float SlashingDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.SlashingDamage);
        public static float BluntDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.BluntDamage);
        public static float PiercingDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.PiercingDamage);
        public static float UnarmedDamage(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDamage);
        public static void IceDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.IceDamage] = value;
        public static void FireDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.FireDamage] = value;
        public static void WindDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.WindDamage] = value;
        public static void EarthDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.EarthDamage] = value;
        public static void LightDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.LightDamage] = value;
        public static void DarkDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.DarkDamage] = value;
        public static void SlashingDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.SlashingDamage] = value;
        public static void BluntDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.BluntDamage] = value;
        public static void PiercingDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.PiercingDamage] = value;
        public static void UnarmedDamage(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.UnarmedDamage] = value;
        
        public static float IceDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.IceDefense);
        public static float FireDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.FireDefense);
        public static float WindDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.WindDefense);
        public static float EarthDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.EarthDefense);
        public static float LightDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.LightDefense);
        public static float DarkDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.DarkDefense);
        public static float SlashingDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.SlashingDefense);
        public static float BluntDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.BluntDefense);
        public static float PiercingDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.PiercingDefense);
        public static float UnarmedDefense(this IEntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDefense);
        public static void IceDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.IceDefense] = value;
        public static void FireDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.FireDefense] = value;
        public static void WindDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.WindDefense] = value;
        public static void EarthDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.EarthDefense] = value;
        public static void LightDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.LightDefense] = value;
        public static void DarkDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.DarkDefense] = value;
        public static void SlashingDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.SlashingDefense] = value;
        public static void BluntDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.BluntDefense] = value;
        public static void PiercingDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.PiercingDefense] = value;
        public static void UnarmedDefense(this IEntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.UnarmedDefense] = value;
    }
}