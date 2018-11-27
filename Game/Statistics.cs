using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public class Statistics
    {
        public int NumGame { get; set; }
        public int WinGame { get; set; }
        public int LeaveGame { get; set; }
        public int NumAnswers { get; set; }
        public int CorrectAnswers { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Login { get; set; }

        public  Statistics(int numGame, int winGame, int leaveGame, int numAnswers, int correctAnswers)
        {
            NumGame = numGame;
            WinGame = winGame;
            LeaveGame = leaveGame;
            NumAnswers = numAnswers;
            CorrectAnswers = correctAnswers;
        }
        public Statistics(int numGame, int winGame, int leaveGame, int numAnswers, int correctAnswers,string Name,string SurName,string Login)
        {
            NumGame = numGame;
            WinGame = winGame;
            LeaveGame = leaveGame;
            NumAnswers = numAnswers;
            CorrectAnswers = correctAnswers;

            this.Name = Name;
            this.SurName = SurName;
            this.Login = Login;
        }
    }
}
