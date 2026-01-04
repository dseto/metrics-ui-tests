using Metrics.UiTests.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;

namespace Metrics.UiTests.Steps;

[Binding]
public sealed class UiInteractionSteps
{
    private readonly IWebDriver _driver;

    public UiInteractionSteps(IWebDriver driver)
    {
        _driver = driver;
    }

    [When("eu clico em {string}")]
    [When("eu clico no botão {string}")]
    public void QuandoEuClicoEm(string buttonLabel)
    {
        var testId = UiTestIdCatalog.TryResolveButton(buttonLabel);
        if (!string.IsNullOrWhiteSpace(testId))
        {
            _driver.FindElement(ByTestId.DataTestId(testId)).Click();
            return;
        }

        // fallback: localizar por texto (menos estável). Mantido apenas para bootstrap do projeto.
        var xpath = $"//button[normalize-space()='{EscapeXPath(buttonLabel)}' or .//span[normalize-space()='{EscapeXPath(buttonLabel)}']]";
        _driver.FindElement(By.XPath(xpath)).Click();
    }

    [When("eu informo o nome {string}")]
    public void QuandoEuInformoONome(string value)
    {
        _driver.FindElement(ByTestId.DataTestId(UiTestIdCatalog.ConnectorName)).SendKeys(value);
    }

    [When("eu informo o baseUrl {string}")]
    public void QuandoEuInformoOBaseUrl(string value)
    {
        _driver.FindElement(ByTestId.DataTestId(UiTestIdCatalog.ConnectorBaseUrl)).SendKeys(value);
    }

    [Then("eu devo ver a lista de conectores")]
    public void EntaoEuDevoVerAListaDeConectores()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        wait.UntilVisible(ByTestId.DataTestId(UiTestIdCatalog.ConnectorsTable));
    }

    private static string EscapeXPath(string value) =>
        value.Replace("'", "’");
}
