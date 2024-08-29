using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Handlers.Considerations.Factories
{
    public interface IConsiderationHandlerFactory
    {
        IConsiderationHandler Create(IUtilityVariables variables, object context);
    }
}