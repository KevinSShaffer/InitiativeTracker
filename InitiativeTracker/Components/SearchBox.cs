using InitiativeTracker.Controllers.Interfaces;
using InitiativeTracker.Helpers;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Components
{
    public class SearchBox : Component
    {
        private readonly Point topLeft;
        private readonly int width;
        private readonly int height;
        private readonly IEnumerable<string> collection;
        private TextBox textBox;
        private ListBox listBox;
        private IRenderer renderer;

        public string Selected => listBox.Selected;

        public SearchBox(IRenderer renderer, Point topLeft, int width, int height, IEnumerable<string> collection)
        {
            this.renderer = renderer;
            this.topLeft = topLeft;
            this.width = width;
            this.height = height;
            this.collection = collection;

            textBox = new TextBox(renderer, new Point(topLeft.X + 2, topLeft.Y + 1), width - 4);
            listBox = new ListBox(renderer, new Point(topLeft.X + 1, topLeft.Y + 2), width - 2, height - 3, ListBoxCollection());

            CharacterKeyPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            CharacterKeyPressed += (o, e) => listBox.Draw();
            LeftArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            RightArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => listBox.Draw();
            DeletePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            DeletePressed += (o, e) => listBox.Draw();
            HomePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            EndPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            UpArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
            DownArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
        }

        private IEnumerable<string> ListBoxCollection()
        {
            foreach (string item in Searcher.Search(collection, textBox.Text).Take(listBox.Limit))
                yield return item;
        }

        public override void Draw()
        {
            renderer.DrawRectangle(topLeft, width, height);
            textBox.Draw();
            listBox.Draw();
        }

        public override void Focus()
        {
            textBox.Focus();
        }
    }
}
