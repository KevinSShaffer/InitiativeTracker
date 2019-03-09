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
        private int firstElementPosition = 0;
        private bool focused = false;

        public int Padding { get; set; } = 1;
        public int Limit => height - 2;
        public ConsoleColor selectedTextColor => focused ? ConsoleColor.White : ConsoleColor.Gray;
        public ConsoleColor BoxColor => focused ? ConsoleColor.White : ConsoleColor.DarkGray;
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
            if (position < list.Count() - 1)
                position++;

            if (firstElementPosition + Limit <= position)
                firstElementPosition = position - Limit + 1;
        }

        private void ListBox_UpArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (position > 0)
                position--;

            if (position < firstElementPosition)
                firstElementPosition = position;
        }

        public override void Draw()
        {
            var tempList = list.Skip(firstElementPosition).ToList();

            DrawBox();

            for (int i = 0; i < Math.Min(tempList.Count, Limit); i++)
            {
                string text = tempList[i].Substring(0, Math.Min(width - 3, tempList[i].Length));

                ConsoleColor textColor = position == i + firstElementPosition ? selectedTextColor : ConsoleColor.DarkGray;

                renderer.With(textColor).DrawText(new Point(topLeft.X + 1 + Padding, topLeft.Y + 1 + i), text);
            }
        }

        private void DrawBox()
        {
            renderer.With(BoxColor).DrawRectangle(topLeft, width, height);
            renderer.Erase(new Point(topLeft.X + 1, topLeft.Y + 1), width - 2, height - 2);

            if (firstElementPosition > 0)
                renderer.With(BoxColor).DrawText(new Point(topLeft.X + width - 1, topLeft.Y + 1), '^'.ToString());

            if (list.Count() - firstElementPosition > Limit)
                renderer.With(BoxColor).DrawText(new Point(topLeft.X + width - 1, topLeft.Y + height - 2), 'v'.ToString());
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
