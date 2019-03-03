using InitiativeTracker.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Tests.Mocks
{
    public class MockGuesser : IGuesser<string>
    {
        List<string> list = new List<string>()
        {
            "asdf",
            "qwer",
            "zxcv"
        };

        public IEnumerable<string> BestGuesses(string input)
        {
            if (input == "foo")
                yield return "bar";
            else
                foreach (string item in list.OrderBy(s => s))
                    yield return item;
        }
    }
}
