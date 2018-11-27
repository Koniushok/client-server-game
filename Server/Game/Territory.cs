using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Territory
    {
        public ClientGame Owner { get; set; }
        public int Point { get; set; }
        public bool Central { get; private set; }
        public int HP { get; set; }

        public Territory(ClientGame clientGame,bool central)
        {
            Owner = clientGame;
            Central = central;
            Point = 300;
            if(central)
            {
                Point = 3000;
                HP = 3000;
            }

            clientGame.Player.Point += Point;

        }

        public Territory()
        {
            Point = 100;
        }

       

    }
}
