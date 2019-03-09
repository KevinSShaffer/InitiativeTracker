using System;

namespace InitiativeTracker.Components
{
    public abstract class Component
    {
        public event EventHandler<KeyPressedEventArgs> CharacterKeyPressed;
        public event EventHandler<KeyPressedEventArgs> EnterPressed;
        public event EventHandler<KeyPressedEventArgs> EscapePressed;
        public event EventHandler<KeyPressedEventArgs> UpArrowPressed;
        public event EventHandler<KeyPressedEventArgs> DownArrowPressed;
        public event EventHandler<KeyPressedEventArgs> LeftArrowPressed;
        public event EventHandler<KeyPressedEventArgs> RightArrowPressed;
        public event EventHandler<KeyPressedEventArgs> BackspacePressed;
        public event EventHandler<KeyPressedEventArgs> DeletePressed;
        public event EventHandler<KeyPressedEventArgs> TabPressed;
        public event EventHandler<KeyPressedEventArgs> ShiftTabPressed;
        public event EventHandler<KeyPressedEventArgs> HomePressed;
        public event EventHandler<KeyPressedEventArgs> EndPressed;

        public void KeyPressed(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.KeyChar >= 32 && keyInfo.KeyChar <= 126)
            {
                OnCharacterKeyPressed(new KeyPressedEventArgs(keyInfo));
            }
            else
            {
                HandleFunctionKeys(keyInfo);
            }
        }

        public abstract void Draw();

        public abstract void Focus();

        public abstract void Unfocus();

        protected void HandleFunctionKeys(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    OnEnterPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.Escape:
                    OnEscapePressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.UpArrow:
                    OnUpArrowPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.DownArrow:
                    OnDownArrowPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.LeftArrow:
                    OnLeftArrowPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.RightArrow:
                    OnRightArrowPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.Backspace:
                    OnBackspacePressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.Delete:
                    OnDeletePressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.Tab:
                    if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
                        OnShiftTabPressed(new KeyPressedEventArgs(keyInfo));
                    else
                        OnTabPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.Home:
                    OnHomePressed(new KeyPressedEventArgs(keyInfo));
                    break;
                case ConsoleKey.End:
                    OnEndPressed(new KeyPressedEventArgs(keyInfo));
                    break;
                default:
                    break;
            }
        }

        protected void OnCharacterKeyPressed(KeyPressedEventArgs e)
        {
            CharacterKeyPressed?.Invoke(this, e);
        }

        protected void OnEnterPressed(KeyPressedEventArgs e)
        {
            EnterPressed?.Invoke(this, e);
        }

        protected void OnEscapePressed(KeyPressedEventArgs e)
        {
            EscapePressed?.Invoke(this, e);
        }

        protected void OnUpArrowPressed(KeyPressedEventArgs e)
        {
            UpArrowPressed?.Invoke(this, e);
        }

        protected void OnDownArrowPressed(KeyPressedEventArgs e)
        {
            DownArrowPressed?.Invoke(this, e);
        }

        protected void OnLeftArrowPressed(KeyPressedEventArgs e)
        {
            LeftArrowPressed?.Invoke(this, e);
        }

        protected void OnRightArrowPressed(KeyPressedEventArgs e)
        {
            RightArrowPressed?.Invoke(this, e);
        }

        protected void OnBackspacePressed(KeyPressedEventArgs e)
        {
            BackspacePressed?.Invoke(this, e);
        }

        protected void OnDeletePressed(KeyPressedEventArgs e)
        {
            DeletePressed?.Invoke(this, e);
        }

        protected void OnTabPressed(KeyPressedEventArgs e)
        {
            TabPressed?.Invoke(this, e);
        }

        protected void OnShiftTabPressed(KeyPressedEventArgs e)
        {
            ShiftTabPressed?.Invoke(this, e);
        }

        protected void OnHomePressed(KeyPressedEventArgs e)
        {
            HomePressed?.Invoke(this, e);
        }

        protected void OnEndPressed(KeyPressedEventArgs e)
        {
            EndPressed?.Invoke(this, e);
        }
    }

    public class KeyPressedEventArgs : EventArgs
    {
        public ConsoleKeyInfo KeyPressed { get; set; }

        public KeyPressedEventArgs(ConsoleKeyInfo keyInfo)
        {
            KeyPressed = keyInfo;
        }
    }
}
