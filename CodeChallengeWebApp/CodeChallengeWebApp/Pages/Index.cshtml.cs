using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeChallengeWebApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public IFormFile UploadedFile { get; set; } = null!;
    
    [BindProperty]
    public string LongestWord { get; set; } = null!;

    public async Task OnPostAsync()
    {
        var filePath = Path.GetFullPath(UploadedFile.Name);
        
        CheckFileExtenstion(UploadedFile);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await UploadedFile.CopyToAsync(stream);
        }
        
        LongestWord = ExtractLongestWordFromLine(await ExtractLongestLineFromFile(UploadedFile) ?? string.Empty);
    }

    #region PrivateHelpers

    private async Task<string?> ExtractLongestLineFromFile(IFormFile file)
    {
        var textFromFile = await System.IO.File.ReadAllLinesAsync(UploadedFile.Name);
        var longestLine = string.Empty;
        var ctr = 0;
        
        foreach (var s in textFromFile)
        {
            if (s.Length > ctr)
            {
                longestLine = s;
                ctr = s.Length;
            }
        }
        
        return string.IsNullOrEmpty(longestLine) ? "Interesting error, keep testing!" : longestLine;
    }

    private static string ExtractLongestWordFromLine(string longestLine)
    {
        if (string.IsNullOrEmpty(longestLine))
            throw new Exception("There has been an error, file may not contain any strings!");
        
        var longestWord = string.Empty;
        var words = longestLine.Split(new[] { " " }, StringSplitOptions.None);
        var ctr = 0;
        foreach (var s in words)
        {
            if (s.Length > ctr)
            {
                longestWord = s;
                ctr = s.Length;
            }
        }

        return longestWord;
    }

    private static void CheckFileExtenstion(IFormFile fileToCheck)
    {
        var getNameForExtensionCheck = Path.GetFullPath(fileToCheck.FileName);
        var ext = Path.GetExtension(getNameForExtensionCheck);
        switch (ext.ToLower())
        {
            case ".gif":
                throw new Exception("Invalid file type uploaded. Is it pronounced Gif or Jif?");
            case ".jpg":
                throw new Exception("Invalid file type uploaded. Not the best quality image, try selecting a text file please.");
            case ".jpeg":
                throw new Exception("Invalid file type uploaded. Can I get the job now?");
            case ".png":
                throw new Exception("Invalid file type uploaded. You really want to break my code, don't you?");
        }

        if (ext != ".txt")
        {
            throw new Exception("You wanted a joke, right? Joke's on you, select a .txt file!");
        }
    }

    #endregion
}