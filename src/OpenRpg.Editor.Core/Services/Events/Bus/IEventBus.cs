using System;

namespace OpenRpg.Editor.Core.Services.Events.Bus;

/// <summary>
/// This interface provides basic Publish and Subscriber style interactions throughout the application
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// This method will publish a message synchronously for any listeners to action
    /// </summary>
    /// <param name="message">The message content to send</param>
    /// <typeparam name="T">The type of the content to send</typeparam>
    void Publish<T>(T message);
        
    /// <summary>
    /// This method will publish a message asynchronously to any listeners
    /// </summary>
    /// <param name="message">The message content to send</param>
    /// <typeparam name="T">The type of the content to send</typeparam>
    /// <returns>A task for the published message</returns>
    void PublishAsync<T>(T message);
        
    /// <summary>
    /// Listens out for any messages of a given type to be published
    /// </summary>
    /// <typeparam name="T">The type of the message to listen out for</typeparam>
    /// <returns></returns>
    IObservable<T> Receive<T>();
}