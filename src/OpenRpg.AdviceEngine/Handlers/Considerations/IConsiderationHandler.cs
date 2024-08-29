using System;
using System.Reactive;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Handlers.Considerations
{
    public interface IConsiderationHandler : IDisposable
    {
        IUtilityVariables UtilityVariables { get; }
        object OwnerContext { get; }

        void StartHandler();
        void StopHandler();
        
        void AddConsideration(IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null);
        void RemoveConsideration(UtilityKey utilityKey);
        void ClearConsiderations();
    }
}