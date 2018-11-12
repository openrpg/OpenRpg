using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDamageStatComputer : IDamageStatComputer
    {
        public float ComputeDamage(float baseDamage, float statBonus, byte damageType, byte damageBonusType, ICollection<Effect> effects)
        {
            var bonusDamage = effects.GetTotalFor(damageType, damageBonusType);
            var totalDamage = baseDamage + bonusDamage;
            if(totalDamage == 0) { return 0; }
            return totalDamage + statBonus;
        }
        
        public float ComputeIceDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.IceDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.IceDamage, BonusTypes.IceBonus, effects); }
        
        public float ComputeFireDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.FireDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.FireDamage, BonusTypes.FireBonus, effects); }
        
        public float ComputeWindDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.WindDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.WindDamage, BonusTypes.WindBonus, effects); }
        
        public float ComputeEarthDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.EarthDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.EarthDamage, BonusTypes.EarthBonus, effects); }
        
        public float ComputeLightDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.LightDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.LightDamage, BonusTypes.LightBonus, effects); }
        
        public float ComputeDarkDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.DarkDamage, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.DarkDamage, BonusTypes.DarkBonus, effects); }

        public float ComputeSlashingDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength * 0.125;
            var dexterityBonus = baseAttributeStats.Dexterity * 0.125;
            return ComputeDamage(baseDamageStats.SlashingDamage, (float)(strengthBonus + dexterityBonus), BonusTypes.SlashingDamage, BonusTypes.SlashingBonus, effects);
        }
        
        public float ComputeBluntDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.BluntDamage, (float)(baseAttributeStats.Strength * 0.25), BonusTypes.BluntDamage, BonusTypes.BluntBonus, effects); }
        
        public float ComputePiercingDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDamage(baseDamageStats.PiercingDamage, (float)(baseAttributeStats.Dexterity * 0.25), BonusTypes.PiercingDamage, BonusTypes.PiercingBonus, effects); }
        
        public float ComputeUnarmedDamage(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength * 0.125;
            var dexterityBonus = baseAttributeStats.Dexterity * 0.125;
            return ComputeDamage(baseDamageStats.UnarmedDamage, (float)(strengthBonus + dexterityBonus), BonusTypes.UnarmedDamage, BonusTypes.UnarmedBonus, effects);
        }
        
        public IDamageStats ComputeStats(IDamageStats baseDamageStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var iceDamage = ComputeIceDamage(baseDamageStats, baseAttributeStats, effects);
            var fireDamage = ComputeFireDamage(baseDamageStats, baseAttributeStats, effects);
            var windDamage = ComputeWindDamage(baseDamageStats, baseAttributeStats, effects);
            var earthDamage = ComputeEarthDamage(baseDamageStats, baseAttributeStats, effects);
            var lightDamage = ComputeLightDamage(baseDamageStats, baseAttributeStats, effects);
            var darkDamage = ComputeDarkDamage(baseDamageStats, baseAttributeStats, effects);
            var slashingDamage = ComputeSlashingDamage(baseDamageStats, baseAttributeStats, effects);
            var bluntDamage = ComputeBluntDamage(baseDamageStats, baseAttributeStats, effects);
            var piercingDamage = ComputePiercingDamage(baseDamageStats, baseAttributeStats, effects);
            var unarmedDamage = ComputeUnarmedDamage(baseDamageStats, baseAttributeStats, effects);

            return new DamageStats
            {
                SlashingDamage = slashingDamage,
                BluntDamage = bluntDamage,
                PiercingDamage = piercingDamage,
                UnarmedDamage = unarmedDamage,
                IceDamage = iceDamage,
                FireDamage = fireDamage,
                WindDamage = windDamage,
                EarthDamage = earthDamage,
                LightDamage = lightDamage,
                DarkDamage = darkDamage
            };
        }
    }
}