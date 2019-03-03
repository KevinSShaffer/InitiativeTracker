using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Tests.Mocks
{
    public class MockRenderer : IRenderer
    {
        public int CanvasWidth => throw new NotImplementedException();

        public int CanvasHeight => throw new NotImplementedException();

        public Point CursorPosition => throw new NotImplementedException();

        public ConsoleColor ForegroundColor { get; set; }

        public void DrawLine(Point start, Point end)
        {

        }

        public void DrawRectangle(Point topLeft, int width, int height)
        {

        }

        public void DrawText(Point start, string text)
        {
        }

        public void Erase(Point topLeft, int width, int height)
        {

        }

        public void MoveCursor(Point point)
        {

        }

        public void ResetColor()
        {

        }
    }
}
