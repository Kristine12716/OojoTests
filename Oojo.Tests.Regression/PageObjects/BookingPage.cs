using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Oojo.Tests.Regression.PageObjects;

public class BookingPage
{
    private const string SelectorForPrice = "//div[@data-qa='_totPrice']";
    private readonly WebDriverWait _wait;

    [FindsBy(How = How.XPath, Using = SelectorForPrice)]
    // ReSharper disable once UnassignedField.Global
    public IWebElement Price;

    public BookingPage(IWebDriver driver)
    {
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        PageFactory.InitElements(driver, this);
    }

    public void AssertPrice(string referencePrice)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(SelectorForPrice)));
        Price.Text.Should().Be(referencePrice);
    }
}