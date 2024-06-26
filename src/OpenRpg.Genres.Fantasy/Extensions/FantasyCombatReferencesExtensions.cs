using System.Collections.Generic;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class FantasyCombatReferencesExtensions
    {
        public static ICollection<StatReference> GetDamageReferences(this IEntityStatsVariables stats)
        {
            return new[]
            {
                new StatReference(FantasyDamageTypes.IceDamage, stats.IceDamage()),
                new StatReference(FantasyDamageTypes.FireDamage, stats.FireDamage()),
                new StatReference(FantasyDamageTypes.WindDamage, stats.WindDamage()),
                new StatReference(FantasyDamageTypes.EarthDamage, stats.EarthDamage()),
                new StatReference(FantasyDamageTypes.LightDamage, stats.LightDamage()),
                new StatReference(FantasyDamageTypes.DarkDamage, stats.DarkDamage()),
                new StatReference(FantasyDamageTypes.SlashingDamage, stats.SlashingDamage()),
                new StatReference(FantasyDamageTypes.BluntDamage, stats.BluntDamage()),
                new StatReference(FantasyDamageTypes.PiercingDamage, stats.PiercingDamage()),
                new StatReference(FantasyDamageTypes.UnarmedDamage, stats.UnarmedDamage()),
                new StatReference(FantasyDamageTypes.Damage, stats.Damage())
            };
        }
        
        public static ICollection<StatReference> GetDefenseReferences(this IEntityStatsVariables stats)
        {
            return new[]
            {
                new StatReference(FantasyDamageTypes.IceDamage, stats.IceDefense()),
                new StatReference(FantasyDamageTypes.FireDamage, stats.FireDefense()),
                new StatReference(FantasyDamageTypes.WindDamage, stats.WindDefense()),
                new StatReference(FantasyDamageTypes.EarthDamage, stats.EarthDefense()),
                new StatReference(FantasyDamageTypes.LightDamage, stats.LightDefense()),
                new StatReference(FantasyDamageTypes.DarkDamage, stats.DarkDefense()),
                new StatReference(FantasyDamageTypes.SlashingDamage, stats.SlashingDefense()),
                new StatReference(FantasyDamageTypes.BluntDamage, stats.BluntDefense()),
                new StatReference(FantasyDamageTypes.PiercingDamage, stats.PiercingDefense()),
                new StatReference(FantasyDamageTypes.UnarmedDamage, stats.UnarmedDefense()),
                new StatReference(FantasyDamageTypes.Damage, stats.Defense())
            };
        }
        
        public static float GetDefenseFor(this IEntityStatsVariables stats, int effectType)
        {
            if (effectType == FantasyEffectTypes.PiercingBonusAmount) { return stats.PiercingDefense(); }
            if (effectType == FantasyEffectTypes.SlashingBonusAmount) { return stats.SlashingDefense(); }
            if (effectType == FantasyEffectTypes.BluntBonusAmount) { return stats.BluntDefense(); }
            if (effectType == FantasyEffectTypes.UnarmedBonusAmount) { return stats.UnarmedDefense(); }
            if (effectType == FantasyEffectTypes.FireBonusAmount) { return stats.FireDefense(); }
            if (effectType == FantasyEffectTypes.IceBonusAmount) { return stats.IceDefense(); }
            if (effectType == FantasyEffectTypes.WindBonusAmount) { return stats.WindDefense(); }
            if (effectType == FantasyEffectTypes.EarthBonusAmount) { return stats.EarthDefense(); }
            if (effectType == FantasyEffectTypes.LightBonusAmount) { return stats.LightDefense(); }
            if (effectType == FantasyEffectTypes.DarkBonusAmount) { return stats.DarkDefense(); }

            if (effectType == FantasyEffectTypes.AllElementDamageBonusAmount)
            {
                return stats.FireDefense() + stats.IceDefense() + stats.WindDefense() +
                       stats.EarthDefense() + stats.LightDefense() + stats.DarkDefense();
            }

            if (effectType == FantasyEffectTypes.AllMeleeDefenseBonusAmount)
            {
                return stats.PiercingDefense() + stats.SlashingDefense() +
                       stats.BluntDefense() + stats.UnarmedDefense();
            }

            if (effectType == FantasyEffectTypes.DamageBonusAmount)
            { return stats.Defense(); }

            return 0;
        }
        
        public static float GetDefenseFromDamageType(this IEntityStatsVariables stats, int damageType)
        {
            if (damageType == FantasyDamageTypes.PiercingDamage) { return stats.PiercingDefense(); }
            if (damageType == FantasyDamageTypes.SlashingDamage) { return stats.SlashingDefense(); }
            if (damageType == FantasyDamageTypes.BluntDamage) { return stats.BluntDefense(); }
            if (damageType == FantasyDamageTypes.UnarmedDamage) { return stats.UnarmedDefense(); }
            if (damageType == FantasyDamageTypes.FireDamage) { return stats.FireDefense(); }
            if (damageType == FantasyDamageTypes.IceDamage) { return stats.IceDefense(); }
            if (damageType == FantasyDamageTypes.WindDamage) { return stats.WindDefense(); }
            if (damageType == FantasyDamageTypes.EarthDamage) { return stats.EarthDefense(); }
            if (damageType == FantasyDamageTypes.LightDamage) { return stats.LightDefense(); }
            if (damageType == FantasyDamageTypes.DarkDamage) { return stats.DarkDefense(); }
            if (damageType == FantasyDamageTypes.Damage) { return stats.Defense(); }
            return 0;
        }

        public static float GetDamageFor(this IEntityStatsVariables stats, int effectType)
        {
            if (effectType == FantasyEffectTypes.PiercingBonusAmount) { return stats.PiercingDamage(); }
            if (effectType == FantasyEffectTypes.SlashingBonusAmount) { return stats.SlashingDamage(); }
            if (effectType == FantasyEffectTypes.BluntBonusAmount) { return stats.BluntDamage(); }
            if (effectType == FantasyEffectTypes.UnarmedBonusAmount) { return stats.UnarmedDamage(); }
            if (effectType == FantasyEffectTypes.FireBonusAmount) { return stats.FireDamage(); }
            if (effectType == FantasyEffectTypes.IceBonusAmount) { return stats.IceDamage(); }
            if (effectType == FantasyEffectTypes.WindBonusAmount) { return stats.WindDamage(); }
            if (effectType == FantasyEffectTypes.EarthBonusAmount) { return stats.EarthDamage(); }
            if (effectType == FantasyEffectTypes.LightBonusAmount) { return stats.LightDamage(); }
            if (effectType == FantasyEffectTypes.DarkBonusAmount) { return stats.DarkDamage(); }

            if (effectType == FantasyEffectTypes.AllElementDamageBonusAmount)
            {
                return stats.FireDamage() + stats.IceDamage() + stats.WindDamage() +
                       stats.EarthDamage() + stats.LightDamage() + stats.DarkDamage();
            }

            if (effectType == FantasyEffectTypes.AllMeleeDefenseBonusAmount)
            {
                return stats.PiercingDamage() + stats.SlashingDamage() +
                       stats.BluntDamage() + stats.UnarmedDamage();
            }

            if (effectType == FantasyEffectTypes.DamageBonusAmount)
            { return stats.Damage(); }

            return 0;
        }
        
        public static float GetDamageFromDamageType(this IEntityStatsVariables stats, int damageType)
        {
            if (damageType == FantasyDamageTypes.PiercingDamage) { return stats.PiercingDamage(); }
            if (damageType == FantasyDamageTypes.SlashingDamage) { return stats.SlashingDamage(); }
            if (damageType == FantasyDamageTypes.BluntDamage) { return stats.BluntDamage(); }
            if (damageType == FantasyDamageTypes.UnarmedDamage) { return stats.UnarmedDamage(); }
            if (damageType == FantasyDamageTypes.FireDamage) { return stats.FireDamage(); }
            if (damageType == FantasyDamageTypes.IceDamage) { return stats.IceDamage(); }
            if (damageType == FantasyDamageTypes.WindDamage) { return stats.WindDamage(); }
            if (damageType == FantasyDamageTypes.EarthDamage) { return stats.EarthDamage(); }
            if (damageType == FantasyDamageTypes.LightDamage) { return stats.LightDamage(); }
            if (damageType == FantasyDamageTypes.DarkDamage) { return stats.DarkDamage(); }
            if (damageType == FantasyDamageTypes.Damage) { return stats.Damage(); }
            return 0;
        }
    }
}