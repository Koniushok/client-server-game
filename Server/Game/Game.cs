using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Games;
using System.Timers;

namespace Server
{
    public class Game
    {
        #region Свойства
        GameServer Server { get; set; }

        public bool Status { get; set; }

        public ClientGame Player1 { get; set; }

        public ClientGame Player2 { get; set; }

        Maps Map { get; set; }

        int MaxTimeTask { get; set; } = 10000;

        int MaxTimeСhoice { get; set; } = 5000;

        int MaxTimeСhoiceCentral { get; set; } = 5000;

        TimeSpan BeginTimeTask { get; set; }


        public System.Timers.Timer StopTimeTask = new System.Timers.Timer();

        public System.Timers.Timer StopTimeChoice = new System.Timers.Timer();

        public System.Timers.Timer StopTimeChoiceCentral = new System.Timers.Timer();

        

        #region Меняющиеся свойства
        ClientGame PlayerStep { get; set; }
        bool StatusQuestion { get; set; }
        GameTask Task { get; set; }
        int AttackTerritory { get; set; }
        int WinPoint { get; set; } = 200;
        int TerPoint { get; set; } = 400;
        int CentalLossPoint { get; set; } = 800;

        List<int> TaskArray { get; set; } = new List<int>();

        public int idTask;
        #endregion

        #region Статистика

        int NumStep { get; set; } = 0;

        public TimeSpan StartTime { get; set; }

        int NumAnswer { get; set; } = 0;

        #endregion

        #endregion

        public Game(GameServer server)
        {
            Server = server;
            Map = new Maps();
            StopTimeTask.Elapsed += StopTask;
            StopTimeTask.Interval = MaxTimeTask;

            StopTimeChoice.Elapsed += StopChoice;
            StopTimeChoice.Interval = MaxTimeСhoice;

            StopTimeChoiceCentral.Elapsed += StopChoiceCentral;
            StopTimeChoiceCentral.Interval = MaxTimeСhoiceCentral;

           
        }

        ClientGame OppositePlayer(ClientGame player)
        {
            ClientGame Opposite = null;
            if (player == Player1)
            {
                Opposite = Player2;
            }
            else if (player == Player2)
            {
                Opposite = Player1;
            }
            return Opposite;
        }

        

        #region Question


        /// <summary>
        /// Задаёт новый вопрос.
        /// </summary>
        /// <param name="task">Вопрос</param>
        void NewQuestion(GameTask task)
        {

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('?', 50));
                Console.WriteLine("GameEvent:");
                Console.WriteLine("Port:{0}", Server.Port);
                Console.WriteLine("Новый вопрос");
                Console.WriteLine("Вопрос:{0}",task.Question);
                Console.WriteLine("Правильный ответ:{0}", task.CorrectAnswer+1);
                Console.WriteLine(new string('?', 50));
                Console.ForegroundColor = color;
            Task = task;
        }

        /// <summary>
        /// Запуск задания.
        /// </summary>
        /// <param name="task"></param>
        void StartQuestion(GameTask task)
        {
            NumAnswer++;
            OccasionHandler.Game(Server, "StartQuestion");
            NewQuestion(task);
            BeginTimeTask = DateTime.Now.TimeOfDay;
            StatusQuestion = true;
            
            StopTimeTask.Start();
        }

        /// <summary>
        /// Остановка задания.
        /// </summary>
        void StopQuestion()
        {
            OccasionHandler.Game(Server, "StartQuestion");
            StatusQuestion = false;
        }

        /// <summary>
        /// Принятия ответа от игроков.
        /// </summary>
        /// <param name="clientGame">Игрок</param>
        /// <param name="answerNumber">Номер ответа</param>
        public void PlayerAnswer(ClientGame clientGame, int answerNumber)
        {
            if (StatusQuestion)
            {
                OccasionHandler.Game(Server, "Принятия ответа от игрока");
                TimeSpan time = DateTime.Now.TimeOfDay - BeginTimeTask;
                clientGame.Player.Answer = answerNumber;
                clientGame.Player.TimeAnswer = time;
                if (Player1.Player.Answer != -1 && Player2.Player.Answer != -1)
                {
                    ResultTask();
                }
            }

        }

