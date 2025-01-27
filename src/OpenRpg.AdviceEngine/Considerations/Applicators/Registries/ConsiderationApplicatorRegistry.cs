using System.Collections.Generic;
using OpenRpg.AdviceEngine.Applicators;

namespace OpenRpg.AdviceEngine.Considerations.Applicators.Registries
{
    public class DefaultConsiderationApplicatorRegistry : DefaultApplicatorRegistry, IConsiderationApplicatorRegistry
    {
        public DefaultConsiderationApplicatorRegistry(IEnumerable<IConsiderationApplicator> applicators = null) : base(applicators)
        {
        }
    }
}