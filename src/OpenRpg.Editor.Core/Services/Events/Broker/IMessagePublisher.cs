namespace OpenRpg.Editor.Core.Services.Events.Broker;

public interface IMessagePublisher
{
    /// <summary>
    /// Send Message to all receiver.
    /// </summary>
    void Publish<T>(T message);
}