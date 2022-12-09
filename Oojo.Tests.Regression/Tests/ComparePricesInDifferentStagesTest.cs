using Oojo.Tests.Regression.PageObjects;
using OpenQA.Selenium.Chrome;
using Serilog;
using Xunit;

namespace Oojo.Tests.Regression.Tests;

public class ComparePricesInDifferentStagesTest: IDisposable
{
    private readonly ChromeDriver _driver;
    private const string Date = "2023-01-07";
    private const string Url = $"https://www.oojo.com/result/NYC-LHE/{Date}/business/";

    public ComparePricesInDifferentStagesTest()
    {
        _driver = new ChromeDriver();
    }
    
    [Fact]
    public void MakeSearch_ClickOnPreBookAndBookFlight_AssertPricesShouldBeTrue()
    {
        var searchResultsPage = new SearchResultsPage(_driver);

        //Open direct search URL:  
        Log.Information("Open direct search URL: {Url}", Url);
        _driver.Navigate().GoToUrl(Url);
        Log.Information("Page is opened");
        searchResultsPage.WaitPageLoad();

        Log.Information("Accept cookies and cancel member subscription");

        var cookiesBanner = searchResultsPage.GetCookiesBanner();
        cookiesBanner.ClickAcceptAllCookies();

        var memberSubscription = searchResultsPage.GetMemberSubscriptionBanner();
        memberSubscription.CancelMemberSubscription();
        memberSubscription.AssertIsNotVisible();

        Log.Information("Wait for search to finish (Loading bar has ended)");
        searchResultsPage.WaitForSearchToFinish();

        Log.Information("Assert that every found flight contains price");
        searchResultsPage.AssertPrice();
        
        Log.Information("Open any first flight");
        var referencePrice = searchResultsPage.GetFirstPrice();
        searchResultsPage.ClickOnFirstFlight();
        
        Log.Information("On the pre-book popup, check if the price matches from the search result & pre-book popup");
        var preBookPopup = searchResultsPage.GetPreBookPopup();
        preBookPopup.AssertPrice(referencePrice);
        
        Log.Information("Click on book-flight");
        var bookingPage = preBookPopup.ClickBookFlight();
        
        Log.Information("Check if the flight price is present");
        bookingPage.AssertPrice(referencePrice);
    }

    public void Dispose()
    {
        _driver.Quit();
        Log.Information("Browser successfully closed");
    }
}