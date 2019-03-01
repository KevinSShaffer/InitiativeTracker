using System;
using System.Text;

namespace InitiativeTracker.Components
{
    public class TextBox : Component
    {
        private readonly StringBuilder sb = new StringBuilder();
        private readonly int x;
        private readonly int y;

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
        }

        private void TextBox_EndPressed(object sender, KeyPressedEventArgs e)
        {
            Console.CursorLeft = sb.Length;
        }

        private void TextBox_HomePressed(object sender, KeyPressedEventArgs e)
        {
            Console.CursorLeft = 0;
        }

        private void TextBox_DeletePressed(object sender, KeyPressedEventArgs e)
        {
            if (sb.Length > Console.CursorLeft)
                sb.Remove(Console.CursorLeft, 1);

            Draw();
        }

        private void TextBox_BackspacePressed(object sender, KeyPressedEventArgs e)
        {
            if (sb.Length > 0)
                sb.Remove(--Console.CursorLeft, 1);

            Draw();
        }

        private void TextBox_RightArrowPressed(object sender, KeyPressedEventArgs e)
        {
            if (Console.CursorLeft < sb.Length)
                Console.CursorLeft++;
        }

        private void TextBox_LeftArrowPressed(object sender, KeyPressedEventArgs e)
        {
            Console.CursorLeft--;
        }

        private void TextBox_CharacterKeyPressed(object sender, KeyPressedEventArgs e)
        {
            sb.Insert(Console.CursorLeft, e.KeyPressed.KeyChar);
            Draw();
            Console.CursorLeft++;
        }

        public override void Draw()
        {
            int pos = Console.CursorLeft;

            Console.CursorVisible = false;
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, y);
            Console.Write(sb.ToString());
            Console.CursorLeft = pos;
            Console.CursorVisible = true;
        }

        public override void Focus()
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
