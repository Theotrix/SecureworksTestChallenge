
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowTestApp.Drivers
{
    public class ChromedriverSetup : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriver;
        private bool _isDisposed;

        public ChromedriverSetup()
        {
            _currentWebDriver = new Lazy<IWebDriver>(CreateWebDriver);
        }

        public IWebDriver Current => _currentWebDriver.Value;

        private static IWebDriver CreateWebDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--window-size=1280,1024");

            return new ChromeDriver(chromeDriverService, chromeOptions);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriver.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}