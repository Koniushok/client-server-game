using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    public enum TypeTask
    {
        Numeric,
        Answer,
    }

    public abstract class GameTask
    {
        public string Question { get; set; }
        public int CorrectAnswer { get; set; }      
    }
}
