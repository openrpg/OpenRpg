using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipStatVariablesExtensions
    {
        public static float MaxArmour(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxArmour);
        public static void MaxArmour(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxArmour] = value;
        public static float MaxShield(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxShield);
        public static void MaxShield(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxShield] = value;
        public static float MaxEnergy(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxEnergy);
        public static void MaxEnergy(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxEnergy] = value;
        
        public static float PhysicalDamage(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.PhysicalDamage);
        public static float BallisticDamage(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.BallisticDamage);
        public static float ExplosiveDamage(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDamage);
        public static float IonDamage(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.IonDamage);
        public static float LaserDamage(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.LaserDamage);
        public static void PhysicalDamage(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.PhysicalDamage] = value;
        public static void BallisticDamage(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.BallisticDamage] = value;
        public static void ExplosiveDamage(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.ExplosiveDamage] = value;
        public static void IonDamage(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.IonDamage] = value;
        public static void LaserDamage(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.LaserDamage] = value;
        
        public static float PhysicalDefense(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.PhysicalDefense);
        public static float BallisticDefense(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.BallisticDefense);
        public static float ExplosiveDefense(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDefense);
        public static float IonDefense(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.IonDefense);
        public static float LaserDefense(this IShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.LaserDefense);
        public static void PhysicalDefense(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.PhysicalDefense] = value;
        public static void BallisticDefense(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.BallisticDefense] = value;
        public static void ExplosiveDefense(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.ExplosiveDefense] = value;
        public static void IonDefense(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.IonDefense] = value;
        public static void LaserDefense(this IShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.LaserDefense] = value;
    }
}