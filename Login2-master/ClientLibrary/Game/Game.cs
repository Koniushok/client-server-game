using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games;
using System.Windows.Threading;

namespace Client
{
    public class Game
    {
        #region Event

        public event Action StartEvent;

        public event Action<bool[], int> ChooseCentralEvent;

        public event Action<int> ResresultChooseCetralEvent;

        public event Action UpDataEvent;

        public event Action<bool[], int> ChooseEvent;

        public event Action<int> ResresultChooseEvent;

        public event Action<int> ChoosEnemyCentralEvent;

        public event Action<int> ChoosEnemyEvent;

        public event Action<GameTask, int, int> BattleEvent;

        public event Action<GameTask, int, int> SuperBattleEvent;


        public event Action DisconectEvent;

        public event Action<int, int,int> ResultTaskEvent;

        public event Action<int,string,string> ResulAnsverEvent;


        public event Action<bool,int , string , int , string , int , int,string > GameOverEvent;

        #endregion

        #region свойства
        public Player MyPlayer { get; set; }

        public Player Enemy { get; set; }

        public Maps Map { get; set; }

        public GameTask Task { get; set; }

        public TimeSpan MaxTime { get; set; }

        bool Status { get; set; }

        #endregion


        public void Start(string enemyLogin)
        {
            MyPlayer = new Player(MainClient.Profile.Login);
            Enemy = new Player(enemyLogin);
            Map = new Maps();

            if (StartEvent != null)
            {
                StartEvent.Invoke();
            }
        }

        public void ChooseCentral(bool[] Territory, int maxtime)
        {
            if (ChooseCentralEvent != null)
            {
                ChooseCentralEvent.Invoke(Territory, maxtime);
            }
        }

        public void Choose(bool[] Territory, int Maxtime)
        {
            if (ChooseEvent != null)
                ChooseEvent.Invoke(Territory, Maxtime);
        }

        public void ResresultChoose(string login, int attackTerritory, bool assign)
        {

            if (ResresultChooseEvent != null)
                ResresultChooseEvent.Invoke(attackTerritory);
        }

        public void ResresultChooseCetral(string login, int Territory)
        {
            ResresultChooseCetralEvent.Invoke(Territory);
        }

        public void UpData(Territory[] territory, Player enemy, Player myPlayer)
        {
            Map.Territories = territory;
            Enemy = enemy;
            MyPlayer = myPlayer;
            if (UpDataEvent != null)
                UpDataEvent.Invoke();
        }

        public void UpTerritory(Territory[] territory)
        {

        }

        public void Battle(GameTask task, int AttackTerritory, int Maxtime)
        {
            if (BattleEvent != null)
            {
                BattleEvent.Invoke(task, AttackTerritory, Maxtime);
            }
        }

        public void SuperBattle(GameTask task, int AttackTerritory, int Maxtime)
        {
            if (SuperBattleEvent != null)
            {
                SuperBattleEvent.Invoke(task, AttackTerritory, Maxtime);
            }
        }

        public void ResultBattle(string loginWinner, string loginLoser, int pointWinner, int pointLoser, int answerWinner, int answerLoser, GameTask task)
        {

        }

        public void ResultSuperBattle(string loginWinner, string loginLoser, int pointWinner, int pointLoser, int answerWinner, int answerLoser, GameTask tas)
        {


        }

        public void ChoosEnemyCentral(int time)
        {
            if (ChoosEnemyCentralEvent != null)
            {
                ChoosEnemyCentralEvent.Invoke(time);
            }
        }

        public void ChoosEnemy(int time)
        {
            if (ChoosEnemyEvent != null)
            {
                ChoosEnemyEvent.Invoke(time);
            }
        }

        public void ResultTask(int my, int enemy,int numTask)
        {
            if (ResultTaskEvent != null)
            {
                ResultTaskEvent.Invoke(my, enemy,numTask);
            }
        }

        public void ResulAnsver(int ansver,string myTime,string enemyTime)
        {
            if (ResulAnsverEvent != null)
            {
                ResulAnsverEvent.Invoke(ansver,myTime,enemyTime);
            }
        }

        public void GameOver(bool win, int myCorrectAnswer, string myTimeAnsver, int enemyCorrectAnswer, string enemyTimeAnsver, int numNumStep, int NumAnswer, string timeGame)
        {
            if(GameOverEvent!=null)
            {
                GameOverEvent.Invoke(win,myCorrectAnswer,myTimeAnsver,enemyCorrectAnswer,enemyTimeAnsver,numNumStep,NumAnswer,timeGame);
            }
        }

        public void Disconect()
        {
            if (DisconectEvent != null)
            {
                DisconectEvent.Invoke();
            }
        }



    }
}
