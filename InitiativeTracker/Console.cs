namespace InitiativeTracker
{
    public class Console : IConsole
    {
        public System.ConsoleColor ForegroundColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public System.ConsoleColor BackgroundColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public void SetBufferSize(int x, int y)
        {
            System.Console.SetBufferSize(x, y);
        }

        public void SetWindowSize(int x, int y)
        {
            System.Console.SetWindowSize(x, y);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        public void Write(char value)
        {
            System.Console.Write(value);
        }
    }
}
