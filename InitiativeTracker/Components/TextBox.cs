using InitiativeTracker.Rendering;
using System.Text;

namespace InitiativeTracker.Components
{
    public class TextBox : Component
    {
        private readonly StringBuilder sb = new StringBuilder();
        private readonly int x;
        private readonly int y;
        private readonly IRenderable renderer;
        private int Cursor
        {
            get => renderer.CursorPosition.X;
            set => renderer.MoveCursor(new Point(value, y));
        }

        public TextBox(int x, int y)
        {
            this.x = x;
            this.y = y;

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
            if (renderer.CursorPosition.X < sb.Length)
                renderer.MoveCursor(new Point(renderer.CursorPosition.X + 1, y));
        }

        private void TextBox_LeftArrowPressed(object sender, KeyPressedEventArgs e)
        {
            renderer.MoveCursor(new Point(renderer.CursorPosition.X - 1, y));
        }

        private void TextBox_CharacterKeyPressed(object sender, KeyPressedEventArgs e)
        {
            sb.Insert(Cursor, e.KeyPressed.KeyChar);
            Draw();
            Cursor++;
        }

        public override void Draw()
        {
            renderer.Erase(new Point(x, y), renderer.CanvasWidth, 1);
            renderer.DrawText(new Point(x, y), sb.ToString());
        }

        public override void Focus()
        {
            Cursor = x;
        }
    }
}
