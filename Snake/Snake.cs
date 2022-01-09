namespace Snake;

public class Snake
{
    private ConsoleColor _bodyColor { get; set; }
    private ConsoleColor _headColor { get; set; }

    public Cell Head { get; set; }

    public Queue<Cell> Body { get; set; }

    public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLenght=3)
    {
        _headColor = headColor;
        _bodyColor = bodyColor;
        Body = new Queue<Cell>();
        Head = new Cell(initialX, initialY, _headColor);

        for (var i = bodyLenght; i >= 0; i--)
        {
            Body.Enqueue(new Cell(Head.X - i - 1,initialY, _bodyColor));
        }
        Draw();
    }

    
    public void Move(Direction direction, bool eat = false)
    {
        Clear();
        Body.Enqueue(new Cell(Head.X, Head.Y, _bodyColor));

        if (!eat)
        {
         Body.Dequeue();   
        }
        
        Head = direction switch
        {
            Direction.Right => new Cell(Head.X + 1, Head.Y, _headColor),
            Direction.Left => new Cell(Head.X - 1, Head.Y, _headColor),
            Direction.Up => new Cell(Head.X, Head.Y - 1, _headColor),
            Direction.Down => new Cell(Head.X, Head.Y + 1, _headColor),
            _ => Head
        };
        Draw();
    }

    public void Draw()
    {
        Head.Draw();
        foreach (var part in Body)
        {
            part.Draw();
        }
    }
    
    public void Clear()
    {
        Head.Clear();
        foreach (var part in Body)
        {
            part.Clear();
        }
    }
}