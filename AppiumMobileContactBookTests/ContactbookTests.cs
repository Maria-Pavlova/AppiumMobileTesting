using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace AppiumMobileContactBookTests
{
    public class ContactbookTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string app = @"C:\Users\Lenovo\AppiumMobileResources\contactbook-androidclient.apk";
       // private const string apiUrl = "http://contactbook.nakov.repl.co/api";
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            options.AddAdditionalCapability("deviceName", "Pixel4");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
          //  var apiUrlField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
          // apiUrlField.Clear();
         //   apiUrlField.SendKeys(apiUrl);
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();
        }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
       

        [Test]
        public void Test_Search_Existing_Contact()
        {
           
            var searchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchField.Clear();
            searchField.SendKeys("Steve");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");

            wait.Until(t => result.Text != "");
            Assert.AreEqual("Contacts found: 1", result.Text);
            var firstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
            Assert.AreEqual("Steve", firstName.Text);
            var lastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");
            Assert.AreEqual("Jobs", lastName.Text);

        }

        [Test]
        public void Test_Search_NotExisting_Contact()
        {
            
            var searchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchField.Clear();
            searchField.SendKeys("Ivan");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");

            wait.Until(t => result.Text != "");
            Assert.That( result.Text.Contains("Contacts found: 0"));
            

        }

        [Test]
        public void Test_Search_Multiple_Contacts()
        {

            var searchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchField.Clear();
            searchField.SendKeys("M");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");

            wait.Until(t => result.Text != "");
            Assert.AreEqual("Contacts found: 5", result.Text);
            var firstNames = driver.FindElementsById("contactbook.androidclient:id/textViewFirstName");
            Assert.AreEqual("Steve", firstNames.First().Text);
            Assert.AreEqual("Albert", firstNames[2].Text);
            

        }
    }
}