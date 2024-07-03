using OpenRpg.Core.Stats.Variables;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStatVariableExtensions
    {
        public static int MaxHealth(this EntityStatsVariables stats) => (int)stats.Get(GenreEntityStatsVariableTypes.MaxHealth);
        public static void MaxHealth(this EntityStatsVariables stats, int value) => stats[GenreEntityStatsVariableTypes.MaxHealth] = value;
        
        public static float Damage(this EntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Damage);
        public static void Damage(this EntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Damage] = value;
        public static float Defense(this EntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Defense);
        public static void Defense(this EntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Defense] = value;
    }
}