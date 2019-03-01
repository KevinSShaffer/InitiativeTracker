using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InitiativeTracker.Tests
{
    [TestClass]
    public class LevenshteinTests
    {
        [TestMethod]
        public void GivenSameStrings_Return0()
        {
            string s1 = "test";
            string s2 = "test";

            int score = Levenshtein.Score(s1, s2);

            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void GivenDifferentStrings_ReturnGreaterThan0()
        {
            string s1 = "test";
            string s2 = "tester";

            int score = Levenshtein.Score(s1, s2);

            Assert.IsTrue(score > 0, $"Score == {score}");
        }

        [TestMethod]
        public void GivenTwoStrings_Return2dArray()
        {
            var array = Levenshtein.InitializeArray("test", "tester");

            Assert.AreEqual(5, array.GetLength(0));
            Assert.AreEqual(7, array.GetLength(1));
            Assert.AreEqual(1, array.GetValue(0, 1));
            Assert.AreEqual(1, array.GetValue(1, 0));
        }

        [TestMethod]
        public void GivenTwoStrings_CalculateScores_ReturnsCalculatedScores()
        {
            string s1 = "kitten", s2 = "sitting";
            var array = Levenshtein.InitializeArray(s1, s2);

            Levenshtein.CalculateScores(array, s1, s2);

            Assert.AreEqual(3, array[s1.Length, s2.Length]);
            Assert.AreEqual(2, array[s1.Length - 2, s2.Length - 2]);
            Assert.AreEqual(4, array[3, s2.Length - 1]);
        }

        [TestMethod]
        public void GivenTwoString_CalculateScore()
        {
            string s1 = "kitten", s2 = "sitting";

            int score = Levenshtein.Score(s1, s2);

            Assert.AreEqual(3, score);
        }
    }
}
