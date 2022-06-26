using SpecFlowTestApp.Drivers;
using SpecFlowTestApp.PageObjects;

namespace SpecFlowTestApp.Hooks
{
    [Binding]
    public class WebAppHooks
    {
        [BeforeScenario]
        public static void BeforeScenario(ChromedriverSetup chromeDriver)
        {
            var webAppElements = new WebAppElements(chromeDriver.Current);
            webAppElements.OpenUploadPage();
        }

        [AfterScenario]
        public static void CloseDriver(ChromedriverSetup chromeDriver)
        {
            chromeDriver.Dispose();
        }
    }
}