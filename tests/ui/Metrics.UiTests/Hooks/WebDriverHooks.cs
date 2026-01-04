using Metrics.UiTests.Config;
using Metrics.UiTests.Driver;
using OpenQA.Selenium;
using Reqnroll;

namespace Metrics.UiTests.Hooks;

[Binding]
public sealed class WebDriverHooks
{
    private readonly IObjectContainer _container;

    public WebDriverHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario(Order = 0)]
    public void CreateAndRegisterWebDriver()
    {
        var settings = UiTestSettings.Load();
        var driver = WebDriverFactory.Create(settings);

        // defaults
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

        _container.RegisterInstanceAs(settings);
        _container.RegisterInstanceAs(driver);
    }

    [AfterScenario(Order = 100)]
    public void QuitWebDriver()
    {
        var driver = _container.Resolve<IWebDriver>();
        try
        {
            driver.Quit();
        }
        catch
        {
            // ignore shutdown errors
        }
        finally
        {
            driver.Dispose();
        }
    }
}
