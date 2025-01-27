using System;
using System.Collections.Generic;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Handlers.Advisors
{
    public interface IAdviceHandler : IDisposable
    {
        IUtilityVariables UtilityVariables { get; }
        object OwnerContext { get; }

        void StartHandler();
        void StopHandler();
        
        void AddAdvice(IAdvice advice);
        void RemoveAdvice(IAdvice advice);
        void ClearAdvice();
        
        IAdvice GetAdvice(int adviceId);
        IEnumerable<IAdvice> GetAllAdvice();
    }
}