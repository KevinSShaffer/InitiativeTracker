using InitiativeTracker.Rendering;
using System.Text;

namespace InitiativeTracker.Components
{
    public class TextBox : Component
    {
        private readonly StringBuilder sb = new StringBuilder();
        private readonly int x;
        private readonly int y;
        private readonly int width;
        private readonly IRenderable renderer;
        private int Cursor
        {
            get => renderer.CursorPosition.X - x;
            set => renderer.MoveCursor(new Point(value + x, y));
        }

        public string Text => sb.ToString();

        public TextBox(Point point, int width)
        {
            x = point.X;
            y = point.Y;
            this.width = width;

            CharacterKeyPressed += TextBox_CharacterKeyPressed;
            LeftArrowPressed += TextBox_LeftArrowPressed;
            RightArrowPressed += TextBox_RightArrowPressed;
            BackspacePressed += TextBox_BackspacePressed;
            DeletePressed += TextBox_DeletePressed;
            HomePressed += TextBox_HomePressed;
            EndPressed += TextBox_EndPressed;

            renderer = RenderFactory.GetRenderer();
        }

        private void TextBox_EndPressed(object sender, KeyPressedEventArgs e)
        {
            Cursor = sb.Length;
        }

        private void TextBox_HomePressed(object sender, KeyPressedEventArgs e)
        {
            Cursor = 0;
        }

        private void TextBox_DeletePressed(object sender, KeyPressedEventArgs e)
        {
            if (sb.Length > Cursor)
                sb.Remove(Cursor, 1);

            Draw();
        }

        private void TextBox_BackspacePressed(object sender, KeyPressedEventArgs e)
        {
            if (sb.Length > 0)
                sb.Remove(--Cursor, 1);

            Draw();
        }

        private void TextBox_RightArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (Cursor < sb.Length)
                Cursor++;
        }

        private void TextBox_LeftArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (Cursor > 0)
                Cursor--;
        }

        private void TextBox_CharacterKeyPressed(object sender, KeyPressedEventArgs e)
        {
            sb.Insert(Cursor, e.KeyPressed.KeyChar);
            Draw();
            Cursor++;
        }

        public override void Draw()
        {
            renderer.Erase(new Point(x, y), width, 1);
            renderer.DrawText(new Point(x, y), sb.ToString());
        }

        public override void Focus()
        {
            Cursor = 0;
        }
    }
}
