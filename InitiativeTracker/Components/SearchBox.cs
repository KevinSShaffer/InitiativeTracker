﻿using InitiativeTracker.Helpers.Interfaces;
using InitiativeTracker.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Components
{
    public class SearchBox : Component
    {
        private readonly Point topLeft;
        private readonly int width;
        private readonly int height;
        private TextBox textBox;
        private ListBox listBox;
        private IRenderer renderer;
        private readonly IGuesser<string> guesser;

        public string Selected => listBox.Selected;

        public SearchBox(IRenderer renderer, IGuesser<string> guesser, Point topLeft, int width, int height)
        {
            this.renderer = renderer;
            this.guesser = guesser;
            this.topLeft = topLeft;
            this.width = width;
            this.height = height;

            textBox = new TextBox(renderer, new Point(topLeft.X + 2, topLeft.Y + 1), width - 4);
            listBox = new ListBox(renderer, new Point(topLeft.X + 1, topLeft.Y + 2), width - 2, height - 3, ListBoxCollection());

            CharacterKeyPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            LeftArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            RightArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            DeletePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            HomePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            EndPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            UpArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
            DownArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
            EnterPressed += (o, e) => textBox.Text = listBox.Selected;
        }

        private IEnumerable<string> ListBoxCollection()
        {
            foreach (string item in guesser.BestGuesses(textBox.Text).Take(listBox.Limit))
                yield return item;
        }

        public void Clear()
        {
            textBox.Clear();
        }

        public override void Draw()
        {
            renderer.DrawRectangle(topLeft, width, height, LineWidth.Thick);
            textBox.Draw();
            listBox.Draw();
        }

        public override void Focus()
        {
            textBox.Focus();
        }
    }
}
