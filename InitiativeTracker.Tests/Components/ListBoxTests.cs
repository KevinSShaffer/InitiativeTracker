using InitiativeTracker.Components;
using InitiativeTracker.Rendering;
using InitiativeTracker.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Tests.Components
{
    [TestClass]
    public class ListBoxTests
    {
        private List<string> list = new List<string>()
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
        private ListBox newListBox => new ListBox(new MockRenderer(), new Point(0, 0), 15, 15, list);
        private ConsoleKeyInfo functionKey(ConsoleKey consoleKey) => new ConsoleKeyInfo('\0', consoleKey, false, false, false);

        [TestMethod]
        public void GivenUpArrowPress_WhenPositionIsFirst_ThenReturnFirstItem()
        {
            var listBox = newListBox;

            listBox.KeyPressed(functionKey(ConsoleKey.UpArrow));

            Assert.AreEqual(list.First(), listBox.Selected);
        }

        [TestMethod]
        public void GivenDownArrowPress_WhenPositionIsFirst_ThenReturnSecondItem()
        {
            var listBox = newListBox;

            listBox.KeyPressed(functionKey(ConsoleKey.DownArrow));

            Assert.AreEqual(list.ElementAt(1), listBox.Selected);
        }

        [TestMethod]
        public void GivenUpArrowPress_WhenPositionIsThird_ThenReturnSecondItem()
        {
            var listBox = newListBox;
            var upKey = functionKey(ConsoleKey.UpArrow);
            var downKey = functionKey(ConsoleKey.DownArrow);

            listBox.KeyPressed(downKey);
            listBox.KeyPressed(downKey);
            listBox.KeyPressed(upKey);

            Assert.AreEqual(list.ElementAt(1), listBox.Selected);
        }

        [TestMethod]
        public void GivenHeightOf15_ThenReturnLimitOf13()
        {
            var listBox = newListBox;

            Assert.AreEqual(listBox.Limit, 13);
        }

        [TestMethod]
        public void GivenEmptyCollection_ThenReturnBlankSelection()
        {
            var listBox = new ListBox(new MockRenderer(), new Point(0, 0), 15, 15, Enumerable.Empty<string>());

            Assert.AreEqual(listBox.Selected, string.Empty);
        }

        [TestMethod]
        public void GivenMoreThanMaximumDownArrowPresses_ThenReturnLastItem()
        {
            var listBox = newListBox;

            for (int i = 0; i <= list.Count + 10; i++)
                listBox.KeyPressed(functionKey(ConsoleKey.DownArrow));

            Assert.AreEqual(list.Last(), listBox.Selected);
        }

        [TestMethod]
        public void GivenNewListBox_WhenFocused_ThenReturnFirstItem()
        {
            var listBox = newListBox;

            listBox.Focus();

            Assert.AreEqual(list.First(), listBox.Selected);
        }
    }
}
