using System.Collections.Generic;

namespace OpenRpg.Entities.Requirements
{
    /// <summary>
    /// Implies the implementation has requirements before it can function/be used
    /// </summary>
    public interface IHasRequirements
    {
        /// <summary>
        /// Requirements required for this object to function/be used
        /// </summary>
        IReadOnlyCollection<Requirement> Requirements { get; }
    }
}