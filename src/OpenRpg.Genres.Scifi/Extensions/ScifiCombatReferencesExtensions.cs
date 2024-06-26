using System.Collections.Generic;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ScifiCombatReferencesExtensions
    {
        public static ICollection<StatReference> GetDamageReferences(this IShipStatsVariables stats)
        {
            return new[]
            {
                new StatReference(ScifiDamageTypes.Ballistic, stats.BallisticDamage()),
                new StatReference(ScifiDamageTypes.Explosive, stats.ExplosiveDamage()),
                new StatReference(ScifiDamageTypes.Ion, stats.IonDamage()),
                new StatReference(ScifiDamageTypes.Laser, stats.LaserDamage()),
                new StatReference(ScifiDamageTypes.Physical, stats.PhysicalDamage()),
            };
        }
        
        public static ICollection<StatReference> GetDefenseReferences(this IShipStatsVariables stats)
        {
            return new[]
            {
                new StatReference(ScifiDamageTypes.Ballistic, stats.BallisticDefense()),
                new StatReference(ScifiDamageTypes.Explosive, stats.ExplosiveDefense()),
                new StatReference(ScifiDamageTypes.Ion, stats.IonDefense()),
                new StatReference(ScifiDamageTypes.Laser, stats.LaserDefense()),
                new StatReference(ScifiDamageTypes.Physical, stats.PhysicalDefense()),
            };
        }
        
        public static float GetDefenseFor(this IShipStatsVariables stats, int effectType)
        {
            if (effectType == ScifiEffectTypes.BallisticDamageAmount) { return stats.BallisticDefense(); }
            if (effectType == ScifiEffectTypes.ExplosiveDamageAmount) { return stats.ExplosiveDefense(); }
            if (effectType == ScifiEffectTypes.IonDamageAmount) { return stats.IonDefense(); }
            if (effectType == ScifiEffectTypes.LaserDamageAmount) { return stats.LaserDefense(); }
            if (effectType == ScifiEffectTypes.PhysicalDamageAmount) { return stats.PhysicalDefense(); }
            return 0;
        }
        
        public static float GetDefenseFromDamageType(this IEntityStatsVariables stats, int damageType)
        {
            if (damageType == ScifiDamageTypes.Ballistic) { return stats.BallisticDamage(); }
            if (damageType == ScifiDamageTypes.Explosive) { return stats.ExplosiveDamage(); }
            if (damageType == ScifiDamageTypes.Ion) { return stats.IonDamage(); }
            if (damageType == ScifiDamageTypes.Laser) { return stats.LaserDamage(); }
            if (damageType == ScifiDamageTypes.Physical) { return stats.PhysicalDamage(); }
            return 0;
        }

        public static float GetDamageFor(this IEntityStatsVariables stats, int effectType)
        {
            if (effectType == ScifiEffectTypes.BallisticDamageAmount) { return stats.BallisticDamage(); }
            if (effectType == ScifiEffectTypes.ExplosiveDamageAmount) { return stats.ExplosiveDamage(); }
            if (effectType == ScifiEffectTypes.IonDamageAmount) { return stats.IonDamage(); }
            if (effectType == ScifiEffectTypes.LaserDamageAmount) { return stats.LaserDamage(); }
            if (effectType == ScifiEffectTypes.PhysicalDamageAmount) { return stats.PhysicalDamage(); }
            return 0;
        }
    }
}