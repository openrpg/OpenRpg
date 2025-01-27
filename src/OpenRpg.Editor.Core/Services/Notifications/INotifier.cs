using System.Threading.Tasks;

namespace OpenRpg.Editor.Core.Services.Notifications;

public interface INotifier
{
    Task ShowNotification(string title, string styles = "is-info", int showForMilliseconds = 2000);
}