using System;
using System.Reactive.Subjects;
using System.Timers;

namespace OpenRpg.Demos.Infrastructure.Scheduling;

public class DefaultUpdateScheduler : IUpdateScheduler
{
    private readonly Timer _timer;
    private DateTime _previousDateTime;
    private readonly Subject<float> _onUpdate = new Subject<float>();

    public IObservable<float> OnUpdate => _onUpdate;
        
    public DefaultUpdateScheduler(int desiredFps = 60)
    {
        _timer = new Timer { Interval = 1000f / desiredFps };
        _timer.Elapsed += UpdateTick;

        _previousDateTime = DateTime.Now;
        _timer.Start();
    }

    private void UpdateTick(object sender, ElapsedEventArgs e)
    {
        var deltaTime = e.SignalTime - _previousDateTime;
        _onUpdate.OnNext((float)deltaTime.TotalSeconds);
        _previousDateTime = e.SignalTime;
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
            
        _onUpdate.Dispose();
    }
}