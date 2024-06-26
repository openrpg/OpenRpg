using OpenRpg.Core.Stats.Entity;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EntityStatExtensions
    {
        public static float PhysicalDamage(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDamage);
        public static float BallisticDamage(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.BallisticDamage);
        public static float ExplosiveDamage(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDamage);
        public static float IonDamage(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.IonDamage);
        public static float LaserDamage(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.LaserDamage);
        public static void PhysicalDamage(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.PhysicalDamage] = value;
        public static void BallisticDamage(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.BallisticDamage] = value;
        public static void ExplosiveDamage(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.ExplosiveDamage] = value;
        public static void IonDamage(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.IonDamage] = value;
        public static void LaserDamage(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.LaserDamage] = value;
        
        public static float PhysicalDefense(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDefense);
        public static float BallisticDefense(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.BallisticDefense);
        public static float ExplosiveDefense(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDefense);
        public static float IonDefense(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.IonDefense);
        public static float LaserDefense(this IEntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.LaserDefense);
        public static void PhysicalDefense(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.PhysicalDefense] = value;
        public static void BallisticDefense(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.BallisticDefense] = value;
        public static void ExplosiveDefense(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.ExplosiveDefense] = value;
        public static void IonDefense(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.IonDefense] = value;
        public static void LaserDefense(this IEntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.LaserDefense] = value;
    }
}