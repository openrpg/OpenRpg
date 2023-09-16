using System;

namespace OpenRpg.Core.Entity
{
    public class DefaultUniqueEntity : DefaultEntity, IUniqueEntity
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}