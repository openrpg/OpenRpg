using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EntityStatVariableExtensions
    {
        public static int Strength(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Strength);
        public static int Dexterity(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Dexterity);
        public static int Constitution(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Constitution);
        public static int Intelligence(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Intelligence);
        public static int Wisdom(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Wisdom);
        public static int Charisma(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Charisma);
        public static void Strength(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Strength] = value;
        public static void Dexterity(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Dexterity] = value;
        public static void Constitution(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Constitution] = value;
        public static void Intelligence(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Intelligence] = value;
        public static void Wisdom(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Wisdom] = value;
        public static void Charisma(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Charisma] = value;

        public static void MaxMagic(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.MaxMagic] = value;
        public static int MaxMagic(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.MaxMagic);
        
        public static void MagicRegen(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.MagicRegen] = value;
        public static float MagicRegen(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.MagicRegen);
        
        public static float IceDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.IceDamage);
        public static float FireDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.FireDamage);
        public static float WindDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.WindDamage);
        public static float EarthDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.EarthDamage);
        public static float LightDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.LightDamage);
        public static float DarkDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.DarkDamage);
        public static float SlashingDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.SlashingDamage);
        public static float BluntDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.BluntDamage);
        public static float PiercingDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.PiercingDamage);
        public static float UnarmedDamage(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDamage);
        public static void IceDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.IceDamage] = value;
        public static void FireDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.FireDamage] = value;
        public static void WindDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.WindDamage] = value;
        public static void EarthDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.EarthDamage] = value;
        public static void LightDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.LightDamage] = value;
        public static void DarkDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.DarkDamage] = value;
        public static void SlashingDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.SlashingDamage] = value;
        public static void BluntDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.BluntDamage] = value;
        public static void PiercingDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.PiercingDamage] = value;
        public static void UnarmedDamage(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.UnarmedDamage] = value;
        
        public static float IceDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.IceDefense);
        public static float FireDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.FireDefense);
        public static float WindDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.WindDefense);
        public static float EarthDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.EarthDefense);
        public static float LightDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.LightDefense);
        public static float DarkDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.DarkDefense);
        public static float SlashingDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.SlashingDefense);
        public static float BluntDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.BluntDefense);
        public static float PiercingDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.PiercingDefense);
        public static float UnarmedDefense(this EntityStatsVariables stats) => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDefense);
        public static void IceDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.IceDefense] = value;
        public static void FireDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.FireDefense] = value;
        public static void WindDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.WindDefense] = value;
        public static void EarthDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.EarthDefense] = value;
        public static void LightDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.LightDefense] = value;
        public static void DarkDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.DarkDefense] = value;
        public static void SlashingDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.SlashingDefense] = value;
        public static void BluntDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.BluntDefense] = value;
        public static void PiercingDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.PiercingDefense] = value;
        public static void UnarmedDefense(this EntityStatsVariables stats, float value) => stats[FantasyEntityStatsVariableTypes.UnarmedDefense] = value;
    }
}