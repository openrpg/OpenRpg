using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace OpenRpg.Editor.Core.Services.Notifications;

public class Notifier : INotifier
{
    public IJSRuntime JsRuntime { get; }

    public Notifier(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async Task ShowNotification(string title, string styles = "is-info", int duration = 2000)
    { await JsRuntime.InvokeVoidAsync("showNotification", title, styles, duration); }
}