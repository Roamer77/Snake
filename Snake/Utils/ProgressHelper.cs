using System.Runtime.Serialization.Formatters.Binary;

namespace Snake.Utils;

public class ProgressHelper
{
    private string DirName = "Progress";
    private string FileName = "PlayerProgress.dat";
    private string ProgressDirPath;
    private BinaryFormatter _formatter;

    public ProgressHelper()
    {
        _formatter = new BinaryFormatter();
        var currentDir = Directory.GetCurrentDirectory();
        var projectDir = currentDir.Substring(0, currentDir.LastIndexOf("bin"));
        ProgressDirPath = projectDir + DirName;
        CreateNecessaryDir();
        CreateFile();
    }
    public void SaveProgress(List<PlayerProgress> records)
    {
        using (var fs = new FileStream($"{ProgressDirPath}/{FileName}", FileMode.Open))
        {
            _formatter.Serialize(fs, records);
            Console.WriteLine("Record have been saved");
        }
    }

    public List<PlayerProgress> ReadProgress()
    {
        using (var fs = new FileStream($"{ProgressDirPath}/{FileName}", FileMode.Open))
        {
            return fs.Length != 0 ? (List<PlayerProgress>) _formatter.Deserialize(fs) : new List<PlayerProgress>();
        }
    }
    
    private void CreateNecessaryDir()
    {
        if (!Directory.Exists(ProgressDirPath))
        {
            Directory.CreateDirectory(ProgressDirPath);
        }
    }

    private void CreateFile()
    {
        if (!File.Exists($"{ProgressDirPath}/{FileName}"))
        {
            File.Create($"{ProgressDirPath}/{FileName}");
        }
    }
}