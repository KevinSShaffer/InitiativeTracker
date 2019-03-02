using System;

namespace InitiativeTracker.Rendering
{
    public class ConsoleRenderer : IRenderable
    {
        private ConsoleColor currentColor;
        private Point cursorPosition;
        private const char Eraser = ' ';
        private const char ThinVerticalLine = '8';
        private const char ThinHorizontalLine = 'o';
        private const char TopRightCorner = '.';
        private const char TopLeftCorner = '.';
        private const char BottomRightCorner = '.';
        private const char BottomLeftCorner = '.';
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
            Draw(() =>
            {
                if (start.X == end.X)
                {
                    for (int i = Math.Min(start.Y, end.Y); i < Math.Max(start.Y, end.Y); i++)
                    {
                        Console.SetCursorPosition(start.X, i);
                        Console.Write(ThinVerticalLine);
                    }
                }
                else if (start.Y == end.Y)
                {
                    for (int i = Math.Min(start.X, end.X); i < Math.Max(start.X, end.X); i++)
                    {
                        Console.SetCursorPosition(i, start.Y);
                        Console.Write(ThinVerticalLine);
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

        public void DrawRectangle(Point topLeft, int width, int height)
        {
            Draw(() =>
            {
                for (int y = topLeft.Y; y < topLeft.Y + height; y++)
                {
                    Console.SetCursorPosition(topLeft.X, y);

                    if (y == topLeft.Y || y == topLeft.Y + height - 1)
                    {
                        Console.Write(y == topLeft.Y ? TopLeftCorner : BottomLeftCorner);

                        for (int x = topLeft.X + 1; x < topLeft.X + width - 1; x++)
                            Console.Write(ThinHorizontalLine);

                        Console.Write(y == topLeft.Y ? TopRightCorner : BottomRightCorner);
                    }
                    else
                    {
                        Console.Write(ThinVerticalLine);
                        Console.SetCursorPosition(topLeft.X + width - 1, y);
                        Console.Write(ThinVerticalLine);
                    }
                }
            });
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
