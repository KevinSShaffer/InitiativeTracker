using System;
using System.Text;

namespace InitiativeTracker
{
    public class Canvas : ICanvas
    {
        private readonly IConsole console;

        public string Header { get; set; }
        public StringBuilder CurrentLine { get; private set; } = new StringBuilder();
        public ICard Card { get; set; }
        public Func<char, ConsoleColor> InputCharacterColoration { private get; set; }
        public Func<char, char> InputCharacterAlteration { private get; set; }
        public ConsoleColor DefaultForeground { get; private set; }
        public ConsoleColor DefaultBackground { get; private set; }

        public Canvas(IConsole console, int x, int y)
        {
            this.console = console;

            this.console.SetWindowSize(x, y);
            this.console.SetBufferSize(x, y);
            DefaultForeground = this.console.ForegroundColor;
            DefaultBackground = this.console.BackgroundColor;
            InputCharacterColoration = c => DefaultForeground;
            InputCharacterAlteration = c => c;
        }

        public void Draw()
        {
            console.Clear();
            console.WriteLine(Header);

            foreach (char c in CurrentLine.ToString())
            {
                console.ForegroundColor = InputCharacterColoration(c);
                console.Write(InputCharacterAlteration(c));
            }

            // draw card
        }
    }
}
