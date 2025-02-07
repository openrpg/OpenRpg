using System;
using OpenRpg.Core.Common;

namespace OpenRpg.Entities.Entity
{
    public class UniqueEntity : Entity, IIsUnique
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}