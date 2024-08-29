using OpenRpg.AdviceEngine.Handlers.Advisors;
using OpenRpg.AdviceEngine.Handlers.Advisors.Factories;
using OpenRpg.AdviceEngine.Handlers.Considerations;
using OpenRpg.AdviceEngine.Handlers.Considerations.Factories;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine
{
    public class Agent : IAgent
    {
        public object OwnerContext { get; }
        public IUtilityVariables UtilityVariables { get; } = new UtilityVariables();
        public IConsiderationHandler ConsiderationHandler { get; }
        public IAdviceHandler AdviceHandler { get; }

        public Agent(IHasDataId ownerContext, IUtilityVariables variables, IConsiderationHandler considerationHandler, IAdviceHandler adviceHandler)
        {
            OwnerContext = ownerContext;
            UtilityVariables = variables;
            ConsiderationHandler = considerationHandler;
            AdviceHandler = adviceHandler;
            ConsiderationHandler.StartHandler();
            AdviceHandler.StartHandler();
        }
        
        public Agent(object ownerContext, IConsiderationHandlerFactory considerationHandlerFactory, IAdviceHandlerFactory adviceHandlerFactory)
        {
            OwnerContext = ownerContext;
            ConsiderationHandler = considerationHandlerFactory.Create(UtilityVariables, OwnerContext);
            AdviceHandler = adviceHandlerFactory.Create(UtilityVariables, OwnerContext);
            ConsiderationHandler.StartHandler();
            AdviceHandler.StartHandler();
        }

        public void Dispose()
        {
            ConsiderationHandler.Dispose();
            AdviceHandler.Dispose();
        }
    }
}