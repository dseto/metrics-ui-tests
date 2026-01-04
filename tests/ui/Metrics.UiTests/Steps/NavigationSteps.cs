using Metrics.UiTests.Config;
using Metrics.UiTests.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace Metrics.UiTests.Steps;

[Binding]
public sealed class NavigationSteps
{
    private readonly IWebDriver _driver;
    private readonly UiTestSettings _settings;

    public NavigationSteps(IWebDriver driver, UiTestSettings settings)
    {
        _driver = driver;
        _settings = settings;
    }

    [When("eu acesso a tela {string}")]
    public void QuandoEuAcessoATela(string screenName)
    {
        new TopNav(_driver, _settings).GoToScreen(screenName);
    }
}
