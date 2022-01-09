using System.Text.Json.Serialization;

namespace Snake.Utils;

[Serializable]
public class Settings
{
    [JsonPropertyName("MapWidth")] 
    public int MapWidth { get; set; }

    [JsonPropertyName("MapHeight")] 
    public int MapHeight { get; set; }

    [JsonPropertyName("SnakeSpeed")]
    public int SnakeSpeed { get; set; }

    public Settings()
    {
        
    }

    public Settings(int mapWidth, int mapHeight, int snakeSpeed)
    {
        MapWidth = mapWidth;
        MapHeight = mapHeight;
        SnakeSpeed = snakeSpeed;
    }
}