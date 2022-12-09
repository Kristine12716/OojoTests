using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Serilog;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;

namespace Oojo.Tests.Regression.PageObjects;

public class MemberSubscriptionBanner
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'modalCustom_root')]//div[contains(@class, 'pointer')]")]
    public IWebElement CancelButton;

    public MemberSubscriptionBanner(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        PageFactory.InitElements(_driver, this);
    }

    public void AssertIsNotVisible()
    {
        _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[class='subscription-banner']")));
        Log.Information("Subscription banner is not visible");
    }


    public void WaitForMemberSubscriptionBanner()
    {
        // Move mouse back and forth in current page
        var action = new Actions(_driver);
        action.MoveByOffset(0, 0).Perform();
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class, 'modalCustom_root')]//*[contains(@class, \"subscribe_modal\")]")));
    }

    public void CancelMemberSubscription() => CancelButton.Click();
}