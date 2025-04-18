using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Entities.Stats.Variables
{
    /// <summary>
    /// Acts as a sort of proxy to let you compute partial stats ahead of time then
    /// pull in values across all of the variables.
    ///
    /// This allows you to only need to recompute a few specific effects while keeping
    /// more static ones pre computed
    /// </summary>
    public class VirtualEntityStatsVariables : EntityStatsVariables, IStatsVariables
    {
        public Dictionary<int, EntityStatsVariables> ComputedStatVariables { get; set; } = new Dictionary<int, EntityStatsVariables>();

        public void AddComputedStats(int key, EntityStatsVariables computedStats) => ComputedStatVariables.Add(key, computedStats);
        public void RemoveComputedStats(int key) => ComputedStatVariables.Remove(key);

        public new float Get(int key)
        { return base.Get(key) + ComputedStatVariables.Sum(x => x.Value.Get(key)); }
    }
}