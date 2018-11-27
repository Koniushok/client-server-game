using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public enum GameCommandClient
    {
        ChooseCentral,
        Choose,
        Answer,
    }

    public enum GameCommanServer
    {
        Start,
        ChooseCentral,
        Choose,
        ResresultChoose,
        ResultChooseCetral,
        UpData,
        UpTerritory,
        Battle,
        SuperBattle,
        ResultBattle,
        ResultSuperBattle,
        ChoosEnemy,
        ChoosEnemyCentral,
        ResultTask,
        ResulAnsver,
        GameOver,

    }
}
