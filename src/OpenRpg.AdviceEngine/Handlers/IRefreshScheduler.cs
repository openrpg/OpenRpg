using System;
using System.Reactive;

namespace OpenRpg.AdviceEngine.Handlers
{
    public interface IRefreshScheduler
    {
        IObservable<Unit> DefaultRefreshPeriod { get; }
    }
}