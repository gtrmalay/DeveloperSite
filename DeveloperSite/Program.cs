using DeveloperSite;
using DeveloperSite;


var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment, app.Services.CreateScope().ServiceProvider);

app.Run();