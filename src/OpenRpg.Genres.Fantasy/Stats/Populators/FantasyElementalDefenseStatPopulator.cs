using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Stats.Populators;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Populators.Conventions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats.Populators
{
    public class FantasyElementalDefenseStatPopulator : CompositeStatPopulator<IEntityStatsVariables>, IEntityPartialStatPopulator
    {
        public int Priority => 10;
        
        public FantasyElementalDefenseStatPopulator()
        {
            PartialPopulators = new[]
            {
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.IceDefense, EffectRelationships.IceDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.FireDefense, EffectRelationships.FireDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.WindDefense, EffectRelationships.WindDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.EarthDefense, EffectRelationships.EarthDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.LightDefense, EffectRelationships.LightDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, 
                    FantasyEntityStatsVariableTypes.DarkDefense, EffectRelationships.DarkDefenseRelationship, GetElementalModBonus, Priority),
            };
        }

        public static float GetElementalModBonus(IEntityStatsVariables stats, IReadOnlyCollection<Effect> activeEffects,
            IReadOnlyCollection<IVariables> relatedVars)
        { return stats.Intelligence() / 100.0f;}
    }
}