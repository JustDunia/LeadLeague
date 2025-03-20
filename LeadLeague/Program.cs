using LeadLeague;
using LeadLeague.Auth;
using LeadLeague.Database;
using LeadLeague.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
        .ReplaceService<IHistoryRepository, MigrationsHistoryRepository>()
        .UseSnakeCaseNamingConvention()
    );

    var app = builder.Build();

    app.UseAuthentication();
    app.UseUserContext();
    app.UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApi();
    }

    if (!app.Environment.IsDevelopment())
    {
        // due to SSL certificate problem in Android Emulator
        app.UseHttpsRedirection();
    }

    app.MapGet("/", [HasRole(RoleType.Admin)] (HttpContext context) =>
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