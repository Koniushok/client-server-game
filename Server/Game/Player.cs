using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Player
    {
        public int CorrectAnswer { get; set; } = 0;

        public TimeSpan TimeAnverAll { get; set; } = new TimeSpan(0, 0, 0);
        /// <summary>
        /// Очков в игре.
        /// </summary>
        public int Point { get; set; } = 0;
        /// <summary>
        /// Номер ответа на текущий вопрос.
        /// </summary>
        public int Answer { get; set; } = -1;
        /// <summary>
        /// Время затраченное на ответ.
        /// </summary>
        public TimeSpan TimeAnswer { get; set; } = new TimeSpan(0, 0, 0);

        public int CentralTerritory { get; set; } = -1;

        public void Reset()
        {
            Answer = -1;
            TimeAnswer= new TimeSpan(0, 0, 0);
        }       
    }
}
