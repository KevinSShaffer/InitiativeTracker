using System;

namespace InitiativeTracker.Rendering
{
    public interface IRenderer
    {
        int CanvasWidth { get; }
        int CanvasHeight { get; }
        Point CursorPosition { get; }
        bool CursorVisible { get; set; }

        void DrawRectangle(Point topLeft, int width, int height);
        void DrawRectangle(Point topLeft, int width, int height, LineWidth lineWidth);
        void DrawLine(Point start, Point end);
        void DrawLine(Point start, Point end, LineWidth lineWidth);
        void DrawText(Point start, string text);
        void Erase(Point topLeft, int width, int height);
        void MoveCursor(Point point);
        void SetWindowSize(int width, int height);
        IRenderer With(ConsoleColor color);
    }
}
