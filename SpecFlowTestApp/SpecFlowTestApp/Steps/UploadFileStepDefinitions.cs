using NUnit.Framework;
using SpecFlowTestApp.Constants;
using SpecFlowTestApp.Drivers;
using SpecFlowTestApp.PageObjects;

namespace SpecFlowTestApp.Steps;

[Binding]
public sealed class UploadFileStepDefinitions
{
    private readonly WebAppElements _webAppElements;

    public UploadFileStepDefinitions(ChromedriverSetup chromeDriver)
    {
        _webAppElements = new WebAppElements(chromeDriver.Current);
    }
    
    [Given(@"UploadFile Page is opened")]
    public void GivenUploadFilePageIsOpened()
    {
        _webAppElements.CheckAppTitleIsDisplayed();
    }

    [Then(@"My App title should be (.*)")]
    public void ThenMyAppTitleShouldBe(string title)
    {
        Assert.AreEqual(title, _webAppElements.GetAppTitleText());
    }

    [When(@"I add (.*) and click AnalyzeFile Button")]
    public void WhenIAddAndClickAnalyzeFileButton(string filePath)
    {
        _webAppElements.SelectFileToBeUploaded(filePath);
        _webAppElements.ClickAnalyzeFileButton();
    }

    [Then(@"The longest word should be (.*)")]
    public void ThenTheLongestWordShouldBe(string longestWord)
    {
        Assert.AreEqual(longestWord, _webAppElements.GetLongestWorldResult());
    }

    [Then(@"I should get the following error (.*)")]
    public void ThenIShouldGetTheFollowingError(string error)
    {
        Assert.AreEqual(error, _webAppElements.GetErrorMessage());
    }

    [Then(@"The longest word must be a huge word")]
    public void ThenTheLongestWordMustBeAHugeWord()
    {
        Assert.AreEqual(BaseConstants.HugeWord, _webAppElements.GetLongestWorldResult());
    }

    [When(@"I click AnalyzeFile button without any selected files")]
    public void WhenIClickAnalyzeFileButtonWithoutAnySelectedFiles()
    {
        _webAppElements.ClickAnalyzeFileButton();
    }

    [Then(@"I should not see any extracted words")]
    public void ThenIShouldNotSeeAnyExtractedWords()
    {
        Assert.AreEqual("", _webAppElements.GetLongestWorldResult());
    }

    [When(@"I validate the longest word (.*)")]
    public void WhenIValidateTheLongestWord(string longestWord)
    {
        Assert.AreEqual(longestWord, _webAppElements.GetLongestWorldResult());
    }

    [When(@"I also add the same (.*) again")]
    public void WhenIAlsoAddTheSameAgain(string filePath)
    {
        _webAppElements.SelectFileToBeUploaded(filePath);
        _webAppElements.ClickAnalyzeFileButton();
    }
}