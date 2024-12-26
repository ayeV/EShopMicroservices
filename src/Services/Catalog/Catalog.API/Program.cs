
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(options => 
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.InitializeMartenWith<CatalogInitialData>();
    }

}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse});
app.Run();
