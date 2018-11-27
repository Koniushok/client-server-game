using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public class TaskAnswer : GameTask
    {
        public string[] Answers { get; set; }


        public TaskAnswer()
        {
            Answers = new string[4];
        }
        public TaskAnswer(string question, int correctAnswer,string answer1, string answer2, string answer3, string answer4)
        {
            Answers = new string[4] { answer1, answer2, answer3, answer4};
            CorrectAnswer = correctAnswer;
            Question = question;          
        }
    }
}
