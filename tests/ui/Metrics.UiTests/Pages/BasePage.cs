using Metrics.UiTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Metrics.UiTests.Pages;

public abstract class BasePage
{
    protected BasePage(IWebDriver driver, UiTestSettings settings)
    {
        Driver = driver;
        Settings = settings;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    protected IWebDriver Driver { get; }
    protected UiTestSettings Settings { get; }
    protected WebDriverWait Wait { get; }
}
