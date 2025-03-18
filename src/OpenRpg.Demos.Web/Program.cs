using ApexCharts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenRpg.Demos.Infrastructure.DI;
using OpenRpg.Demos.Web;
using OpenRpg.Demos.Web.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddModule<OpenRpgModule>();
builder.Services.AddModule<OpenRpgDataModule>();
builder.Services.AddApexCharts();
await builder.Build().RunAsync();