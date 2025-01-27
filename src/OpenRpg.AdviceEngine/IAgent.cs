using System;
using OpenRpg.AdviceEngine.Handlers.Advisors;
using OpenRpg.AdviceEngine.Handlers.Considerations;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine
{
    public interface IAgent : IDisposable
    {
        object OwnerContext { get; }
        IUtilityVariables UtilityVariables { get; }
        IConsiderationHandler ConsiderationHandler { get; }
        IAdviceHandler AdviceHandler { get; }
    }
}