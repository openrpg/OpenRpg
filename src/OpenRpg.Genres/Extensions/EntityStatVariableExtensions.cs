using OpenRpg.Core.Stats.Entity;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStatVariableExtensions
    {
        public static int MaxHealth(this IEntityStatsVariables stats) => (int)stats.Get(GenreEntityStatsVariableTypes.MaxHealth);
        public static void MaxHealth(this IEntityStatsVariables stats, int value) => stats[GenreEntityStatsVariableTypes.MaxHealth] = value;
        
        public static float Damage(this IEntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Damage);
        public static void Damage(this IEntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Damage] = value;
        public static float Defense(this IEntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Defense);
        public static void Defense(this IEntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Defense] = value;
    }
}