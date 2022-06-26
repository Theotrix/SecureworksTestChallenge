namespace SpecFlowTestApp.Exceptions;

public class BaseExceptions : Exception
{
    public BaseExceptions(string message) : base(message)
    {
    }

    public const string ElementNotVisible = " did not become visible after ";
    public const string ElementNotFound = " was not found after ";
    public const string ElementNotInteractable = " did not become interactable after ";
    public const string MyAppTitleNotFound = "The app title isn't displayed, check the page.";
}