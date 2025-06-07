using OpenRpg.AdviceEngine.Handlers;
using OpenRpg.AdviceEngine.Handlers.Advisors;
using OpenRpg.AdviceEngine.Handlers.Advisors.Factories;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Rx.Advisors.Factories
{
    public class DefaultAdviceHandlerFactory : IAdviceHandlerFactory
    {
        public IRefreshScheduler RefreshScheduler { get; }

        public DefaultAdviceHandlerFactory(IRefreshScheduler refreshScheduler)
        { RefreshScheduler = refreshScheduler; }

        public IAdviceHandler Create(IUtilityVariables variables, object context)
        { return new AdviceHandler(RefreshScheduler, variables, context); }
    }
}