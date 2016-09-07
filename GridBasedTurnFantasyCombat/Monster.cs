using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
    public class Monster : Creature
    {
        public string Type { get; set; }
        public Monster () : base(){ }
        public Monster(string type)
        {
            Type = type;
        }

        public void MoveAround()
        {
            // do stuff about random walking
        }
    }
}
