using Oojo.Tests.Regression.PageObjects;
using OpenQA.Selenium.Chrome;
using Serilog;
using Xunit;

namespace Oojo.Tests.Regression.Tests;

public class ComparePricesAndDatesTest: IDisposable
{
    private readonly ChromeDriver _driver;
    private const string Date = "2023-01-07";
    private const string Url = $"https://www.oojo.com/result/NYC-LHE/{Date}/business/";

    public ComparePricesAndDatesTest()
    {
        _driver = new ChromeDriver();
    }
    
    [Fact]
    public void MakeSearch_AcceptCookiesAndCloseMemberSubscription_AssertPricesAndDatesShouldBeTrue()
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
        
        Log.Information("Assert that every found flight contains departure date");
        searchResultsPage.AssertDepartureDate(Date);
    }

    public void Dispose()
    {
        _driver.Quit();
        Log.Information("Browser successfully closed");
    }
}