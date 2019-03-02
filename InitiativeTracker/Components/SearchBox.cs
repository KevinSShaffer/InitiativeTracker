using InitiativeTracker.Helpers;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IRenderable renderer;

        public SearchBox(Point topLeft, int width, int height, IEnumerable<string> collection)
        {
            textBox = new TextBox(new Point(topLeft.X + 2, topLeft.Y + 1), width - 4);
            listBox = new ListBox(new Point(topLeft.X + 1, topLeft.Y + 2), width - 2, height - 3);
            renderer = RenderFactory.GetRenderer();

            this.topLeft = topLeft;
            this.width = width;
            this.height = height;
            this.collection = collection;

            CharacterKeyPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            CharacterKeyPressed += (o, e) => FillListBox();
            LeftArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            RightArrowPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => FillListBox();
            DeletePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            DeletePressed += (o, e) => FillListBox();
            HomePressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            EndPressed += (o, e) => textBox.KeyPressed(e.KeyPressed);
            UpArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
            DownArrowPressed += (o, e) => listBox.KeyPressed(e.KeyPressed);
            EnterPressed += SearchBox_EnterPressed;

            listBox.Load(collection);
        }

        private void FillListBox()
        {
            listBox.Load(Searcher.Search(collection, textBox.Text).Take(listBox.Limit));
            listBox.Draw();
        }

        private void SearchBox_EnterPressed(object sender, KeyPressedEventArgs e)
        {
            throw new NotImplementedException();
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
