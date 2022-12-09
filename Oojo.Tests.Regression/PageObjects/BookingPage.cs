using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Oojo.Tests.Regression.PageObjects;

public class BookingPage
{
    private const string SelectorForPrice = "//div[@data-qa='_totPrice']";
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    [FindsBy(How = How.XPath, Using = SelectorForPrice)]
    public IWebElement Price;

    public BookingPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        PageFactory.InitElements(_driver, this);
    }

    public void AssertPrice(string referencePrice)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(SelectorForPrice)));
        Price.Text.Should().Be(referencePrice);
    }
}