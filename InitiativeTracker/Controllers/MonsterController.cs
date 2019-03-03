using System.Collections.Generic;
using InitiativeTracker.Controllers.Interfaces;
using InitiativeTracker.Models;

namespace InitiativeTracker.Controllers
{
    public class MonsterController : IController<Monster, string>
    {
        public void Delete(string name)
        {
            throw new System.NotImplementedException();
        }

        public Monster Get(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Monster> Get()
        {
            throw new System.NotImplementedException();
        }

        public void Patch(Monster monster)
        {
            throw new System.NotImplementedException();
        }

        public void Post(Monster monster)
        {
            throw new System.NotImplementedException();
        }

        public void Put(Monster monster)
        {
            throw new System.NotImplementedException();
        }
    }
}
