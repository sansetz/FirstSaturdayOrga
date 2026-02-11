using FirstSaturdayOrga;
using FirstSaturdayOrga.Services.App;
using FirstSaturdayOrga.Services.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AllFsDataSource>();
builder.Services.AddScoped<AllPhotosDataSource>();
builder.Services.AddScoped<AllMapMarkersDataSource>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<DashboardService>();

await builder.Build().RunAsync();
