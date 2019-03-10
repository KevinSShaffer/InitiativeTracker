using InitiativeTracker.Rendering;
using System;

namespace InitiativeTracker.Components
{
    public class Button : Component
    {
        private readonly IRenderer renderer;
        private readonly Point topLeft;
        private int height = 3;
        private bool focused;

        private ConsoleColor Color => focused ? ConsoleColor.White : ConsoleColor.DarkGray;
        public string Text { get; set; }
        public int Width => Text.Length + 4;

        public Button(IRenderer renderer, Point topLeft)
        {
            this.renderer = renderer;
            this.topLeft = topLeft;
        }

        public override void Draw()
        {
            renderer.With(Color).DrawRectangle(topLeft, Width, 3);
            renderer.Erase(new Point(topLeft.X + 1, topLeft.Y + 1), Width - 2, height - 2);
            renderer.With(Color).DrawText(new Point(topLeft.X + 2, topLeft.Y + 1), Text);
        }

        public override void Focus()
        {
            focused = true;
        }

        public override void Unfocus()
        {
            focused = false;
        }
    }
}
