using System.Collections.Generic;

namespace OpenRpg.AdviceEngine.Applicators
{
    public interface IApplicatorRegistry
    {
        void AddApplicator(IApplicator applicator);
        void RemoveApplicator(IApplicator applicator);
        IEnumerable<IApplicator> GetApplicators();

        void ApplyAll(IAgent context);
        void ApplyOnlyPriority(IAgent context, int specificPriority);
    }
}