using InitiativeTracker.Rendering;
using System;

namespace InitiativeTracker.Tests.Mocks
{
    public class MockRenderer : IRenderer
    {
        private Point point = new Point(0, 0);

        public int CanvasWidth => throw new NotImplementedException();

        public int CanvasHeight => throw new NotImplementedException();

        public Point CursorPosition => point;
        public bool CursorVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DrawLine(Point start, Point end)
        {

        }

        public void DrawLine(Point start, Point end, LineWidth lineWidth)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Point topLeft, int width, int height)
        {

        }

        public void DrawRectangle(Point topLeft, int width, int height, LineWidth lineWidth)
        {
            throw new NotImplementedException();
        }

        public void DrawText(Point start, string text)
        {
        }

        public void Erase(Point topLeft, int width, int height)
        {

        }

        public void MoveCursor(Point point)
        {
            this.point = point;
        }

        public void SetWindowSize(int width, int height)
        {
            throw new NotImplementedException();
        }

        public IRenderer With(ConsoleColor color)
        {
            throw new NotImplementedException();
        }
    }
}
