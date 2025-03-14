using LeadLeague;
using LeadLeague.Auth;
using LeadLeague.Database;
using LeadLeague.OpenApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static LeadLeague.Auth.AuthConfiguration;

Logger.Create();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.ConfigureAuth();
    builder.Services.AddProblemDetails();
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    });

    builder.Services.AddDbContext<AppDbContext>(options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"))
    );

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseUserContext();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApi();
    }

    app.UseHttpsRedirection();

    app.MapGet("/", [Authorize] (HttpContext context) =>
    {

        return "Hello world";
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}