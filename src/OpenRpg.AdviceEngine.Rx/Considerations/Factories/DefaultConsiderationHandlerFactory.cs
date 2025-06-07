using OpenRpg.AdviceEngine.Handlers;
using OpenRpg.AdviceEngine.Handlers.Considerations;
using OpenRpg.AdviceEngine.Handlers.Considerations.Factories;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Rx.Considerations.Factories
{
    public class DefaultConsiderationHandlerFactory : IConsiderationHandlerFactory
    {
        public IRefreshScheduler RefreshScheduler { get; }

        public DefaultConsiderationHandlerFactory(IRefreshScheduler refreshScheduler)
        { RefreshScheduler = refreshScheduler; }

        public IConsiderationHandler Create(IUtilityVariables variables, object context)
        { return new ConsiderationHandler(RefreshScheduler, variables, context); }
    }
}