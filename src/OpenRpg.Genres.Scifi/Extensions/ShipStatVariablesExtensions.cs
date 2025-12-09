using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipStatVariablesExtensions
    {
        extension(ShipStatsVariables stats)
        {
            public float MaxArmour
            {
                get => stats.Get(ScifiShipStatsVariableTypes.MaxArmour);
                set => stats[ScifiShipStatsVariableTypes.MaxArmour] = value;
            }
            
            public float MaxShield
            {
                get => stats.Get(ScifiShipStatsVariableTypes.MaxShield);
                set => stats[ScifiShipStatsVariableTypes.MaxShield] = value;
            }
            
            public float MaxEnergy
            {
                get => stats.Get(ScifiShipStatsVariableTypes.MaxEnergy);
                set => stats[ScifiShipStatsVariableTypes.MaxEnergy] = value;
            }
            
            public float PhysicalDamage
            {
                get => stats.Get(ScifiShipStatsVariableTypes.PhysicalDamage);
                set => stats[ScifiShipStatsVariableTypes.PhysicalDamage] = value;
            }
            
            public float BallisticDamage
            {
                get => stats.Get(ScifiShipStatsVariableTypes.BallisticDamage);
                set => stats[ScifiShipStatsVariableTypes.BallisticDamage] = value;
            }
            
            public float ExplosiveDamage
            {
                get => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDamage);
                set => stats[ScifiShipStatsVariableTypes.ExplosiveDamage] = value;
            }
            
            public float IonDamage
            {
                get => stats.Get(ScifiShipStatsVariableTypes.IonDamage);
                set => stats[ScifiShipStatsVariableTypes.IonDamage] = value;
            }
            
            public float LaserDamage
            {
                get => stats.Get(ScifiShipStatsVariableTypes.LaserDamage);
                set => stats[ScifiShipStatsVariableTypes.LaserDamage] = value;
            }
            
            public float PhysicalDefense
            {
                get => stats.Get(ScifiShipStatsVariableTypes.PhysicalDefense);
                set => stats[ScifiShipStatsVariableTypes.PhysicalDefense] = value;
            }
            
            public float BallisticDefense
            {
                get => stats.Get(ScifiShipStatsVariableTypes.BallisticDefense);
                set => stats[ScifiShipStatsVariableTypes.BallisticDefense] = value;
            }
            
            public float ExplosiveDefense
            {
                get => stats.Get(ScifiShipStatsVariableTypes.ExplosiveDefense);
                set => stats[ScifiShipStatsVariableTypes.ExplosiveDefense] = value;
            }
            
            public float IonDefense
            {
                get => stats.Get(ScifiShipStatsVariableTypes.IonDefense);
                set => stats[ScifiShipStatsVariableTypes.IonDefense] = value;
            }
            
            public float LaserDefense
            {
                get => stats.Get(ScifiShipStatsVariableTypes.LaserDefense);
                set => stats[ScifiShipStatsVariableTypes.LaserDefense] = value;
            }
        }
    }
}