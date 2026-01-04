using Metrics.UiTests.Config;
using Metrics.UiTests.Support;
using OpenQA.Selenium;

namespace Metrics.UiTests.Pages;

public sealed class TopNav : BasePage
{
    public TopNav(IWebDriver driver, UiTestSettings settings) : base(driver, settings) { }

    public void GoToScreen(string screenName)
    {
        var navTestId = UiTestIdCatalog.TryResolveScreenNav(screenName)
                       ?? throw new ArgumentException($"Tela não mapeada no UiTestIdCatalog: '{screenName}'");

        Wait.UntilVisible(ByTestId.DataTestId(UiTestIdCatalog.AppShell));

        Driver.FindElement(ByTestId.DataTestId(navTestId)).Click();
    }
}
