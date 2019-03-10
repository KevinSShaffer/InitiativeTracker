using InitiativeTracker.Helpers.Interfaces;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;

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
        private bool focused = false;

        public string Text { get => textBox.Text; set => textBox.Text = value; }
        public string Selected { get => listBox.Selected; set => listBox.Selected = value; }
        public ConsoleColor BoxColor => focused ? ConsoleColor.White : ConsoleColor.DarkGray;

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
            EnterPressed += SearchBox_EnterPressed;
        }

        private void SearchBox_EnterPressed(object sender, KeyPressedEventArgs e)
        {
            textBox.Text = listBox.Selected;
            listBox.Selected = textBox.Text;
        }

        private IEnumerable<string> ListBoxCollection()
        {
            foreach (string item in guesser.BestGuesses(textBox.Text))
                yield return item;
        }

        public void Clear()
        {
            textBox.Clear();
        }

        public override void Draw()
        {
            renderer.With(BoxColor).DrawRectangle(topLeft, width, height, LineWidth.Thick);
            textBox.Draw();
            listBox.Draw();
        }

        public override void Focus()
        {
            focused = true;
            textBox.Focus();
            listBox.Focus();
        }

        public override void Unfocus()
        {
            focused = false;
            textBox.Unfocus();
            listBox.Unfocus();
        }
    }
}
