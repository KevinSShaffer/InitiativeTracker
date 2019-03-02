using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Helpers
{
    public static class Searcher
    {
        public static IEnumerable<string> Search(IEnumerable<string> collection, string input)
        {
            var startsWith = StartsWithFirstThreeCharacters(collection, input);

            if (startsWith.Count() > 0)
                return ClosestMatches(startsWith, input) ?? Enumerable.Empty<string>();
            else
                return ClosestMatches(collection, input);
        }

        static IEnumerable<string> StartsWithFirstThreeCharacters(IEnumerable<string> collection, string input)
        {
            return collection.Where(s => s.ToLower().StartsWith(input.Substring(0, Math.Min(input.Length, 3)).ToLower()));
        }

        static IEnumerable<string> ClosestMatches(IEnumerable<string> collection, string input)
        {
            return collection.ToDictionary(s => s, s => Levenshtein.Score(input.ToLower(), s.ToLower()))
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key);
        }
    }
}
