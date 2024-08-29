using OpenRpg.Core.Requirements;

namespace OpenRpg.AdviceEngine.Applicators
{
    public interface IApplicator : IHasRequirements
    {
        int Priority { get; }
        bool CanApplyTo(IAgent agent);
        void ApplyTo(IAgent agent);
    }
}