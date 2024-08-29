using System;
using System.Reactive;
using System.Reactive.Linq;

namespace OpenRpg.AdviceEngine.Handlers
{
    public class DefaultRefreshScheduler : IRefreshScheduler
    {
        public IObservable<Unit> PreBuiltScheduler;

        public DefaultRefreshScheduler(float defaultFrequency = 0.5f)
        {
            PreBuiltScheduler = Observable
                .Timer(TimeSpan.FromSeconds(defaultFrequency), TimeSpan.FromSeconds(defaultFrequency))
                .Select(x => Unit.Default);
        }

        public IObservable<Unit> DefaultRefreshPeriod => PreBuiltScheduler;
    }
}