        GameTask RandomTask(TypeTask task)
        {
            Random ran = new Random();
            
            int num = SQL.NumberOfTask(task);

            bool test = true;
            int k=-1;
            while (test)
            {
                test = false;
                k = ran.Next(1, num + 1);
                foreach (int item in TaskArray)
                {
                    if(item==k)
                    {
                        test = true;
                        break;
                    }
                }
            }
            TaskArray.Add(k);

            return SQL.GetTask(k, task,this);
        }
        #endregion

        #region Territory
        /// <summary>
        /// Игрок передал выбранную центральную территорию.Если территория доступна выбор завершается.
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="number">НОмер выбранной территории</param>
        /// <returns></returns>
        public bool PlayerSetCentral(ClientGame player, int number)
        {
            if (StopTimeChoiceCentral.Enabled)
            {
                OccasionHandler.Game(Server, "Игрок передал выбранную центральную территорию");
                if (Map.TestChooseCentral(number))
                {
                    SetCentralTerritory(player, number);
                    ResultChooseCentral();
                    return true;
                }
                
            }
            return false;
        }

        /// <summary>
        /// Игрок передал выбранную территорию.Если территория доступна выбор завершается.
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="number">Номер выбранной территории</param>
        /// <returns></returns>
        public bool PlayerSetTerritory(ClientGame player, int number)
        {
            if (StopTimeChoice.Enabled)
            {
                OccasionHandler.Game(Server, "Игрок передал выбранную территорию");
                if (Map.TestChoose(player, number))
                {
                    //SetTerritory(player, number);
                    AttackTerritory = number;
                    ResultChooseTerritory();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Присвоение центральной территории игроку.
        /// </summary>
        /// <param name="clientGame">Игрок.</param>
        /// <param name="number">Номер территории.</param>
        void SetCentralTerritory(ClientGame clientGame, int number)
        {
            if (Map.Territories[number].Owner != clientGame && Map.Territories[number].Owner != null)
            {
                Map.Territories[number].Owner.Player.Point -= Map.Territories[number].Point;
            }

            OccasionHandler.Game(Server, "Присвоение центральной территории игроку");
            Territory t = new Territory(clientGame, true);
            Map.Territories[number] = t;
            clientGame.Player.CentralTerritory = number;
        }

        /// <summary>
        /// Присвоение территории игроку.
        /// </summary>
        /// <param name="clientGame">Игрок</param>
        /// <param name="number">Номер территории</param>
        void SetTerritory(ClientGame clientGame, int number)
        {
            clientGame.Player.Point += Map.Territories[number].Point;
            if (Map.Territories[number].Owner != null)
            {
                Map.Territories[number].Owner.Player.Point -= Map.Territories[number].Point;
            }
            else
            {
                Map.Territories[number].Point = TerPoint;
            }

            OccasionHandler.Game(Server, "Присвоение территории игроку.");
            Map.Territories[number].Owner = clientGame;
        }

        /// <summary>
        /// Результат выбранной территории.
        /// </summary>
        void ResultChooseTerritory()
        {
            OccasionHandler.Game(Server, "Результат выбранной территории.");
            StopTimeChoice.Stop();
            if (AttackTerritory == -1)
            {
                AttackTerritory = Map.RandomTerritories(PlayerStep);
            }



            if (Map.TestChoose(PlayerStep, AttackTerritory))
            {
                if (Map.Territories[AttackTerritory].Owner == null)
                {
                    Player1.ResresultChoose(PlayerStep, AttackTerritory, true);
                    Player2.ResresultChoose(PlayerStep, AttackTerritory, true);
                    Thread.Sleep(2500);
                    SetTerritory(PlayerStep, AttackTerritory);
                    NextStep();
                    return;
                }
                else
                if (Map.Territories[AttackTerritory].Owner != PlayerStep)
                {
                    Player1.ResresultChoose(PlayerStep, AttackTerritory, false);
                    Player2.ResresultChoose(PlayerStep, AttackTerritory, false);
                    Thread.Sleep(2500);
                    StartBattle();
                }


            }
        }

        /// <summary>
        /// Результат выбранной центральной территории.
        /// </summary>
        void ResultChooseCentral()
        {
            OccasionHandler.Game(Server, "Результат выбранной центральной территории.");
            StopTimeChoiceCentral.Stop();
            if (PlayerStep.Player.CentralTerritory == -1)
            {
                SetCentralTerritory(PlayerStep, Map.RandomCentral(PlayerStep));
            }
            Player1.ResresultChooseCetral(PlayerStep, PlayerStep.Player.CentralTerritory);
            Player2.ResresultChooseCetral(PlayerStep, PlayerStep.Player.CentralTerritory);
            Thread.Sleep(3000);
            NextStep();
        }


        /// <summary>
        /// Выбираем территорию.
        /// </summary>
        void Choose()
        {
            OccasionHandler.Game(Server, "Выбор территории.");
            PlayerStep.Choose(Map.AccessibleTerritories(PlayerStep), MaxTimeСhoice);
            OppositePlayer(PlayerStep).ChoosEnemy(MaxTimeСhoice);
            StopTimeChoice.Start();
        }

        /// <summary>
        /// Выбираем центральную территорию.
        /// </summary>
        void ChooseCentral()
        {
            OccasionHandler.Game(Server, "Выбираем центральную территорию.");
            if (Player1.Player.CentralTerritory == -1)
            {
                PlayerStep = Player1;
            }
            else
            if (Player2.Player.CentralTerritory == -1)
            {
                PlayerStep = Player2;
            }
            PlayerStep.ChooseCentral(Map.AccessibleCentralTerritories(PlayerStep), MaxTimeСhoiceCentral);
            OppositePlayer(PlayerStep).ChoosEnemyCentral(MaxTimeСhoiceCentral);
            StopTimeChoiceCentral.Start();
        }
        #endregion


        #region TimerStop(Действия происходящие при завершении таймер.Таймер обозначает завершения  некоторого процесса)
        void StopTask(object source, ElapsedEventArgs e)
        {
            StatusQuestion = false;
            OccasionHandler.Game(Server, "StopTask");
            ResultTask();

        }

        void StopChoiceCentral(object source, ElapsedEventArgs e)
        {
            OccasionHandler.Game(Server, "StopChoiceCentral");
            ResultChooseCentral();
        }

        void StopChoice(object source, ElapsedEventArgs e)
        {
            OccasionHandler.Game(Server, "StopChoice");
            ResultChooseTerritory();
        }
        #endregion

        #region Battle
        /// <summary>
        /// Запуск сражения между игрока.
        /// </summary>
        /// <param name="number"></param>
        void StartBattle()
        {
            OccasionHandler.Game(Server, "Запуск сражения между игрока.");
            if (Map.TestCentral(AttackTerritory))
            {
                SuperBattle();
            }
            else
            {
                Battle();
            }
        }
        /// <summary>
        /// Сражение за главную территорию.
        /// </summary>
        void SuperBattle()
        {
            StartQuestion(RandomTask(TypeTask.Answer));
            Player1.SuperBattle(Task, AttackTerritory, MaxTimeTask);
            Player2.SuperBattle(Task, AttackTerritory, MaxTimeTask);
        }
        /// <summary>
        /// Обычное сражение.
        /// </summary>
        void Battle()
        {
            StartQuestion(RandomTask(TypeTask.Answer));
            Player1.Battle(Task, AttackTerritory, MaxTimeTask);
            Player2.Battle(Task, AttackTerritory, MaxTimeTask);
        }

        /// <summary>
        /// Результат выполнения задания(викторины).
        /// </summary>
        void ResultTask()
        {
            OccasionHandler.Game(Server, "Результат выполнения задания(викторины).");
            StopTimeTask.Stop();
            if (Map.TestCentral(AttackTerritory))
            {
                ResultSuperBattle();
            }
            else
            {
                ResultBattle();

            }
        }

        /// <summary>
        /// Результат обычного сражения.
        /// </summary>
        void ResultBattle()
        {
            OccasionHandler.Game(Server, "Результат обычного сражения.");
            ClientGame winner = GetWinner();
            ClientGame loser = GetLoser(winner);

            Player1.ResultTask(Player1.Player.Answer, Player2.Player.Answer,idTask);
            Player2.ResultTask(Player2.Player.Answer, Player1.Player.Answer, idTask);

            Thread.Sleep(3000);


            Player1.ResulAnsver(Task.CorrectAnswer, Player1.Player.TimeAnswer, Player2.Player.TimeAnswer);
            Player2.ResulAnsver(Task.CorrectAnswer, Player2.Player.TimeAnswer, Player1.Player.TimeAnswer);

            Thread.Sleep(3000);


            if (winner == PlayerStep)
            {
                SetTerritory(winner, AttackTerritory);
            }
            else if (winner != null)
            {
                winner.Player.Point += WinPoint;
            }

            NextStep();

        }

        /// <summary>
        /// Результат сражения за главную территорию.
        /// </summary>
        void ResultSuperBattle()
        {
            OccasionHandler.Game(Server, "Результат сражения на главной территорию.");
            ClientGame winner = GetWinner();
            ClientGame loser = GetLoser(winner);

            Player1.ResultTask(Player1.Player.Answer, Player2.Player.Answer, idTask);
            Player2.ResultTask(Player2.Player.Answer, Player1.Player.Answer, idTask);

            Thread.Sleep(3000);

            Player1.ResulAnsver(Task.CorrectAnswer,Player1.Player.TimeAnswer,Player2.Player.TimeAnswer);
            Player2.ResulAnsver(Task.CorrectAnswer,Player2.Player.TimeAnswer, Player1.Player.TimeAnswer);

            Thread.Sleep(3000);

            if (winner == PlayerStep)
            {
                Map.Territories[AttackTerritory].Owner.Player.Point -= CentalLossPoint;

                Player1.Player.Reset();
                Player2.Player.Reset();

                UpDATA();

                if (EndGameCheck())
                {
                    return;
                }

                StartBattle();

                return;
            }
            else if (winner != null)
            {
                winner.Player.Point += WinPoint;
            }

            NextStep();


        }

        /// <summary>
        /// Возвращает победителя.
        /// </summary>
        /// <returns></returns>
        ClientGame GetWinner()
        {
            #region Без ответа .
            if (Player1.Player.Answer == -1 && Player2.Player.Answer == -1)
            {
                Player1.Player.TimeAnverAll += TimeSpan.FromMilliseconds(MaxTimeTask);
                Player2.Player.TimeAnverAll += TimeSpan.FromMilliseconds(MaxTimeTask);
                return null;
            }

            if (Player1.Player.Answer == -1)
            {
                Player1.Player.TimeAnverAll += TimeSpan.FromMilliseconds(MaxTimeTask);
                Player2.Player.TimeAnverAll += Player2.Player.TimeAnswer;
                Player2.Player.CorrectAnswer++;
                return Player2;
            }

            if (Player2.Player.Answer == -1)
            {
                Player2.Player.TimeAnverAll += TimeSpan.FromMilliseconds(MaxTimeTask);
                Player1.Player.TimeAnverAll += Player1.Player.TimeAnswer;
                Player1.Player.CorrectAnswer++;
                return Player1;
            }
            #endregion

            if (Task is TaskAnswer)
            {
                Player1.Player.TimeAnverAll += Player1.Player.TimeAnswer;
                Player2.Player.TimeAnverAll += Player2.Player.TimeAnswer;
                #region TaskAnswer
                if (Task.CorrectAnswer == Player1.Player.Answer && Task.CorrectAnswer == Player2.Player.Answer)
                {
                    Player1.Player.CorrectAnswer++;
                    Player2.Player.CorrectAnswer++;
                    if (Player1.Player.TimeAnswer > Player2.Player.TimeAnswer)
                    {
                        return Player2;
                    }
                    else
                    {
                        return Player1;
                    }
                }

                if (Task.CorrectAnswer == Player1.Player.Answer)
                {
                    Player1.Player.CorrectAnswer++;
                    return Player1;
                }
                if (Task.CorrectAnswer == Player2.Player.Answer)
                {
                    Player2.Player.CorrectAnswer++;
                    return Player2;
                }
                return null;
                #endregion
            }

            else
            {
                #region NumericeTask
                int AnswerP1 = Math.Abs(Task.CorrectAnswer - Player1.Player.Answer);
                int AnswerP2 = Math.Abs(Task.CorrectAnswer - Player2.Player.Answer);
                if (AnswerP1 < AnswerP2)
                {
                    return Player1;
                }
                if (AnswerP1 < AnswerP2)
                {
                    return Player2;
                }
                if (AnswerP1 == AnswerP2)
                {
                    if (Player1.Player.TimeAnswer > Player2.Player.TimeAnswer)
                    {
                        return Player2;
                    }
                    else
                    {
                        return Player1;
                    }
                }
                return null;
                #endregion
            }



        }
        ClientGame GetLoser(ClientGame winner)
        {
            ClientGame loser = null;
            if (winner == Player1)
            {
                loser = Player2;
            }
            else if (winner == Player2)
            {
                loser = Player1;
            }
            return loser;
        }
        #endregion



        void NextStep()
        {
           

            if (Status)
            {
                NumStep++;

                OccasionHandler.Game(Server, "NextStep");
                Reset();
                UpDATA();

                if (Player1.Player.CentralTerritory == -1 || Player2.Player.CentralTerritory == -1)
                {
                    ChooseCentral();
                    return;
                }

                if (EndGameCheck())
                {
                    NumStep--;
                    return;
                }


                if (PlayerStep == Player1)
                {
                    PlayerStep = Player2;
                }
                else
                {
                    PlayerStep = Player1;
                }
                Choose();
            }
            else
            {
                UpDATA();
                End();
            }
        }

        void Reset()
        {
            Player1.Player.Reset();
            Player2.Player.Reset();
            AttackTerritory = -1;
        }

        bool EndGameCheck()
        {

            if (Player1.Player.Point <= 0 || Player2.Player.Point <= 0)
            {
                End();
                return true;
            }
            return false;
        }

        public void End()
        {
            Status = false;
            
            SQL.AddNumGame(Player1.Client.Profile.Login);
            SQL.AddNumAnswersGame(Player1.Client.Profile.Login,NumAnswer);
            SQL.AddCorrectAnswersGame(Player1.Client.Profile.Login,Player1.Player.CorrectAnswer);

            SQL.AddNumGame(Player2.Client.Profile.Login);
            SQL.AddNumAnswersGame(Player2.Client.Profile.Login,NumAnswer);
            SQL.AddCorrectAnswersGame(Player2.Client.Profile.Login, Player2.Player.CorrectAnswer);

            if (Player1.Player.Point<=0)
            {
              
                SQL.AddWinGame(Player2.Client.Profile.Login);
                OccasionHandler.Game(Server, "Игра завершена(победитель игрок"+Player2.Client.Profile.Login);
                Player2.GameOver(true,Player2,Player1,NumStep,NumAnswer,StartTime);
                Player1.GameOver(false, Player1, Player2, NumStep, NumAnswer, StartTime);
                OccasionHandler.Game(Server, "Игра завершена\n" + "Победитель:" + Player2.Client.Profile.Login+"\n"+"Проигравший:" + Player1.Client.Profile.Login);
            }
            else
            {
                OccasionHandler.Game(Server, "Игра завершена(победитель игрок" + Player1.Client.Profile.Login);
                SQL.AddWinGame(Player1.Client.Profile.Login);
                Player2.GameOver(false, Player2, Player1, NumStep, NumAnswer, StartTime);
                Player1.GameOver(true, Player1, Player2, NumStep, NumAnswer, StartTime);
                OccasionHandler.Game(Server, "Игра завершена\n" + "Победитель:" + Player1.Client.Profile.Login + "\n" + "Проигравший:" + Player2.Client.Profile.Login);
            }
           
            MainServer.Server.StopGame(Server);
            //Player1.Disconnect();
            //Player2.Disconnect();
        }

        public void Start()
        {
            

            Status = true;

            StartTime = DateTime.Now.TimeOfDay;

            Player1.Send(MainServer.GenerationData.game.Start(Player2));
            Thread.Sleep(100);
            Player2.Send(MainServer.GenerationData.game.Start(Player1));
            Thread.Sleep(100);
            NextStep();
            Player1.StartGame(Player2.Client.Profile.Login);
            Thread.Sleep(100);
            Player2.StartGame(Player1.Client.Profile.Login);

            Player1.StartGame(Player2.Client.Profile.Login);
            Thread.Sleep(100);
            Player2.StartGame(Player1.Client.Profile.Login);

            OccasionHandler.Game(Server, "Игра началась");

        }

        void UpDATA()
        {
            if (Player1.Player.Point < 0)
            {
                Player1.Player.Point = 0;
            }
            if (Player2.Player.Point < 0)
            {
                Player2.Player.Point = 0;
            }

            if (Player1.Player.CentralTerritory != -1)
                Map.Territories[Player1.Player.CentralTerritory].Point = Player1.Player.Point;
            if (Player2.Player.CentralTerritory != -1)
                Map.Territories[Player2.Player.CentralTerritory].Point = Player2.Player.Point;
            Player1.UpData(Map.Territories, Player2);
            Player2.UpData(Map.Territories, Player1);
            OccasionHandler.Game(Server, "UpDATA");
            Thread.Sleep(2000);
        }


    }
}
