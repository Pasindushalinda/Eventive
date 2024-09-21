using Eventive.Api.Extensions;
using Eventive.Common.Application;
using Eventive.Common.Infrastructure;
using Eventive.Modules.Events.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
