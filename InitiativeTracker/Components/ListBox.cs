using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Components
{
    public class ListBox : Component
    {
        private readonly Point topLeft;
        private readonly int width;
        private readonly int height;
        private readonly IEnumerable<string> list; 
        private readonly IRenderer renderer;
        private int position = 0;

        public int Padding { get; set; } = 1;
        public int Limit => height - 2;
        public string Selected
        {
            get => list.ElementAtOrDefault(position) ?? string.Empty;
            set => position = list.Any(s => s == value) ? list.ToList().IndexOf(value) : position;
        }

        public ListBox(IRenderer renderer, Point topLeft, int width, int height, IEnumerable<string> list)
        {
            this.renderer = renderer;
            this.topLeft = topLeft;
            this.width = width;
            this.height = height;
            this.list = list;

            UpArrowPressed += ListBox_UpArrowPressed;
            DownArrowPressed += ListBox_DownArrowPressed;
        }

        private void ListBox_DownArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (position < Limit - 1)
                position++;
        }

        private void ListBox_UpArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (position > 0)
                position--;
        }

        public override void Draw()
        {
            var tempList = list.ToList();

            renderer.DrawRectangle(topLeft, width, height);
            renderer.Erase(new Point(topLeft.X + 1, topLeft.Y + 1), width - 2, height - 2);

            for (int i = 0; i < Math.Min(tempList.Count(), Limit); i++)
            {
                if (position == i)
                    renderer.ForegroundColor = ConsoleColor.White;
                else
                    renderer.ForegroundColor = ConsoleColor.DarkGray;

                renderer.DrawText(new Point(topLeft.X + 1 + Padding, topLeft.Y + 1 + i), tempList[i]);
            }

            renderer.ResetColor();
        }

        public override void Focus()
        {
            position = 0;
        }
    }
}
