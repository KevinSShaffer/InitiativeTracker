using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Rendering
{
    public class ConsoleRenderer : IRenderable
    {
        private Point cursorPosition;
        private const char Eraser = ' ';
        private const char ThinVerticalLine = 'o';
        private static ConsoleRenderer instance; 

        private ConsoleRenderer() { }

        public int CanvasWidth => Console.BufferWidth;

        public int CanvasHeight => Console.BufferHeight;

        public Point CursorPosition => new Point(Console.CursorLeft, Console.CursorTop);

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
        }

        private void EndDraw()
        {
            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
            Console.CursorVisible = true;
        }
    }
}
