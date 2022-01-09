namespace Snake.Utils;

[Serializable]
public class PlayerProgress : Settings
{
    private int Score { get; set; }
    private int Id { get; set; }

    public PlayerProgress()
    {
        
    }

    public PlayerProgress(int id, int score,int mapWidth, int mapHeight,int snakeSpeed)
        :base(mapWidth,mapHeight,snakeSpeed)
    {
        Score = score;
        Id = id;
    }

    public override string ToString()
    {
        return $"{Id} ,score: {Score}, speed:{SnakeSpeed}, map: {MapWidth},{MapHeight}";
    }
}