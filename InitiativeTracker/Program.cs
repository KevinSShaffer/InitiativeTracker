using InitiativeTracker.Components;
using System;

namespace InitiativeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var activeComponent = new TextBox(0, 1);

            activeComponent.Focus();

            while (true)
            {
                activeComponent.KeyPressed(Console.ReadKey(true));
            }
        }
    }
}
