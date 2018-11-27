using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Territory
    {
        /// <summary>
        /// Владелец.
        /// </summary>
        public Player Owner { get; set; }
        /// <summary>
        /// Игровые очки.
        /// </summary>
        public int Point { get; set; }
        public bool Central { get; private set; }
        public int HP { get; set; }

        public Territory(Player owner,int point,bool central, int hp)
        {
            Owner = owner;
            Point = point;
            Central = central;
            HP = hp;
        }

        public Territory()
        {
        }
    }
}
