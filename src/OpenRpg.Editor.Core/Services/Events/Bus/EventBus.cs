using System;
using OpenRpg.Editor.Core.Services.Events.Broker;
using OpenRpg.Editor.Core.Services.Threading;

namespace OpenRpg.Editor.Core.Services.Events.Bus;

public class EventBus : IEventBus
{
    public IMessageBroker MessageBroker { get; }
    public IThreadHandler ThreadHandler { get; }

    public EventBus(IMessageBroker messageBroker, IThreadHandler threadHandler)
    {
        MessageBroker = messageBroker;
        ThreadHandler = threadHandler;
    }

    public void Publish<T>(T message)
    { MessageBroker.Publish(message); }

    public void PublishAsync<T>(T message)
    { ThreadHandler.Run(() => MessageBroker.Publish(message)); }

    public IObservable<T> Receive<T>()
    { return MessageBroker.Receive<T>(); }

}