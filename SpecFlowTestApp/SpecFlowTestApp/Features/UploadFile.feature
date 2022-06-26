Feature: UploadFile
	Testing functionality of an UploadFile button accepting .txt files and returning longest word from said file

@UploadFile
Scenario: Check the title of the page
	Given UploadFile Page is opened
	Then My App title should be Welcome to my test app
	
Scenario: Add various files and check the longest word
	Given UploadFile Page is opened
	When I add <filePath> and click AnalyzeFile Button
	Then The longest word should be <longestWord>
	
Scenarios: 
| filePath                                              | longestWord                |
| E:/Secureworks/FilesToUpload/test.txt                 | longlinewiththelongestword |
| E:/Secureworks/FilesToUpload/teasdasdast.txt          | longestwordheh             |
| E:/Secureworks/FilesToUpload/specialCharsăîțâName.txt | hmmthisactuallyworks       |
| E:/Secureworks/FilesToUpload/specialCharInFile.txt    | îăîțîăîțîăîțăîțîățwdqw     |
| E:/Secureworks/FilesToUpload/zeroSizeFile.txt         | file is empty on purpose   |


Scenario: Add various invalid files and try to break the app
	Given UploadFile Page is opened
	When I add <filePath> and click AnalyzeFile Button
	Then I should get the following error <error>
	
Scenarios: 
| filePath                                   | error                                                                                                |
| E:/Secureworks/FilesToUpload/someJpg.jpg   | Exception: Invalid file type uploaded. Not the best quality image, try selecting a text file please. |
| E:/Secureworks/FilesToUpload/somePng.png   | Exception: Invalid file type uploaded. You really want to break my code, don't you?                  |
| E:/Secureworks/FilesToUpload/someTiff.tiff | Exception: You wanted a joke, right? Joke's on you, select a .txt file!                              |
| E:/Secureworks/FilesToUpload/someGif.gif   | Exception: Invalid file type uploaded. Is it pronounced Gif or Jif?                                  |
| E:/Secureworks/FilesToUpload/someJpeg.jpeg | Exception: Invalid file type uploaded. Can I get the job now?                                        |


Scenario: Testing a file with a very long word
	Given UploadFile Page is opened
	When I add E:/Secureworks/FilesToUpload/hugeFile.txt and click AnalyzeFile Button
	Then The longest word must be a huge word
	

Scenario: I click AnalyzeFile without having a file selected
	Given UploadFile Page is opened
	When I click AnalyzeFile button without any selected files
	Then I should get the following error NullReferenceException: Object reference not set to an instance of an object.
	

Scenario: Check no word is displayed when opening the page
	Given UploadFile Page is opened
	Then I should not see any extracted words
	

Scenario: Upload the same file twice and check results
	Given UploadFile Page is opened
	When I add <filePath> and click AnalyzeFile Button
	And I validate the longest word <longestWord>
	But I also add the same <filePath> again
	Then The longest word should be <longestWord>
	
Scenarios:
  | filePath                                     | longestWord                |
  | E:/Secureworks/FilesToUpload/test.txt        | longlinewiththelongestword |
  | E:/Secureworks/FilesToUpload/teasdasdast.txt | longestwordheh             |