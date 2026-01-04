using Metrics.UiTests.Config;
using Metrics.UiTests.Support;
using OpenQA.Selenium;

namespace Metrics.UiTests.Pages;

public sealed class LoginPage : BasePage
{
    public LoginPage(IWebDriver driver, UiTestSettings settings) : base(driver, settings) { }

    public void Open()
    {
        Driver.Navigate().GoToUrl(new Uri(Settings.BaseUrl, "/login"));
    }

    public void Login(string username, string password)
    {
        var u = Wait.UntilVisible(ByTestId.DataTestId(UiTestIdCatalog.LoginUsername));
        u.Clear();
        u.SendKeys(username);

        var p = Driver.FindElement(ByTestId.DataTestId(UiTestIdCatalog.LoginPassword));
        p.Clear();
        p.SendKeys(password);

        Driver.FindElement(ByTestId.DataTestId(UiTestIdCatalog.LoginSubmit)).Click();
    }
}
