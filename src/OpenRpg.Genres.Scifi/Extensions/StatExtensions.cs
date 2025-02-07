using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EntityStatExtensions
    {
        public static float PhysicalDamage(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDamage);
        public static float BallisticDamage(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.BallisticDamage);
        public static float ExplosiveDamage(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDamage);
        public static float IonDamage(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.IonDamage);
        public static float LaserDamage(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.LaserDamage);
        public static void PhysicalDamage(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.PhysicalDamage] = value;
        public static void BallisticDamage(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.BallisticDamage] = value;
        public static void ExplosiveDamage(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.ExplosiveDamage] = value;
        public static void IonDamage(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.IonDamage] = value;
        public static void LaserDamage(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.LaserDamage] = value;
        
        public static float PhysicalDefense(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDefense);
        public static float BallisticDefense(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.BallisticDefense);
        public static float ExplosiveDefense(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDefense);
        public static float IonDefense(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.IonDefense);
        public static float LaserDefense(this EntityStatsVariables stats) => stats.Get(ScifiEntityStatsVariableTypes.LaserDefense);
        public static void PhysicalDefense(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.PhysicalDefense] = value;
        public static void BallisticDefense(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.BallisticDefense] = value;
        public static void ExplosiveDefense(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.ExplosiveDefense] = value;
        public static void IonDefense(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.IonDefense] = value;
        public static void LaserDefense(this EntityStatsVariables stats, float value) => stats[ScifiEntityStatsVariableTypes.LaserDefense] = value;
    }
}