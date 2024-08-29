using OpenRpg.AdviceEngine.Applicators;
using OpenRpg.Core.Requirements;

namespace OpenRpg.AdviceEngine.Advisors.Applicators
{
    public abstract class DefaultAdviceApplicator<T> : DefaultApplicator<T>, IAdviceApplicator
    {
        public override int Priority => 1;

        protected DefaultAdviceApplicator(IRequirementChecker<T> requirementChecker) : base(requirementChecker)
        {}

        public override void ApplyTo(IAgent agent)
        {
            var advice = CreateAdvice(agent);
            agent.AdviceHandler.AddAdvice(advice);
        }

        public abstract IAdvice CreateAdvice(IAgent agent);
    }
}