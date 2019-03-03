using System.Collections.Generic;

namespace InitiativeTracker.Controllers.Interfaces
{
    public interface IController<T, IdT>
    {
        T Get(IdT id);
        IEnumerable<T> Get();
        void Post(T monster);
        void Put(T monster);
        void Patch(T monster);
        void Delete(IdT id);
    }
}
