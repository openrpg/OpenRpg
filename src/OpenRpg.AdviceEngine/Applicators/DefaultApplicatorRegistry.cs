using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.AdviceEngine.Applicators
{
    public class DefaultApplicatorRegistry : IApplicatorRegistry
    {
        public List<IApplicator> Applicators { get; }

        public DefaultApplicatorRegistry(IEnumerable<IApplicator> applicators = null)
        {
            Applicators = applicators?.ToList() ?? new List<IApplicator>();
        }

        public void AddApplicator(IApplicator applicator) => Applicators.Add(applicator);
        public void RemoveApplicator(IApplicator applicator) => Applicators.Remove(applicator);
        public IEnumerable<IApplicator> GetApplicators() => Applicators;

        public void ApplyAll(IAgent context)
        {
            foreach (var applicator in Applicators.OrderBy(x => x.Priority))
            {
                if(applicator.CanApplyTo(context))
                { applicator.ApplyTo(context); }
            }
        }

        public void ApplyOnlyPriority(IAgent context, int specificPriority)
        {
            var applicatorsForPriority = Applicators.Where(x => x.Priority == specificPriority);
            foreach (var applicator in applicatorsForPriority)
            {
                if (applicator.CanApplyTo(context))
                { applicator.ApplyTo(context); }
            }
        }
    }
}