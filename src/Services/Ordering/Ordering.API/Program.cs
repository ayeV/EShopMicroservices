using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
     AddApplicationServices()
    .AddInfraestructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();


app.Run();
