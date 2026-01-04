using OpenQA.Selenium;

namespace Metrics.UiTests.Support;

public static class ByTestId
{
    public static By DataTestId(string testId) =>
        By.CssSelector($"[data-testid='{testId}']");
}
