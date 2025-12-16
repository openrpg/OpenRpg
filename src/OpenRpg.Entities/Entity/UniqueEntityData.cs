using System;
using OpenRpg.Core.Common;

namespace OpenRpg.Entities.Entity
{
    public class UniqueEntityData : EntityData, IIsUnique
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}