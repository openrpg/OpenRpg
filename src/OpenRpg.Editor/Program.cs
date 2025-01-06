using System;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Editor.Modules;
using OpenRpg.Editor.UI;
using Photino.Blazor;

namespace OpenRpg.Editor;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        appBuilder.Services.AddLogging();
        DataServiceModule.Setup(appBuilder.Services);
        appBuilder.RootComponents.Add<App>("#app");

        var app = appBuilder.Build();
        app.Services.SetupAppData();
        
        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            { app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString()); };

        app.MainWindow
            .SetTitle("OpenRpg - Data Editor")
            .SetSize(1920, 1080)
            .SetUseOsDefaultSize(false);

        app.Run();
    }
}