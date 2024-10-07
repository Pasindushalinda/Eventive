using Eventive.Api.Extensions;
using Eventive.Api.Middleware;
using Eventive.Common.Application;
using Eventive.Common.Infrastructure;
using Eventive.Modules.Events.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//add serilog
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

//Registers the GlobalExceptionHandler as the implementation of the IExceptionHandler interface.
//This ensures that the GlobalExceptionHandler will be used to handle exceptions throughout the application.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Registers services to generate detailed problem responses conforming to the RFC 7807 specification.
// This is often used to provide standardized error responses in a RESTful API.
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//specified required assemblies 
builder.Services.AddApplication([Eventive.Modules.Events.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("Database")!,
    builder.Configuration.GetConnectionString("Cache")!
);

builder.Configuration.AddModuleConfiguration(["events"]);

builder.Services.AddEventModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

EventModule.MapEndpoint(app);

//add serilog middleware
//track the incoming request and produced structured log
app.UseSerilogRequestLogging();

app.Run();
