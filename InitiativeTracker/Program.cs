using InitiativeTracker.Helpers;
using InitiativeTracker.Rendering;
using InitiativeTracker.Views;
using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var activeView = new CreateEncounter(RenderFactory.GetRenderer(), new Guesser(names));

            activeView.Focus();
            activeView.Draw();

            while (true)
            {
                activeView.KeyPressed(Console.ReadKey(true));
                activeView.Draw();
            }
        }

        static List<string> names = new List<string>()
        {
            "JAMES",
            "JOHN",
            "ROBERT",
            "MICHAEL",
            "WILLIAM",
            "DAVID",
            "RICHARD",
            "CHARLES",
            "JOSEPH",
            "THOMAS",
            "CHRISTOPHER",
            "DANIEL",
            "PAUL",
            "MARK",
            "DONALD",
            "GEORGE",
            "KENNETH",
            "STEVEN",
            "EDWARD",
            "BRIAN",
            "RONALD",
            "ANTHONY",
            "KEVIN",
            "JASON",
            "MATTHEW",
            "GARY",
            "TIMOTHY",
            "JOSE",
            "LARRY",
            "JEFFREY",
            "FRANK",
            "SCOTT",
            "ERIC",
            "STEPHEN",
            "ANDREW",
            "RAYMOND",
            "GREGORY",
            "JOSHUA",
            "JERRY",
            "DENNIS",
            "WALTER",
            "PATRICK",
            "PETER",
            "HAROLD",
            "DOUGLAS",
            "HENRY",
            "CARL",
            "ARTHUR",
            "RYAN",
            "ROGER",
            "JOE",
            "JUAN",
            "JACK",
            "ALBERT",
            "JONATHAN",
            "JUSTIN",
            "TERRY",
            "GERALD",
            "KEITH",
            "SAMUEL",
            "WILLIE",
            "RALPH",
            "LAWRENCE",
            "NICHOLAS",
            "ROY",
            "BENJAMIN",
            "BRUCE",
            "BRANDON",
            "ADAM",
            "HARRY",
            "FRED",
            "WAYNE"
        };
    }
}
