using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumMobileSummatorTests
{
    public class MobileSummatorTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumUrl = "http://[::1]:4723/wd/hub";
        private const string app =  @"C:\Users\Lenovo\com.example.androidappsummator.apk";

        [OneTimeSetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            options.AddAdditionalCapability("deviceName", "Pixel4");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
        [Test]
        public void TestSummator_PositivNumbers()
        {
            var field1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            field1.Clear();
            field1.SendKeys("5");
            var field2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            field2.Clear();
            field2.SendKeys("15");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            calcButton.Click();
            var fieldResult = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            Assert.AreEqual("20", fieldResult.Text);
        }

        [Test]
        public void TestSummator_InvalidValues()
        {
            var field1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            field1.Clear();
            field1.SendKeys(".");
            var field2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            field2.Clear();
            field2.SendKeys(".");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            calcButton.Click();
            var fieldResult = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            Assert.AreEqual("error", fieldResult.Text);
        }
    }
}