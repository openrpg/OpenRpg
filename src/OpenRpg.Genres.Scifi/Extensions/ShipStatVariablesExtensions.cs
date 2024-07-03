using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipStatVariablesExtensions
    {
        public static float MaxArmour(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxArmour);
        public static void MaxArmour(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxArmour] = value;
        public static float MaxShield(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxShield);
        public static void MaxShield(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxShield] = value;
        public static float MaxEnergy(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.MaxEnergy);
        public static void MaxEnergy(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.MaxEnergy] = value;
        
        public static float PhysicalDamage(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.PhysicalDamage);
        public static float BallisticDamage(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.BallisticDamage);
        public static float ExplosiveDamage(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDamage);
        public static float IonDamage(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.IonDamage);
        public static float LaserDamage(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.LaserDamage);
        public static void PhysicalDamage(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.PhysicalDamage] = value;
        public static void BallisticDamage(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.BallisticDamage] = value;
        public static void ExplosiveDamage(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.ExplosiveDamage] = value;
        public static void IonDamage(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.IonDamage] = value;
        public static void LaserDamage(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.LaserDamage] = value;
        
        public static float PhysicalDefense(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.PhysicalDefense);
        public static float BallisticDefense(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.BallisticDefense);
        public static float ExplosiveDefense(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDefense);
        public static float IonDefense(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.IonDefense);
        public static float LaserDefense(this ShipStatsVariables stats) => stats.Get(ScifiShipStatsVariableTypes.LaserDefense);
        public static void PhysicalDefense(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.PhysicalDefense] = value;
        public static void BallisticDefense(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.BallisticDefense] = value;
        public static void ExplosiveDefense(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.ExplosiveDefense] = value;
        public static void IonDefense(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.IonDefense] = value;
        public static void LaserDefense(this ShipStatsVariables stats, float value) => stats[ScifiShipStatsVariableTypes.LaserDefense] = value;
    }
}