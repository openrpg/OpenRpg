using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Handlers.Considerations.Factories
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