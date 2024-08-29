using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Handlers.Advisors.Factories
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