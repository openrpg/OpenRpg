using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;

namespace OpenRpg.Genres.Fantasy.State.Populators
{
    public class FantasyStatePopulator : IEntityStatePopulator
    {
        public void Populate(EntityStateVariables state, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var statsVars = relatedVars.SingleOrDefault(x => x.VariableType == CoreVariableTypes.EntityStatsVariables);
            if(statsVars == null) { return; }

            var entityStats = statsVars as EntityStatsVariables;
            state.Health(entityStats.MaxHealth());
            state.Magic(entityStats.MaxMagic());
        }
    }
}