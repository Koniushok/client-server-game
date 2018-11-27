using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Maps
    {
        public Territory[] Territories { get; set; }
        public bool[,] ContactTerritory { get; set; }

        public Maps()
        {
            int k = 20;       
            Territories = new Territory[k];
            ContactTerritory = new bool[k,k];
            for (int i = 0; i < Territories.Length; i++)
            {
                Territories[i] = new Territory();
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    ContactTerritory[i,j] = false;
                }
            }

            ContactTerritory[0,1]= true;

            ContactTerritory[1,0]= true;
            ContactTerritory[1,2]= true;
            ContactTerritory[1,3]= true;

            ContactTerritory[2,1]= true;
            ContactTerritory[2,3]= true;
            ContactTerritory[2,4]= true;
            ContactTerritory[2,5]= true;

            ContactTerritory[3,1]= true;
            ContactTerritory[3,2]= true;
            ContactTerritory[3,5]= true;
            ContactTerritory[3,6]= true;

            ContactTerritory[4,2]= true;
            ContactTerritory[4,5]= true;

            ContactTerritory[5,2]= true;
            ContactTerritory[5,3]= true;
            ContactTerritory[5,4]= true;
            ContactTerritory[5,6]= true;
            ContactTerritory[5,7]= true;
            ContactTerritory[5,8]= true;

            ContactTerritory[6,3]= true;
            ContactTerritory[6,5]= true;
            ContactTerritory[6,8]= true;
            ContactTerritory[6,9]= true;
            ContactTerritory[6,11]= true;
            ContactTerritory[6,12]= true;

            ContactTerritory[7,5]= true;
            ContactTerritory[7,8]= true;
            ContactTerritory[7,10]= true;

            ContactTerritory[8,5]= true;
            ContactTerritory[8,6]= true;
            ContactTerritory[8,7]= true;
            ContactTerritory[8,10]= true;
            ContactTerritory[8,11]= true;


            ContactTerritory[9,6]= true;
            ContactTerritory[9,12]= true;

            ContactTerritory[10,7]= true;
            ContactTerritory[10,8]= true;
            ContactTerritory[10,11]= true;
            ContactTerritory[10,12]= true;

            ContactTerritory[11,6]= true;
            ContactTerritory[11,8]= true;
            ContactTerritory[11,10]= true;
            ContactTerritory[11,12]= true;
            ContactTerritory[11,13]= true;
            ContactTerritory[11,14]= true;
            ContactTerritory[11,16]= true;

            ContactTerritory[12,6]= true;
            ContactTerritory[12,9]= true;
            ContactTerritory[12,11]= true;
            ContactTerritory[12,16]= true;
            ContactTerritory[12,17]= true;

            ContactTerritory[13,10]= true;
            ContactTerritory[13,11]= true;
            ContactTerritory[13,14]= true;

            ContactTerritory[14,11]= true;
            ContactTerritory[14,13]= true;
            ContactTerritory[14,15]= true;
            ContactTerritory[14,16]= true;

            ContactTerritory[15,14]= true;
            ContactTerritory[15,16]= true;
            ContactTerritory[15,18]= true;

            ContactTerritory[16,11]= true;
            ContactTerritory[16,12]= true;
            ContactTerritory[16,14]= true;
            ContactTerritory[16,15]= true;
            ContactTerritory[16,17]= true;
            ContactTerritory[16,18]= true;
            ContactTerritory[16,19]= true;

            ContactTerritory[17,12]= true;
            ContactTerritory[17,16]= true;

            ContactTerritory[18,15]= true;
            ContactTerritory[18,16]= true;
            ContactTerritory[18,19]= true;

            ContactTerritory[19,16]= true;
            ContactTerritory[19,18]= true;
           
        }
    }
}
