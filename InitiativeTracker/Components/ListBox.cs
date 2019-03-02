using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;

namespace InitiativeTracker.Components
{
    public class ListBox : Component
    {
        private readonly int x;
        private readonly int y;
        private readonly int width;
        private readonly int height;
        private readonly List<string> list; 
        private readonly IRenderable renderer;
        private int position = 0;

        public int Padding { get; set; } = 1;
        public int Limit => height - 2;

        public ListBox(Point point, int width, int height)
        {
            x = point.X;
            y = point.Y;
            this.width = width;
            this.height = height;
            list = new List<string>();
            renderer = RenderFactory.GetRenderer();

            UpArrowPressed += ListBox_UpArrowPressed;
            DownArrowPressed += ListBox_DownArrowPressed;
        }

        private void ListBox_DownArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (position < list.Count - 1)
                position++;

            Draw();
        }

        private void ListBox_UpArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (position > 0)
                position--;

            Draw();
        }

        public override void Draw()
        {
            renderer.DrawRectangle(new Point(x, y), width, height);
            renderer.Erase(new Point(x + 1, y + 1), width - 2, height - 2);

            for (int i = 0; i < Math.Min(list.Count, Limit); i++)
            {
                if (position == i)
                    renderer.ForegroundColor = ConsoleColor.White;
                else
                    renderer.ForegroundColor = ConsoleColor.DarkGray;

                renderer.DrawText(new Point(x + 1 + Padding, y + 1 + i), list[i]);
            }

            renderer.ResetColor();
        }

        public override void Focus()
        {
            position = 0;
        }

        public void Load(IEnumerable<string> list)
        {
            this.list.Clear();
            this.list.AddRange(list);
        }
    }
}
