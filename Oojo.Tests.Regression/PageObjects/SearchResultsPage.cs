using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Serilog;
#pragma warning disable CS0649

namespace Oojo.Tests.Regression.PageObjects;

class SearchResultsPage
{
    private readonly IWebDriver _driver;
    private WebDriverWait _wait;

    [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'pqBookingInfo')]")]
    public IList<IWebElement> SearchResults;

    public SearchResultsPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        PageFactory.InitElements(_driver, this);
    }
    
    public MemberSubscriptionBanner GetMemberSubscriptionBanner()
    {
        var banner = new MemberSubscriptionBanner(_driver);
        banner.WaitForMemberSubscriptionBanner();
        return banner;
    }

    public CookiesBanner GetCookiesBanner()
    {
        var banner = new CookiesBanner(_driver);
        banner.WaitForCookiesBanner();
        return banner;
    }
    
    public PreBookPopup GetPreBookPopup() => new PreBookPopup(_driver);

    public void WaitPageLoad()
    {
        var title = _driver.Title;
        title.Should().Be("NYC-LHE 2023-01-07 Flights | Oojo.com");

        //Make sure elements are visible

        IWebElement myDynamicElement = _driver.FindElement(By.XPath("//*[contains(@class, 'qa-mainLogo header_beeLogo')]"));

        Log.Information("good");
    }

    public void WaitForSearchToFinish()
    {
        _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@id='progress-bar-container']//*[@id='nprogress']")));
        Log.Information("Search finished");
    }

    public void AssertPrice()
    {
        foreach (var searchResult in SearchResults)
        {
            var price = searchResult.FindElement(By.XPath("//div//div[contains(@class, 'Price')]"));
            price.Text.Should().NotBeEmpty();
        }
    }

    public void AssertDepartureDate(string date)
    {
        foreach (var searchResult in SearchResults)
        {
            var departureDate = searchResult.FindElement(By.XPath("//div[contains(@data-qa, 'pqDateFrom')]"));
            var formatted = DateTime.Parse(date).ToString("ddd, MMM d");
            departureDate.Text.Should().Be(formatted);
            Log.Information("Departure date is correct");
        }
    }
    
    public string GetFirstPrice()
    {
        var price = SearchResults.First().FindElement(By.XPath("//div//div[contains(@class, 'Price')]"));
        price.Text.Should().NotBeEmpty();
        return price.Text;
    }
    
    public void ClickOnFirstFlight()
    {
        IWebElement acceptCookiesButton = _driver.FindElement(By.XPath("//div[contains(@class,'qa-popularAirline')]//button[text()='View details']"));
        acceptCookiesButton.Click();
        Log.Information("First selected flight clicked");
    }
}