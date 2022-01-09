namespace Snake;

public class Cell
{
    private const char CellChar='#';
    public int X { get; }

    public int Y { get; }

    public ConsoleColor Color { get; }

    public Cell(int x, int y, ConsoleColor color)
    {
        X = x;
        Y = y;
        Color = color;
    }

    public void Draw()
    {
        Console.ForegroundColor = Color;
        Console.SetCursorPosition(X,Y);
        Console.Write(CellChar);
    }

    public void Clear()
    {
        Console.SetCursorPosition(X,Y);
        Console.Write(' ');
    }
}