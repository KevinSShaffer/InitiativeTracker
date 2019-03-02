using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Rendering
{
    public interface IRenderable
    {
        int CanvasWidth { get; }
        int CanvasHeight { get; }
        Point CursorPosition { get; }

        void DrawLine(Point start, Point end);
        void DrawText(Point start, string text);
        void Erase(Point topLeft, int width, int height);
        void MoveCursor(Point point);
    }
}
