using System.Collections.Generic;

namespace InitiativeTracker.Helpers.Interfaces
{
    public interface IGuesser<T>
    {
        IEnumerable<T> BestGuesses(T input);
    }
}
