using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDefenseStatComputer : IDefenseStatComputer
    {
        public float ComputeDefense(float baseDefense, float statBonus, byte defenseType, byte defenseBonusType, ICollection<Effect> effects)
        {
            var bonusDefense = effects.GetTotalFor(defenseType, defenseBonusType);
            var totalDefense = baseDefense + bonusDefense;
            if(totalDefense == 0) { return 0; }
            return totalDefense + statBonus;
        }
        
        public float ComputeIceDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.IceDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.IceDefense, BonusTypes.IceBonus, effects); }
        
        public float ComputeFireDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.FireDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.FireDefense, BonusTypes.FireBonus, effects); }
        
        public float ComputeWindDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.WindDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.WindDefense, BonusTypes.WindBonus, effects); }
        
        public float ComputeEarthDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.EarthDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.EarthDefense, BonusTypes.EarthBonus, effects); }
        
        public float ComputeLightDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.LightDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.LightDefense, BonusTypes.LightBonus, effects); }
        
        public float ComputeDarkDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.DarkDefense, (float)(baseAttributeStats.Intelligence * 0.25), BonusTypes.DarkDefense, BonusTypes.DarkBonus, effects); }

        public float ComputeSlashingDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength * 0.125;
            var dexterityBonus = baseAttributeStats.Dexterity * 0.125;
            return ComputeDefense(baseDefenseStats.SlashingDefense, (float)(strengthBonus + dexterityBonus), BonusTypes.SlashingDefense, BonusTypes.SlashingBonus, effects);
        }
        
        public float ComputeBluntDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.BluntDefense, (float)(baseAttributeStats.Strength * 0.25), BonusTypes.BluntDefense, BonusTypes.BluntBonus, effects); }
        
        public float ComputePiercingDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        { return ComputeDefense(baseDefenseStats.PiercingDefense, (float)(baseAttributeStats.Dexterity * 0.25), BonusTypes.PiercingDefense, BonusTypes.PiercingBonus, effects); }
        
        public float ComputeUnarmedDefense(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength * 0.125;
            var dexterityBonus = baseAttributeStats.Dexterity * 0.125;
            return ComputeDefense(baseDefenseStats.UnarmedDefense, (float)(strengthBonus + dexterityBonus), BonusTypes.UnarmedDefense, BonusTypes.UnarmedBonus, effects);
        }
        
        public IDefenseStats ComputeStats(IDefenseStats baseDefenseStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var iceDefense = ComputeIceDefense(baseDefenseStats, baseAttributeStats, effects);
            var fireDefense = ComputeFireDefense(baseDefenseStats, baseAttributeStats, effects);
            var windDefense = ComputeWindDefense(baseDefenseStats, baseAttributeStats, effects);
            var earthDefense = ComputeEarthDefense(baseDefenseStats, baseAttributeStats, effects);
            var lightDefense = ComputeLightDefense(baseDefenseStats, baseAttributeStats, effects);
            var darkDefense = ComputeDarkDefense(baseDefenseStats, baseAttributeStats, effects);
            var slashingDefense = ComputeSlashingDefense(baseDefenseStats, baseAttributeStats, effects);
            var bluntDefense = ComputeBluntDefense(baseDefenseStats, baseAttributeStats, effects);
            var piercingDefense = ComputePiercingDefense(baseDefenseStats, baseAttributeStats, effects);
            var unarmedDefense = ComputeUnarmedDefense(baseDefenseStats, baseAttributeStats, effects);

            return new DefenseStats
            {
                SlashingDefense = slashingDefense,
                BluntDefense = bluntDefense,
                PiercingDefense = piercingDefense,
                UnarmedDefense = unarmedDefense,
                IceDefense = iceDefense,
                FireDefense = fireDefense,
                WindDefense = windDefense,
                EarthDefense = earthDefense,
                LightDefense = lightDefense,
                DarkDefense = darkDefense
            };
        }
    }
}