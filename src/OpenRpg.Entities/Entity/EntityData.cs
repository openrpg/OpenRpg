using OpenRpg.Core.Common;
using OpenRpg.Core.Templates;
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
    public class EntityData : IHasLocaleDescription, IHasState<EntityStateVariables>, IHasStats<EntityStatsVariables>, ITemplateData<EntityVariables>
    {
        /// <summary>
        /// An associated template id for the entity
        /// </summary>
        /// <remarks>This isnt always used depending on if its a purely runtime character i.e player vs a npc/monster etc</remarks>
        public int TemplateId { get; set; } = -1;
        
        public string NameLocaleId { get; set; } = string.Empty;
        public string DescriptionLocaleId { get; set; } = string.Empty;
        
        public EntityStatsVariables Stats { get; set; } = new();
        public EntityStateVariables State { get; set; } = new();
        public EntityVariables Variables { get; set; } = new();
    }
}