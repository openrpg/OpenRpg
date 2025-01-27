using System;

namespace OpenRpg.Demos.Infrastructure.Scheduling;

public interface IUpdateScheduler : IDisposable
{
    IObservable<float> OnUpdate { get; }
}