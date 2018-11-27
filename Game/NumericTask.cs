using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public class NumericTask:GameTask
    {
        public NumericTask(string question, int correctAnswer)
        {
            CorrectAnswer = correctAnswer;
            Question = question;
        }

        public NumericTask()
        {
           
        }
    }
}
