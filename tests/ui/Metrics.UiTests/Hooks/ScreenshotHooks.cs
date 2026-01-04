using OpenQA.Selenium;
using Reqnroll;

namespace Metrics.UiTests.Hooks;

[Binding]
public sealed class ScreenshotHooks
{
    private readonly IWebDriver _driver;
    private readonly ScenarioContext _scenario;

    public ScreenshotHooks(IWebDriver driver, ScenarioContext scenario)
    {
        _driver = driver;
        _scenario = scenario;
    }

    [AfterStep]
    public void AfterStepTakeScreenshotOnError()
    {
        if (_scenario.TestError is null) return;

        try
        {
            if (_driver is ITakesScreenshot sc)
            {
                var bytes = sc.GetScreenshot().AsByteArray;
                // Reqnroll / xUnit attach support varies by runner. Keep it simple:
                // write file next to test output
                var fileName = $"screenshot_{Sanitize(_scenario.ScenarioInfo.Title)}_{DateTimeOffset.UtcNow:yyyyMMdd_HHmmss}.png";
                var path = Path.Combine(AppContext.BaseDirectory, fileName);
                File.WriteAllBytes(path, bytes);
            }
        }
        catch
        {
            // ignore screenshot errors
        }
    }

    private static string Sanitize(string value)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
            value = value.Replace(c, '_');
        return value;
    }
}
