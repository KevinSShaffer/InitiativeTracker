using InitiativeTracker.Components;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var activeComponent = new SearchBox(RenderFactory.GetRenderer(), new Point(0, 0), 30, 20, names);

            activeComponent.Focus();
            activeComponent.Draw();

            while (true)
            {
                activeComponent.KeyPressed(Console.ReadKey(true));
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
