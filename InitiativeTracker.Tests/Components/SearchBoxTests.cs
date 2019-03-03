using InitiativeTracker.Components;
using InitiativeTracker.Rendering;
using InitiativeTracker.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InitiativeTracker.Tests.Components
{
    [TestClass]
    public class SearchBoxTests
    {
        private SearchBox newSearchBox => new SearchBox(new MockRenderer(), new MockGuesser(), new Point(0, 0), 15, 15);
        private ConsoleKeyInfo functionKey(ConsoleKey consoleKey) => new ConsoleKeyInfo('\0', consoleKey, false, false, false);
        private ConsoleKeyInfo characterKey(char c) => new ConsoleKeyInfo(c, (ConsoleKey)c, false, false, false);

        private void writeWord(SearchBox searchBox, string s)
        {
            foreach (char c in s)
                searchBox.KeyPressed(characterKey(c));
        }

        [TestMethod]
        public void GivenConfiguredSearchBox_WhenTextIsEntered_ThenReturnBestGuess()
        {
            var searchBox = newSearchBox;

            searchBox.Focus();
            writeWord(searchBox, "foo");

            Assert.AreEqual("bar", searchBox.Selected);
        }

        [TestMethod]
        public void GivenConfiguredSearchBox_WhenArrowDown_ThenSecondSelected()
        {
            var searchBox = newSearchBox;

            searchBox.Focus();
            writeWord(searchBox, "bar");
            searchBox.KeyPressed(functionKey(ConsoleKey.DownArrow));

            Assert.AreEqual("qwer", searchBox.Selected);           
        }
    }
}
