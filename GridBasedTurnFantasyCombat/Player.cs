using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
    public class Player : Creature
    {
        public string Name { get; set; }
        public Player()
        {

        }
        public Player(string name)
        {
            Name = name;
        }
    }
}
