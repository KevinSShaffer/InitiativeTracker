using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Models
{
    public class MonsterGroup
    {
        public Monster Monster { get; set; }
        public int Quantity { get; set; }

        public MonsterGroup(Monster monster, int quantity)
        {
            Monster = monster;
            Quantity = quantity;
        }
    }
}
