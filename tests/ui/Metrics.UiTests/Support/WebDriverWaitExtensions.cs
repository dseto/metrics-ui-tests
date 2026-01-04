using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Metrics.UiTests.Support;

public static class WebDriverWaitExtensions
{
    public static IWebElement UntilVisible(this WebDriverWait wait, By by)
    {
        return wait.Until(driver =>
        {
            var el = driver.FindElement(by);
            return el.Displayed ? el : null;
        })!;
    }

    public static bool UntilGone(this WebDriverWait wait, By by)
    {
        return wait.Until(driver =>
        {
            try
            {
                var el = driver.FindElement(by);
                return !el.Displayed;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        });
    }
}
