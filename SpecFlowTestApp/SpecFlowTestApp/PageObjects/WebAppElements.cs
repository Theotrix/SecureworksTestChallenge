using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowTestApp.Constants;
using SpecFlowTestApp.Exceptions;

namespace SpecFlowTestApp.PageObjects;

public class WebAppElements
{
    private readonly IWebDriver _webDriver;

    public WebAppElements(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    
    //searching by id, setting driver wait to 5 seconds and waiting until element is visible
    private IWebElement UploadFileButton => SearchElement(BaseConstants.Id, "UploadedFile", 5, BaseConstants.ElementVisible);

    //searching by css selector
    private IWebElement AnalyzeFileButton => SearchElement(BaseConstants.CssSelector, "body > div > main > div:nth-child(4) > form > input[type=submit]:nth-child(2)");
    private IWebElement MyTestAppTitle => SearchElement(BaseConstants.CssSelector, "body > div > main > div:nth-child(1) > h1");
    private IWebElement LongestWordResult => SearchElement(BaseConstants.Id, "longestWord");

    //unsafe page elements
    private IWebElement AdvancedButton => SearchElement(BaseConstants.Id, "details-button", BaseConstants.ElementVisible);
    private IWebElement ProceedLink => SearchElement(BaseConstants.Id, "proceed-link", BaseConstants.ElementVisible);

    //error page elements
    private IWebElement ErrorMessage => SearchElement(BaseConstants.CssSelector, "#stackpage > ul > li:nth-child(1) > h2", 5, BaseConstants.ElementVisible);
    
    public void ClickAnalyzeFileButton()
    {
        AnalyzeFileButton.Click();
    }

    public void SelectFileToBeUploaded(string filePath)
    {
        UploadFileButton.SendKeys(filePath);
    }

    public string GetLongestWorldResult()
    {
        return LongestWordResult.Text;
    }
    
    public string GetErrorMessage()
    {
        return ErrorMessage.Text;
    }
    
    public string GetAppTitleText()
    {
        return MyTestAppTitle.Text;
    }

    public void CheckAppTitleIsDisplayed()
    {
        if (!MyTestAppTitle.Displayed)
        {
            throw new Exception(BaseExceptions.MyAppTitleNotFound);
        }
    }

    public void OpenUploadPage()
    {
        _webDriver.Navigate().GoToUrl(BaseConstants.WebAppUri);
        SkipUnsafePageWarning();
    }

    #region Private Helpers

    private void SkipUnsafePageWarning()
    {
        AdvancedButton.Click();
        ProceedLink.Click();
    }

    private static By SetByForSeleniumElement(string stringBy, string identifier)
    {
        var by = stringBy switch
        {
            BaseConstants.CssSelector => By.CssSelector(identifier),
            BaseConstants.Id => By.Id(identifier),
            _ => throw new ArgumentOutOfRangeException(nameof(stringBy), stringBy, "Invalid or no By selected.")
        };
        //review function
        return by;
    }

    private IWebElement SearchElement(string by, string identifier, string? neededElementStatus = null)
    {
        return SearchElement(SetByForSeleniumElement(by, identifier), BaseConstants.DefaultDriverWaitTime, neededElementStatus);
    }

    private IWebElement SearchElement(string by, string identifier, int seconds, string? neededElementStatus = null)
    {
        return SearchElement(SetByForSeleniumElement(by, identifier), seconds,
            neededElementStatus);
    }
    
    /// <summary>
    /// Waits until an element is found or visible and returns it
    /// </summary>
    /// <param name="by">The element identifier</param>
    /// <param name="seconds">Timeout in seconds</param>
    /// <param name="neededElementStatus">Wait for the element to become visible/clickable</param>
    /// <returns>IWebElement</returns>
    private IWebElement SearchElement(By by, int seconds, string? neededElementStatus = null)
    {
        //review function
        try
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(seconds));
            var element = wait.Until(el => el.FindElement(by));
            if (neededElementStatus != null)
            {
                element = neededElementStatus switch
                {
                    BaseConstants.ElementVisible => wait.Until(
                        ExpectedConditions.ElementIsVisible(by)),
                    BaseConstants.ElementClickable => wait.Until(
                        ExpectedConditions.ElementToBeClickable(by)),
                    _ => element
                };
            }
            
            return element;
        }
        catch (WebDriverTimeoutException)
        {
            throw new BaseExceptions(by + BaseExceptions.ElementNotFound + seconds + "seconds");
        }
        catch (ElementNotVisibleException)
        {
            throw new BaseExceptions(by + BaseExceptions.ElementNotVisible + seconds + "seconds");
        }
        catch (ElementNotInteractableException)
        {
            throw new BaseExceptions(by + BaseExceptions.ElementNotInteractable + seconds + "seconds");
        }
    }
    #endregion
}