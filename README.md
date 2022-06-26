# SecureworksTestChallenge

Hi, here are the instructions in order to run the tests.

You'll need to install the following:
- .net 6 -> https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- C# Supported IDE -> (https://visualstudio.microsoft.com/downloads/ or https://www.jetbrains.com/rider/download/)
- Add SpecFlow plugin to your IDE (https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudio or https://plugins.jetbrains.com/plugin/15957-specflow-for-rider)

After you finish these steps, you can open the projects in your IDE.

We'll start with the CodeChallengeWebApp project, which contains the WebApplication that needs to be tested:
- open the .sln file and wait for the project to load
- Run the Project, assuming the localhost port is free on your device, the program will run and a browser tab with the WebApp will open, otherwise you will need to go to Properties -> LaunchSettings.json and update the ports. Attention: If you do this, you'll have to update the the port on the SpecFlowTestApp as well!

Once on the WebApp, you can start doing manual tests to get familiar with the app. You can use the files provided in this repo or you can create your own files to upload.

Now we can open SpecFlowTestApp project, which contains the automated tests that need to be run against the WebApp. After the project is loaded, you will have to go to Features -> UploadFile.feature and adjust the Scenarios to include your own files, or you can use the existing files from this repo but you'll have to update the paths based on where you cloned this repo on your local machine.

After updating all the paths, you can now run the tests. One test will fail on purpose, others should be green. 


Thank you!
