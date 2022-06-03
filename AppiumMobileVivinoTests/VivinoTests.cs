using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace AppiumMobileVivinoTests
{
    public class VivinoTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string AppPackage = "vivino.web.app";
        private const string AppStartupActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string email = "test_vivino@gmail.com";
        private const string password = "p@ss987654321";

        //private const string app = @"C:\Users\Lenovo\Downloads\vivino.web.app_8.18.11.apk";


        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
           // options.AddAdditionalCapability("app", app);
            options.AddAdditionalCapability("appPackage", AppPackage);
            options.AddAdditionalCapability("appActivity", AppStartupActivity);
            options.AddAdditionalCapability("deviceName", "Pixel4");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                   
         }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        
        [Test]
        public void TestVivinoApp()
        {
            var loginLink = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            loginLink.Click();
            var emailField = driver.FindElementById("vivino.web.app:id/edtEmail");
            emailField.SendKeys(email);
            var passField = driver.FindElementById("vivino.web.app:id/edtPassword");
            passField.SendKeys(password);
            var signinButton = driver.FindElementById("vivino.web.app:id/action_signin");
            signinButton.Click();
            
            var explorerTab = driver.FindElementById("vivino.web.app:id/wine_explorer_tab"); ;
            explorerTab.Click();

            var searchButton = driver.FindElementById("vivino.web.app:id/search_vivino");
            searchButton.Click();

            var searchField = driver.FindElementById("vivino.web.app:id/editText_input");
            searchField.SendKeys("Katarzyna Reserve Red 2006");
           
                        
            var foundedProducts = driver.FindElementById("vivino.web.app:id/listviewWineListActivity");
            var firstProduct = foundedProducts.FindElementByClassName("android.widget.FrameLayout");
            firstProduct.Click();
            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.AreEqual("Reserve Red 2006", wineName.Text);
            var ratingElement = driver.FindElementById("vivino.web.app:id/rating");
            double rating = double.Parse(ratingElement.Text);
            Assert.IsTrue(rating >= 3.00 && rating <= 5.00 );

            var tabsSummary = driver.FindElementById("vivino.web.app:id/tabs");
            var highlightsTab = tabsSummary.FindElementByXPath("//android.widget.TextView[1]");
            
            highlightsTab.Click();
            var highlightsDescription = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/highlight_description\"))");

            Assert.AreEqual("Among top 1% of all wines in the world", highlightsDescription.Text);

            var factsTab = tabsSummary.FindElementByXPath("//android.widget.TextView[2]");
            factsTab.Click();
            var factsTitle = driver.FindElementById("vivino.web.app:id/wine_fact_title");
            Assert.AreEqual("Grapes",factsTitle.Text);

            var factText = driver.FindElementById("vivino.web.app:id/wine_fact_text");
            Assert.AreEqual( "Cabernet Sauvignon,Merlot", factText.Text);






        }
    }
}