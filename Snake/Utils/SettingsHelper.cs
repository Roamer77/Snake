using System.Text;
using System.Text.Json;

namespace Snake.Utils;

public class SettingsHelper
{
  
    private const string FileName = "settings.json";
    private const string DirName = "Settings";
    private string SettingsDirPath;

    public SettingsHelper()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var projectDir = currentDir.Substring(0, currentDir.LastIndexOf("bin"));
        SettingsDirPath = projectDir + DirName;
    }
    public void SaveSettings(Settings settings)
    {
        var res = JsonSerializer.Serialize(settings);
        
       if (!Directory.Exists(SettingsDirPath))
       {
           Directory.CreateDirectory(SettingsDirPath);
       }
       try
       {
           using (var fs = new FileStream($"{SettingsDirPath}/{FileName}",FileMode.OpenOrCreate))
           {
               byte[] input = Encoding.Default.GetBytes(res);
               fs.Write(input, 0, input.Length);
           }
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
       }
    }

    public Settings ReadSettings()
    {
        var defaultSettings = new Settings(20, 20, 3);
        try
        {
            var readingResult = File.ReadAllText($"{SettingsDirPath}/{FileName}", Encoding.Default);
            return JsonSerializer.Deserialize<Settings>(readingResult) ?? defaultSettings;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return defaultSettings;
    }
}