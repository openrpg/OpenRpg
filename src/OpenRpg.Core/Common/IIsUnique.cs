using System;

namespace OpenRpg.Core.Common
{
    /// <summary>
    /// This implies the object needs runtime uniqueness such as a unique npc or procedural item
    /// </summary>
    public interface IIsUnique
    {
        /// <summary>
        /// The unique id for this instance
        /// </summary>
        Guid UniqueId { get; set; }
    }
}