using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Genres.Populators.Entity.State
{
    public class BasicEntityStatePopulator : IEntityStatePopulator
    {
        public void Populate(EntityStateVariables varsToPopulate, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var statsVars = relatedVars.SingleOrDefault(x => x.VariableType == CoreVariableTypes.EntityStatsVariables);
            if(statsVars == null) { return; }

            var entityStats = statsVars as EntityStatsVariables;
            varsToPopulate.Health(entityStats.MaxHealth());
            varsToPopulate.Stamina(entityStats.MaxStamina());
        }
    }
}