using Microsoft.Extensions.Configuration;

namespace Metrics.UiTests.Config;

public sealed record Credentials(string AdminUsername, string AdminPassword)
{
    public static Credentials Load()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        // Gherkin usa "ADMIN_USERNAME" e "ADMIN_PASSWORD" como placeholders.
        var u = Environment.GetEnvironmentVariable("ADMIN_USERNAME")
                ?? config["Credentials:ADMIN_USERNAME"]
                ?? "admin@example.com";

        var p = Environment.GetEnvironmentVariable("ADMIN_PASSWORD")
                ?? config["Credentials:ADMIN_PASSWORD"]
                ?? "change-me";

        return new Credentials(u, p);
    }

    /// <summary>
    /// Resolve strings do tipo "ADMIN_USERNAME", "<ADMIN_USERNAME>" ou qualquer outro env var.
    /// </summary>
    public string ResolvePlaceholder(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;

        var v = value.Trim();
        if (v.StartsWith("<") && v.EndsWith(">") && v.Length > 2)
            v = v[1..^1];

        // Primeiro tenta env var (para qualquer placeholder genérico)
        var env = Environment.GetEnvironmentVariable(v);
        if (!string.IsNullOrWhiteSpace(env))
            return env;

        return v switch
        {
            "ADMIN_USERNAME" => AdminUsername,
            "ADMIN_PASSWORD" => AdminPassword,
            _ => value
        };
    }
}
