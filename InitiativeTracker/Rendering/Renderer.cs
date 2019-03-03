using System;

namespace InitiativeTracker.Rendering
{
    public class ConsoleRenderer : IRenderer
    {
        private ConsoleColor currentColor;
        private Point cursorPosition;
        private const char Eraser = ' ';
        private static ConsoleRenderer instance; 

        public int CanvasWidth => Console.BufferWidth;

        public int CanvasHeight => Console.BufferHeight;

        public Point CursorPosition => new Point(Console.CursorLeft, Console.CursorTop);

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        private ConsoleRenderer() { }

        public static ConsoleRenderer Instance()
        {
            if (instance == null)
                instance = new ConsoleRenderer();

            return instance;
        }

        public void DrawLine(Point start, Point end)
        {
            DrawLine(start, end, LineWidth.Thin);
        }

        public void DrawLine(Point start, Point end, LineWidth lineWidth)
        {
            Draw(() =>
            {
                if (start.X == end.X)
                {
                    for (int i = Math.Min(start.Y, end.Y); i < Math.Max(start.Y, end.Y); i++)
                    {
                        Console.SetCursorPosition(start.X, i);
                        Console.Write(Line.Set(lineWidth).Vertical);
                    }
                }
                else if (start.Y == end.Y)
                {
                    for (int i = Math.Min(start.X, end.X); i < Math.Max(start.X, end.X); i++)
                    {
                        Console.SetCursorPosition(i, start.Y);
                        Console.Write(Line.Set(lineWidth).Horizontal);
                    }
                }
                else
                {
                    EndDraw();
                    throw new ArgumentException("start and end must share a value on a dimension.");
                }
            });
        }

        public void DrawText(Point start, string text)
        {
            Draw(() =>
            {
                Console.SetCursorPosition(start.X, start.Y);
                Console.Write(text);
            });
        }

        public void DrawRectangle(Point topLeft, int width, int height)
        {
            DrawRectangle(topLeft, width, height, LineWidth.Thin);
        }

        public void DrawRectangle(Point topLeft, int width, int height, LineWidth lineWidth)
        {
            Draw(() =>
            {
                for (int y = topLeft.Y; y < topLeft.Y + height; y++)
                {
                    Console.SetCursorPosition(topLeft.X, y);

                    if (y == topLeft.Y || y == topLeft.Y + height - 1)
                    {
                        Console.Write(y == topLeft.Y ? Line.Set(lineWidth).TopLeft : Line.Set(lineWidth).BottomLeft);

                        for (int x = topLeft.X + 1; x < topLeft.X + width - 1; x++)
                            Console.Write(Line.Set(lineWidth).Horizontal);

                        Console.Write(y == topLeft.Y ? Line.Set(lineWidth).TopRight : Line.Set(lineWidth).BottomRight);
                    }
                    else
                    {
                        Console.Write(Line.Set(lineWidth).Vertical);
                        Console.SetCursorPosition(topLeft.X + width - 1, y);
                        Console.Write(Line.Set(lineWidth).Vertical);
                    }
                }
            });
        }

        public void Erase(Point topLeft, int width, int height)
        {
            Draw(() =>
            {
                for (int x = topLeft.X; x < width + topLeft.X; x++)
                    for (int y = topLeft.Y; y < height + topLeft.Y; y++)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(Eraser);
                    }
            });
        }

        public void MoveCursor(Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }

        private void Draw(Action instructions)
        {
            StartDraw();
            instructions();
            EndDraw();
        }

        private void StartDraw()
        {
            cursorPosition = new Point(Console.CursorLeft, Console.CursorTop);
            Console.CursorVisible = false;
            currentColor = Console.ForegroundColor;
        }

        private void EndDraw()
        {
            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
            Console.CursorVisible = true;
            Console.ForegroundColor = currentColor;
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }

    public static class Line
    {
        public class RectangleLineSet
        {
            public char Vertical { get; private set; }
            public char Horizontal { get; private set; }
            public char TopLeft { get; private set; }
            public char TopRight { get; private set; }
            public char BottomRight { get; private set; }
            public char BottomLeft { get; private set; }

            public RectangleLineSet(char vertical, char horizontal, char topRight, char topLeft, char bottomRight, char bottomLeft)
            {
                Vertical = vertical;
                Horizontal = horizontal;
                TopLeft = topLeft;
                TopRight = topRight;
                BottomRight = bottomRight;
                BottomLeft = bottomLeft;
            }
        }

        public static RectangleLineSet Thick => new RectangleLineSet('║', '═', '╗', '╔', '╝', '╚');
        public static RectangleLineSet Thin => new RectangleLineSet('│', '─', '┐', '┌', '┘', '└');

        public static RectangleLineSet Set(LineWidth lineWidth)
        {
            switch(lineWidth)
            {
                case LineWidth.Thin:
                    return Thin;
                case LineWidth.Thick:
                    return Thick;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public enum LineWidth
    {
        Thick,
        Thin
    }
}
