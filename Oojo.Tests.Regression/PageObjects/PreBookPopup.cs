using FluentAssertions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Oojo.Tests.Regression.PageObjects;

public class PreBookPopup
{
    
    private readonly IWebDriver _driver;
    
    [FindsBy(How = How.XPath, Using = "//div[contains(@class,'Details_result')]//div[contains(@class, 'price')]")]
    public IWebElement Price;

    public PreBookPopup(IWebDriver driver)
    {
        _driver = driver;
        PageFactory.InitElements(_driver, this);
    }

    public void AssertPrice(string referencePrice) => Price.Text.Should().Be(referencePrice);

    public BookingPage ClickBookFlight()
    {
        IWebElement bookFlightBtn = _driver.FindElement(By.XPath( "//*[text()='Book Flight']"));
        bookFlightBtn.Click();
        return new BookingPage(_driver);
    }
}