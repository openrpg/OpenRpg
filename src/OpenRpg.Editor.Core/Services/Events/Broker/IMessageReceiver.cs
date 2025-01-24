using System;

namespace OpenRpg.Editor.Core.Services.Events.Broker;

public interface IMessageReceiver
{
    /// <summary>
    /// Subscribe typed message.
    /// </summary>
    IObservable<T> Receive<T>();
}