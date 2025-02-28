using System;
using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Common;

namespace OpenRpg.Items.Templates
{
    public class ProceduralItemData : ItemData, IIsUnique
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public IReadOnlyCollection<Association> ProceduralEffectAssociations { get; set; } = Array.Empty<Association>();
    }
}