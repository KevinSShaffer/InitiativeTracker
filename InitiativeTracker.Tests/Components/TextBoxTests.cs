using InitiativeTracker.Components;
using InitiativeTracker.Rendering;
using InitiativeTracker.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InitiativeTracker.Tests.Components
{
    [TestClass]
    public class TextBoxTests
    {
        private TextBox newTextBox => new TextBox(new MockRenderer(), new Point(0, 0), 10);
        private ConsoleKeyInfo functionKey(ConsoleKey consoleKey) => new ConsoleKeyInfo('\0', consoleKey, false, false, false);
        private ConsoleKeyInfo characterKey(char c) => new ConsoleKeyInfo(c, (ConsoleKey)c, false, false, false);

        private void writeWord(TextBox textBox, string s)
        {
            foreach (char c in s)
                textBox.KeyPressed(characterKey(c));
        }

        [TestMethod]
        public void GivenMultipleCharactersPressed_ThenReturnWord()
        {
            var textBox = newTextBox;

            writeWord(textBox, "foo");

            Assert.AreEqual("foo", textBox.Text);
        }

        [TestMethod]
        public void GivenArrowLeft_WhenTextIsNotBlank_ThenInsertCharacters()
        {
            var textBox = newTextBox;

            writeWord(textBox, "foobr");
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(characterKey('a'));

            Assert.AreEqual("foobar", textBox.Text);            
        }

        [TestMethod]
        public void GivenArrowLeft_WhenCursorAtFirstPosition_ThenCursorStaysAtFirstPosition()
        {
            var textBox = newTextBox;
            string word = "bar";

            writeWord(textBox, word);

            for (int i = 0; i <= word.Length; i++)
                textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));

            writeWord(textBox, "foo");

            Assert.AreEqual("foobar", textBox.Text);
        }

        [TestMethod]
        public void GivenArrowLeft_AndArrowRight_WhenTextIsNotBlank_ThenInsertCharacters()
        {
            var textBox = newTextBox;

            writeWord(textBox, "foobr");
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.RightArrow));
            textBox.KeyPressed(characterKey('a'));

            Assert.AreEqual("foobar", textBox.Text);
        }

        [TestMethod]
        public void GivenArrowRight_WhenCursorAtLastPosition_ThenCursorStaysAtLastPosition()
        {
            var textBox = newTextBox;
            string word = "foo";

            writeWord(textBox, word);

            for (int i = 0; i <= word.Length; i++)
                textBox.KeyPressed(functionKey(ConsoleKey.RightArrow));

            writeWord(textBox, "bar");

            Assert.AreEqual("foobar", textBox.Text);
        }

        [TestMethod]
        public void GivenBackspace_WhenCursorAtEndOfWord_ThenRemoveLastLetter()
        {
            var textBox = newTextBox;

            writeWord(textBox, "foob");

            textBox.KeyPressed(functionKey(ConsoleKey.Backspace));

            Assert.AreEqual("foo", textBox.Text);
        }

        [TestMethod]
        public void GivenDelete_WhenCursorInMiddleOfWord_ThenRemoveMiddleCharacter()
        {
            var textBox = newTextBox;

            writeWord(textBox, "fooxbar");

            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.LeftArrow));
            textBox.KeyPressed(functionKey(ConsoleKey.Delete));

            Assert.AreEqual("foobar", textBox.Text);
        }

        [TestMethod]
        public void GivenHome_ThenSetCursorAtBeginningOfWord()
        {
            var textBox = newTextBox;

            writeWord(textBox, "bar");

            textBox.KeyPressed(functionKey(ConsoleKey.Home));

            writeWord(textBox, "foo");

            Assert.AreEqual("foobar", textBox.Text);
        }

        [TestMethod]
        public void GivenEnd_ThenSetCursorAtEndOfWord()
        {
            var textBox = newTextBox;

            writeWord(textBox, "foo");

            textBox.KeyPressed(functionKey(ConsoleKey.Home));
            textBox.KeyPressed(functionKey(ConsoleKey.End));

            writeWord(textBox, "bar");

            Assert.AreEqual("foobar", textBox.Text);
        }
    }
}
