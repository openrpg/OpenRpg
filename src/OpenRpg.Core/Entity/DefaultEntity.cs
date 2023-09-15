using System;
using OpenRpg.Core.State.Entity;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Variables.Entity;

namespace OpenRpg.Core.Entity
{
    /// <summary>
    /// This represents the default implementation for an entity
    /// </summary>
    public class DefaultEntity : IEntity
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public string NameLocaleId { get; set; } = string.Empty;
        public string DescriptionLocaleId { get; set; } = string.Empty;
        public IEntityStatsVariables Stats { get; set; } = new DefaultEntityStatsVariables();
        public IEntityStateVariables State { get; set; } = new DefaultEntityStateVariables();
        public IEntityVariables Variables { get; set; } = new DefaultEntityVariables();

        public DefaultEntity()
        { }
    }
}