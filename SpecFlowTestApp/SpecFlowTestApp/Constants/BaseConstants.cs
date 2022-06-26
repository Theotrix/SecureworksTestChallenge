﻿namespace SpecFlowTestApp.Constants;

public static class BaseConstants
{
    public const int DefaultDriverWaitTime = 15;
    
    public const string WebAppUri = "https://localhost:7237"; //update this var if you run the app on a different port
    public const string ElementClickable = "use this if you need to waint until element is clickable";
    public const string ElementVisible = "use this if you need to wait until element is visible";
    public const string CssSelector = "CssSelector selector";
    public const string Id = "Id selector";
}