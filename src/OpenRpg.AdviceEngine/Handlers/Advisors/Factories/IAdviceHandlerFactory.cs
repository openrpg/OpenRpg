using OpenRpg.AdviceEngine.Variables;
namespace OpenRpg.AdviceEngine.Handlers.Advisors.Factories
{
    public interface IAdviceHandlerFactory
    {
        IAdviceHandler Create(IUtilityVariables variables, object context);
    }
}