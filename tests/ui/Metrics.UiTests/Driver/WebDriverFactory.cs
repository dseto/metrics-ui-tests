using Metrics.UiTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Metrics.UiTests.Driver;

public static class WebDriverFactory
{
    public static IWebDriver Create(UiTestSettings settings)
    {
        // NOTE: Selenium 4+ inclui Selenium Manager (baixando driver automaticamente).
        // Se você precisar rodar offline, considere empacotar um driver ou usar Grid.

        if (settings.SeleniumGridUrl is not null)
        {
            var options = CreateOptions(settings);
            return new RemoteWebDriver(settings.SeleniumGridUrl, options);
        }

        return settings.Browser switch
        {
            "chrome" => CreateChrome(settings),
            "edge" => CreateEdge(settings),
            "firefox" => CreateFirefox(settings),
            _ => throw new ArgumentException($"Browser não suportado: '{settings.Browser}'. Use chrome|edge|firefox.")
        };
    }

    private static ICapabilities CreateOptions(UiTestSettings settings)
    {
        return settings.Browser switch
        {
            "chrome" => BuildChromeOptions(settings),
            "edge" => BuildEdgeOptions(settings),
            "firefox" => BuildFirefoxOptions(settings),
            _ => throw new ArgumentException($"Browser não suportado: '{settings.Browser}'. Use chrome|edge|firefox.")
        };
    }

    private static IWebDriver CreateChrome(UiTestSettings settings)
    {
        var options = BuildChromeOptions(settings);
        return new ChromeDriver(options);
    }

    private static ChromeOptions BuildChromeOptions(UiTestSettings settings)
    {
        var options = new ChromeOptions();
        if (settings.Headless)
        {
            // new headless (Chrome 109+)
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        return options;
    }

    private static IWebDriver CreateEdge(UiTestSettings settings)
    {
        var options = BuildEdgeOptions(settings);
        return new EdgeDriver(options);
    }

    private static EdgeOptions BuildEdgeOptions(UiTestSettings settings)
    {
        var options = new EdgeOptions();
        if (settings.Headless)
        {
            options.AddArgument("headless=new");
            options.AddArgument("window-size=1920,1080");
        }
        return options;
    }

    private static IWebDriver CreateFirefox(UiTestSettings settings)
    {
        var options = BuildFirefoxOptions(settings);
        return new FirefoxDriver(options);
    }

    private static FirefoxOptions BuildFirefoxOptions(UiTestSettings settings)
    {
        var options = new FirefoxOptions();
        if (settings.Headless)
            options.AddArgument("-headless");
        return options;
    }
}
