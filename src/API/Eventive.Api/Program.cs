using Eventive.Api.Extensions;
using Eventive.Common.Application;
using Eventive.Common.Infrastructure;
using Eventive.Modules.Events.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//add serilog
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//specified required assemblies 
builder.Services.AddApplication([Eventive.Modules.Events.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

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
