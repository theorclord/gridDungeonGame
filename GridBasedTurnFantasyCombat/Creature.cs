using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedTurnFantasyCombat
{
    public abstract class Creature
    {
        public int[] Coord { get; set; }
        public int Level { get; set; }
        public int HitPoints{ get; set; }
        public int MovementSpeed { get; set; }
        //should be changed to a weapon
        public int Damage { get; set; }

        public Creature()
        {
            Level = 1;
            HitPoints = 0;
            Damage = 0;
            MovementSpeed = 0;
        }

        public void AttackCreature(Creature crea)
        {
            crea.HitPoints -= Damage;
        }

        public bool Isdead()
        {
            return HitPoints < 0;
        }
    }
}
