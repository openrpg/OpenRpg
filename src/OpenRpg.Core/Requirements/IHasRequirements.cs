using System.Collections.Generic;

namespace OpenRpg.Core.Requirements
{
    public interface IHasRequirements
    {
        IReadOnlyCollection<Requirement> Requirements { get; }
    }
}