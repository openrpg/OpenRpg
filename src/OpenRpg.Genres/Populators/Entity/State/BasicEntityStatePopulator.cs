using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.State.Entity;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Genres.Populators.Entity.State
{
    public class BasicEntityStatePopulator : IEntityStatePopulator
    {
        public void Populate(IEntityStateVariables varsToPopulate, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var statsVars = relatedVars.SingleOrDefault(x => x.VariableType == CoreVariableTypes.EntityStatsVariables);
            if(statsVars == null) { return; }

            var entityStats = statsVars as IEntityStatsVariables;
            varsToPopulate.Health(entityStats.MaxHealth());
        }
    }
}