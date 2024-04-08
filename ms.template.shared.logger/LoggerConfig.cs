using Serilog;

namespace ms.template.shared.logger;

public static class LoggerConfig
{
    private static readonly LoggerConfiguration _loggerConfiguration;

    static LoggerConfig()
    {
        _loggerConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName();
    }

    public static LoggerConfiguration GetConsoleLogger()
        => _loggerConfiguration.WriteTo.Console();
}
