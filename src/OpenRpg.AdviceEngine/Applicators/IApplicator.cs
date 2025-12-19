using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.AdviceEngine.Applicators
{
    public interface IApplicator
    {
        int Priority { get; }
        bool CanApplyTo(IAgent agent);
        void ApplyTo(IAgent agent);
        IReadOnlyCollection<Requirement> Requirements { get; }
    }
}