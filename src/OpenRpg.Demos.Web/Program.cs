using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenRpg.Demos.Web;
using OpenRpg.Demos.Web.Extensions;
using OpenRpg.Demos.Web.Modules;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddModule<OpenRpgModule>();
builder.Services.AddModule<OpenRpgDataModule>();
await builder.Build().RunAsync();

