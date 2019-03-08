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
            if (string.IsNullOrWhiteSpace(input))
                return collection.OrderBy(s => s);

            var results = StartsWith(input).Concat(Contains(input)).Distinct();

            if (results.Count() > 0)
                results = LevenshteinOrderByDescending(results, input) ?? Enumerable.Empty<string>();

            return results.Concat(LevenshteinOrderByDescending(collection, input)).Distinct();
        }

        private IEnumerable<string> StartsWith(string input)
        {
            return collection.Where(s => s.ToLower().StartsWith(input.ToLower()));
        }

        private IEnumerable<string> Contains(string input)
        {
            return collection.Where(s => s.ToLower().Contains(input.ToLower()));
        }

        private IEnumerable<string> LevenshteinOrderByDescending(IEnumerable<string> collection, string input)
        {
            return collection.ToDictionary(s => s, s => Levenshtein.Score(input.ToLower(), s.ToLower()))
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key);
        }
    }
}
