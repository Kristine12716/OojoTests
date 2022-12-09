using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Oojo.Tests.Regression.PageObjects;

public class CookiesBanner
{
    private readonly IWebDriver _driver;

    public CookiesBanner(IWebDriver driver)
    {
        _driver = driver;
    }
    
    public void WaitForCookiesBanner()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("onetrust-banner-sdk")));
    }

    public void ClickAcceptAllCookies()
    {
        IWebElement acceptCookiesButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
        acceptCookiesButton.Click();
    }
}