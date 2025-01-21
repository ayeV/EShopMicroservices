using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure;
using Ordering.Infraestructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
     AddApplicationServices()
    .AddInfraestructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDbAsync();
    
}
app.Run();
