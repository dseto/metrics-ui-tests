using Microsoft.Extensions.Configuration;

namespace Metrics.UiTests.Config;

public sealed record UiTestSettings(
    Uri BaseUrl,
    string Browser,
    bool Headless,
    Uri? SeleniumGridUrl
)
{
    public static UiTestSettings Load()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        // ENV overrides (prefer explicit env vars for CI)
        var baseUrl = config["UI_BASE_URL"] ?? config["UiTests:BaseUrl"] ?? "http://localhost:5173";
        var browser = config["UI_BROWSER"] ?? config["UiTests:Browser"] ?? "chrome";
        var headlessRaw = config["UI_HEADLESS"] ?? config["UiTests:Headless"] ?? "true";
        var gridRaw = config["SELENIUM_GRID_URL"] ?? config["UiTests:SeleniumGridUrl"] ?? "";

        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri))
            throw new ArgumentException($"UI_BASE_URL inválida: '{baseUrl}'");

        Uri? gridUri = null;
        if (!string.IsNullOrWhiteSpace(gridRaw))
        {
            if (!Uri.TryCreate(gridRaw, UriKind.Absolute, out var tmp))
                throw new ArgumentException($"SELENIUM_GRID_URL inválida: '{gridRaw}'");
            gridUri = tmp;
        }

        var headless = bool.TryParse(headlessRaw, out var h) && h;

        return new UiTestSettings(baseUri, browser.Trim().ToLowerInvariant(), headless, gridUri);
    }
}
