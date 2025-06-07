using System;

namespace OpenRpg.AdviceEngine.Handlers
{
    public interface IRefreshScheduler
    {
        IObservable<bool> DefaultRefreshPeriod { get; }
    }
}