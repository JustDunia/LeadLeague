using Serilog;
using Serilog.Events;

namespace LeadLeague;

public static class Logger
{
    public static void Create()
    {
        var seqEndpoint = Environment.GetEnvironmentVariable("SEQ_ENDPOINT") ?? "http://localhost:5341";
        var seqApiKey = Environment.GetEnvironmentVariable("SEQ_API_KEY");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Fatal)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(serverUrl: seqEndpoint, apiKey: seqApiKey)
            .CreateLogger();
    }
}
