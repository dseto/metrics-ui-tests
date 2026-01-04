using Metrics.UiTests.Config;
using Metrics.UiTests.Pages;
using Metrics.UiTests.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;

namespace Metrics.UiTests.Steps;

[Binding]
public sealed class AuthSteps
{
    private readonly IWebDriver _driver;
    private readonly UiTestSettings _settings;
    private readonly Credentials _creds;

    public AuthSteps(IWebDriver driver, UiTestSettings settings)
    {
        _driver = driver;
        _settings = settings;
        _creds = Credentials.Load();
    }

    [Given("que eu estou autenticado como {string}")]
    public void DadoQueEuEstouAutenticadoComo(string userPlaceholder)
    {
        var username = _creds.ResolvePlaceholder(userPlaceholder);
        var password = _creds.AdminPassword;

        var login = new LoginPage(_driver, _settings);
        login.Open();
        login.Login(username, password);

        // aguarda sair do /login (contrato mínimo)
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        wait.Until(_ => !_driver.Url.Contains("/login", StringComparison.OrdinalIgnoreCase));
    }

    [Then("eu devo ver a home carregada")]
    public void EntaoEuDevoVerAHomeCarregada()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        wait.UntilVisible(ByTestId.DataTestId(UiTestIdCatalog.AppShell));
    }
}
