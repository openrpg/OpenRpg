using System.Collections.Generic;

namespace OpenRpg.Core.Requirements
{
    public interface IHasRequirements
    {
        IEnumerable<Requirement> Requirements { get; }
    }
}