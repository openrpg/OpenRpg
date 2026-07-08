using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EntityStatExtensions
    {
        extension(EntityStatsVariables stats)
        {
            public float PhysicalDamage
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDamage);
                set => stats[ScifiEntityStatsVariableTypes.PhysicalDamage] = value;
            }
            
            public float BallisticDamage
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.BallisticDamage);
                set => stats[ScifiEntityStatsVariableTypes.BallisticDamage] = value;
            }
            
            public float ExplosiveDamage
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDamage);
                set => stats[ScifiEntityStatsVariableTypes.ExplosiveDamage] = value;
            }
            
            public float IonDamage
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.IonDamage);
                set => stats[ScifiEntityStatsVariableTypes.IonDamage] = value;
            }
            
            public float LaserDamage
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.LaserDamage);
                set => stats[ScifiEntityStatsVariableTypes.LaserDamage] = value;
            }
            
            public float PhysicalDefense
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.PhysicalDefense);
                set => stats[ScifiEntityStatsVariableTypes.PhysicalDefense] = value;
            }
            
            public float BallisticDefense
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.BallisticDefense);
                set => stats[ScifiEntityStatsVariableTypes.BallisticDefense] = value;
            }
            
            public float ExplosiveDefense
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.ExplosiveDefense);
                set => stats[ScifiEntityStatsVariableTypes.ExplosiveDefense] = value;
            }
            
            public float IonDefense
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.IonDefense);
                set => stats[ScifiEntityStatsVariableTypes.IonDefense] = value;
            }
            
            public float LaserDefense
            {
                get => stats.Get(ScifiEntityStatsVariableTypes.LaserDefense);
                set => stats[ScifiEntityStatsVariableTypes.LaserDefense] = value;
            }
        }
    }
}