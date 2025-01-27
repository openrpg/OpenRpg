using System;
using OpenRpg.Core.Common;

namespace OpenRpg.Core.Entity
{
    public class UniqueEntity : Entity, IIsUnique
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}