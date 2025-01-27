using System.Collections.Generic;
using OpenRpg.AdviceEngine.Applicators;
using OpenRpg.Core.Requirements;

namespace OpenRpg.AdviceEngine.Considerations.Applicators
{
    public abstract class DefaultExternalConsiderationApplicator<T> : DefaultApplicator<T>, IConsiderationApplicator
    {
        public override int Priority => 2;

        protected DefaultExternalConsiderationApplicator(IRequirementChecker<T> requirementChecker) : base(requirementChecker)
        {}

        public override void ApplyTo(IAgent agent)
        {
            var considerations = CreateConsiderations(agent);
            foreach (var consideration in considerations)
            { agent.ConsiderationHandler.AddConsideration(consideration); }
        }

        public abstract IEnumerable<IConsideration> CreateConsiderations(IAgent agent);
    }
}