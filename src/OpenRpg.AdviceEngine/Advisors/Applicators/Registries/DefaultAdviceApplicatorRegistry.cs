using System.Collections.Generic;
using OpenRpg.AdviceEngine.Applicators;

namespace OpenRpg.AdviceEngine.Advisors.Applicators.Registries
{
    public class DefaultAdviceApplicatorRegistry : DefaultApplicatorRegistry, IAdviceApplicatorRegistry
    {
        public DefaultAdviceApplicatorRegistry(IEnumerable<IAdviceApplicator> applicators = null) : base(applicators)
        {
        }
    }
}