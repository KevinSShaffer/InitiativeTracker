namespace InitiativeTracker
{
    public interface IConsole
    {
        System.ConsoleColor ForegroundColor { get; set; }
        System.ConsoleColor BackgroundColor { get; set; }
        void SetWindowSize(int x, int y);
        void SetBufferSize(int x, int y);
        void Clear();
        void WriteLine(string value);
        void Write(char value);
    }
}
