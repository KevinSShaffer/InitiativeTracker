using InitiativeTracker.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Helpers
{
    public class Guesser : IGuesser<string>
    {
        private readonly IEnumerable<string> collection;

        public Guesser(IEnumerable<string> collection)
        {
            this.collection = collection;
        }

        public IEnumerable<string> BestGuesses(string input)
        {
            var startsWith = StartsWith(input);

            if (startsWith.Count() > 0)
                startsWith = LevenshteinOrderByDescending(startsWith, input) ?? Enumerable.Empty<string>();

            return startsWith.Concat(LevenshteinOrderByDescending(collection, input)).Distinct();
        }

        private IEnumerable<string> StartsWith(string input)
        {
            return collection.Where(s => s.ToLower().StartsWith(input.ToLower()));
        }

        private IEnumerable<string> LevenshteinOrderByDescending(IEnumerable<string> collection, string input)
        {
            return collection.ToDictionary(s => s, s => Levenshtein.Score(input.ToLower(), s.ToLower()))
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key);
        }
    }
}
