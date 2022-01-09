using System.Diagnostics;
using Snake.Utils;

namespace Snake
{
    public class Program
    {
        private static int MapWidth;
        private static int MapHeight;
        private static int SnakeSpeed;
        
        private const ConsoleColor _foodColor = ConsoleColor.Yellow;
            
        static void Main()
        {
            var progressHelper = new ProgressHelper();
            var savedRecords = progressHelper.ReadProgress();
            var stHelper = new SettingsHelper();
            var settings = stHelper.ReadSettings();
            MapWidth = settings.MapWidth;
            MapHeight = settings.MapHeight;
            SnakeSpeed = settings.SnakeSpeed;
            
            var score = 0;
            var frameRate = 0; 
            var snake = new Snake(10,10, ConsoleColor.Red,ConsoleColor.Green);
            var currentMovmnet = Direction.Right;
            var stopWatch = new Stopwatch();
            
            Console.CursorVisible = false;
            DrawBorders();
            
            var food = GenerateFood(snake,_foodColor);
            food.Draw();
            
            while (true)
            {
                stopWatch.Restart();
                var oldMovement = currentMovmnet;
                
                while (stopWatch.ElapsedMilliseconds <= SnakeSpeed - frameRate)
                {
                    if (currentMovmnet == oldMovement)
                    {
                        currentMovmnet = ReadMovement(currentMovmnet);
                    }
                }
                stopWatch.Restart();
                
                if (food.X == snake.Head.X && food.Y == snake.Head.Y)
                {
                    snake.Move(currentMovmnet,true);
                    food = GenerateFood(snake,_foodColor);
                    food.Draw();
                    score++;
                }
                else
                {
                    snake.Move(currentMovmnet);
                }

                frameRate = (int) stopWatch.ElapsedMilliseconds;


                if (snake.Head.X == 0
                    || snake.Head.Y == 0
                    || snake.Head.X == MapWidth - 1
                    || snake.Head.Y == MapHeight - 1
                    || snake.Body.Any(item => item.X == snake.Head.X && item.Y == snake.Head.Y))
                {
                   break;
                }
            }

            ShowEndGameMessage(score);
            savedRecords.Add(new PlayerProgress(savedRecords.Count,score, MapWidth, MapHeight, SnakeSpeed));
            progressHelper.SaveProgress(savedRecords);
            ShowListOfRecords(savedRecords);
        }

        static void ShowListOfRecords(List<PlayerProgress> records)
        {
            foreach (var record in records)
            {
                Console.SetCursorPosition(MapWidth+5,MapHeight/2);
                Console.WriteLine(record);
            }
        }

        static void ShowEndGameMessage(int score)
        {
            Console.SetCursorPosition(MapWidth+5,MapHeight/2);
            Console.WriteLine($"Game over, score: {score}");
        }

        static Direction ReadMovement(Direction currentDirection)
        {
            if (!Console.KeyAvailable)
            {
                return currentDirection;
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };
            
            return currentDirection;
        }

        static Cell GenerateFood(Snake snake,ConsoleColor foodColor)
        {
            Cell food;
            do
            {
                food = new Cell(new Random().Next(1,MapWidth-4),new Random().Next(1,MapHeight-4),foodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y 
                     || snake.Body.Any(item => item.X == food.X && item.Y == food.Y));

            return food;
        }

        static void DrawBorders()
        {
            for (var i = 0; i < MapWidth; i++)
            {
                new Cell(i, 0,ConsoleColor.White).Draw();
                new Cell(MapWidth, i,ConsoleColor.White).Draw();
            }
            for (var i = 0; i < MapHeight; i++)
            {
                new Cell(0, i,ConsoleColor.White).Draw();
                new Cell(i, MapHeight-1,ConsoleColor.White).Draw();
            }
        }

    }
}

