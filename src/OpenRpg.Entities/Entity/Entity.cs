using OpenRpg.Core.Common;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.State;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Entities.Stats;
using OpenRpg.Entities.Stats.Variables;

namespace OpenRpg.Entities.Entity
{
    /// <summary>
    /// This represents the base building block that all entities can build off such as characters or monsters etc
    /// </summary>
    /// <remarks>
    /// Things like class/race may not always be applicable to all entities, so you can extend this further
    /// to add additional requirements on but this gives a building block to allow more functionality in
    /// the base layers.
    /// </remarks>
    public class Entity : IHasLocaleDescription, IHasState<EntityStateVariables>, IHasStats<EntityStatsVariables>, IHasVariables<EntityVariables>
    {
        public string NameLocaleId { get; set; } = string.Empty;
        public string DescriptionLocaleId { get; set; } = string.Empty;
        
        public EntityStatsVariables Stats { get; set; } = new EntityStatsVariables();
        public EntityStateVariables State { get; set; } = new EntityStateVariables();
        public EntityVariables Variables { get; set; } = new EntityVariables();
    }
}