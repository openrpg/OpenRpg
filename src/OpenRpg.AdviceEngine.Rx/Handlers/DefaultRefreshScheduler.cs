using System;
using System.Reactive.Linq;
using OpenRpg.AdviceEngine.Handlers;

namespace OpenRpg.AdviceEngine.Rx.Handlers
{
    public class DefaultRefreshScheduler : IRefreshScheduler
    {
        public IObservable<bool> PreBuiltScheduler;

        public DefaultRefreshScheduler(float defaultFrequency = 0.5f)
        {
            PreBuiltScheduler = Observable
                .Timer(TimeSpan.FromSeconds(defaultFrequency), TimeSpan.FromSeconds(defaultFrequency))
                .Select(x => true);
        }

        public IObservable<bool> DefaultRefreshPeriod => PreBuiltScheduler;
    }
}