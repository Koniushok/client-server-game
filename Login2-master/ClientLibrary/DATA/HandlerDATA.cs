using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;
using System.IO;
using Games;
using System.Windows.Threading;

namespace Client
{
    public class HandlerDATA
    {
        public HandlerDATA()
        {

        }
        LoginHadler login = new LoginHadler();
        GameServerHadler gameServer = new GameServerHadler();
        DataHadler data = new DataHadler();
        public void Processing(Server server, BinaryReader reader)
        {
            ServerCommand command = (ServerCommand)reader.ReadInt32();
            //System.Windows.Forms.MessageBox.Show(command.ToString());
            switch (command)
            {
                case ServerCommand.Login:
                    {
                        login.Processing(server, reader);
                        break;
                    }
                case ServerCommand.Game:
                    {
                        gameServer.Processing(server, reader);
                        break;
                    }
                case ServerCommand.Data:
                    {
                        data.Processing(server, reader);
                        break;
                    }
            }

        }

        GameHadler game = new GameHadler();
        public void Processing(GameServer gameServer, BinaryReader reader) 
        {
            game.Processing(gameServer, reader);
        }
    }

    class GameHadler : DispatcherObject
    {
        BinaryReader Reader { get; set; }
        GameServer GameServer { get; set; }
        public void Processing(GameServer gameServer, BinaryReader reader)
        {
            Reader = reader;
            GameServer = gameServer;
            GameCommanServer command = (GameCommanServer)Reader.ReadInt32();
            switch (command)
            {
                case GameCommanServer.Start:
                    {
                        Start();
                        break;
                    }
                case GameCommanServer.Battle:
                    {
                        Battle();
                        break;
                    }
                case GameCommanServer.Choose:
                    {
                        Choose();
                        break;
                    }
                case GameCommanServer.ChooseCentral:
                    {
                        ChooseCentral();
                        break;
                    }
                case GameCommanServer.ResresultChoose:
                    {
                        ResresultChoose();
                        break;
                    }
                case GameCommanServer.ResultBattle:
                    {
                        ResultBattle();
                        break;
                    }
                case GameCommanServer.ResultChooseCetral:
                    {
                        ResultChooseCetral();
                        break;
                    }
                case GameCommanServer.ResultSuperBattle:
                    {
                        ResultSuperBattle();
                        break;
                    }
                case GameCommanServer.SuperBattle:
                    {
                        SuperBattle();
                        break;
                    }
                case GameCommanServer.UpData:
                    {
                        UpData();
                        break;
                    }
                case GameCommanServer.UpTerritory:
                    {
                        UpTerritory();
                        break;
                    }
                case GameCommanServer.ChoosEnemyCentral:
                    {
                        ChoosEnemyCentral();
                        break;
                    }
                case GameCommanServer.ChoosEnemy:
                    {
                        ChoosEnemy();
                        break;
                    }
                case GameCommanServer.ResultTask:
                    {
                        ResultTask();
                        break;
                    }
                case GameCommanServer.ResulAnsver:
                    {
                        ResulAnsver();
                        break;
                    }
                case GameCommanServer.GameOver:
                    {
                        GameOver();
                        break;
                    }
               

            }

        }

        public void Start()
        {
            string login = Reader.ReadString();
            GameServer.Game.Start(login);
            Dispatcher.BeginInvoke((Action)(() => GameServerOccasionHandler.StartGame()));
        }

        public void ChooseCentral()
        {
            int length = Reader.ReadInt32();
            bool[] territory = new bool[length];
            for (int i = 0; i < length; i++)
            {
                territory[i] = Reader.ReadBoolean();
            }
            int maxtime = Reader.ReadInt32();

            Dispatcher.BeginInvoke((Action<bool[], int>)((ter, time) => GameServer.Game.ChooseCentral(ter, time)), territory, maxtime);
        }

        public void Choose()
        {
            int length = Reader.ReadInt32();
            bool[] territory = new bool[length];
            for (int i = 0; i < length; i++)
            {
                territory[i] = Reader.ReadBoolean();
            }
            int maxtime = Reader.ReadInt32();

            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.Choose(territory, maxtime)));

        }

        public void ResresultChoose()
        {
            string login = Reader.ReadString();
            int attackTerritory = Reader.ReadInt32();
            bool assign = Reader.ReadBoolean();

            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.ResresultChoose(login, attackTerritory, assign)));
        }

        public void ResultChooseCetral()
        {
            string login = Reader.ReadString();
            int territory = Reader.ReadInt32();


            Dispatcher.BeginInvoke((Action<string, int>)((logins, ter) => GameServer.Game.ResresultChooseCetral(logins, ter)), login, territory);

        }

        public void UpData()
        {

            string login = Reader.ReadString();
            int point = Reader.ReadInt32();
            Player myPlayer = new Player(point, login);

            login = Reader.ReadString();
            point = Reader.ReadInt32();
            Player enemy = new Player(point, login);



            int length = Reader.ReadInt32();
            Territory[] territories = new Territory[length];
            for (int i = 0; i < length; i++)
            {
                login = Reader.ReadString();
                Player owner = null;
                if (login == enemy.Login)
                {
                    owner = enemy;
                }
                else if (login == myPlayer.Login)
                {
                    owner = myPlayer;
                }

                point = Reader.ReadInt32();

                bool central = Reader.ReadBoolean();

                int hp = Reader.ReadInt32();

                territories[i] = new Territory(owner, point, central, hp);
            }


            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.UpData(territories, enemy, myPlayer)));



        }

        public void UpTerritory()
        {
            string login = "null";
            Player myPlayer = GameServer.Game.MyPlayer;
            Player enemy = GameServer.Game.Enemy;
            int point = 0;


            int length = Reader.ReadInt32();
            Territory[] territories = new Territory[length];
            for (int i = 0; i < length; i++)
            {
                login = Reader.ReadString();
                Player owner = null;
                if (login == enemy.Login)
                {
                    owner = enemy;
                }
                else if (login == enemy.Login)
                {
                    owner = myPlayer;
                }

                point = Reader.ReadInt32();

                bool central = Reader.ReadBoolean();

                int hp = Reader.ReadInt32();

                territories[i] = new Territory(owner, point, central, hp);
            }

            GameServer.Game.UpTerritory(territories);
        }

        public void Battle()
        {
            TypeTask typetask = (TypeTask)Reader.ReadInt32();
            GameTask task = null;
            if (typetask == TypeTask.Answer)
            {
                TaskAnswer t = new TaskAnswer();
                t.Question = Reader.ReadString();
                for (int i = 0; i < 4; i++)
                {
                    t.Answers[i] = Reader.ReadString();

                }
                task = t;
            }
            else
                if (typetask == TypeTask.Numeric)
            {
                NumericTask t = new NumericTask();
                t.Question = Reader.ReadString();
                task = t;
            }

            int AttackTerritory = Reader.ReadInt32();
            int Maxtime = Reader.ReadInt32();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.Battle(task, AttackTerritory, Maxtime)));
            //GameServer.Game.Battle(task, AttackTerritory, Maxtime);


        }

        public void SuperBattle()
        {
            TypeTask typetask = (TypeTask)Reader.ReadInt32();
            GameTask task = null;
            if (typetask == TypeTask.Answer)
            {
                TaskAnswer t = new TaskAnswer();
                t.Question = Reader.ReadString();
                for (int i = 0; i < 4; i++)
                {
                    t.Answers[i] = Reader.ReadString();

                }
                task = t;
            }
            else
                if (typetask == TypeTask.Numeric)
            {
                NumericTask t = new NumericTask();
                t.Question = Reader.ReadString();
                task = t;
            }

            int AttackTerritory = Reader.ReadInt32();
            int Maxtime = Reader.ReadInt32();

            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.SuperBattle(task, AttackTerritory, Maxtime)));


        }

        public void ResultBattle()
        {
            int pointWinner = 0;
            int answerWiner = 0;
            string loginWinner = Reader.ReadString();
            if (loginWinner != "null")
            {
                pointWinner = Reader.ReadInt32();
                answerWiner = Reader.ReadInt32();
            }

            int pointLoser = 0;
            int anserLoser = 0;
            string loginLoser = Reader.ReadString();
            if (loginLoser != "null")
            {
                pointLoser = Reader.ReadInt32();
                anserLoser = Reader.ReadInt32();
            }

            TypeTask typetask = (TypeTask)Reader.ReadInt32();
            GameTask task = null;
            if (typetask == TypeTask.Answer)
            {
                TaskAnswer t = new TaskAnswer();
                t.Question = Reader.ReadString();
                for (int i = 0; i < 4; i++)
                {
                    t.Answers[i] = Reader.ReadString();
                }
                t.CorrectAnswer = Reader.ReadInt32();
                task = t;
            }
            else
                if (typetask == TypeTask.Numeric)
            {
                NumericTask t = new NumericTask();
                t.Question = Reader.ReadString();
                t.CorrectAnswer = Reader.ReadInt32();
                task = t;
            }

            GameServer.Game.ResultBattle(loginWinner, loginLoser, pointWinner, pointLoser, answerWiner, anserLoser, task);


        }

        public void ResultSuperBattle()
        {
            int pointWinner = 0;
            int answerWiner = 0;
            string loginWinner = Reader.ReadString();
            if (loginWinner != "null")
            {
                pointWinner = Reader.ReadInt32();
                answerWiner = Reader.ReadInt32();
            }

            int pointLoser = 0;
            int anserLoser = 0;
            string loginLoser = Reader.ReadString();
            if (loginLoser != "null")
            {
                pointLoser = Reader.ReadInt32();
                anserLoser = Reader.ReadInt32();
            }

            TypeTask typetask = (TypeTask)Reader.ReadInt32();
            GameTask task = null;
            if (typetask == TypeTask.Answer)
            {
                TaskAnswer t = new TaskAnswer();
                t.Question = Reader.ReadString();
                for (int i = 0; i < 4; i++)
                {
                    t.Answers[i] = Reader.ReadString();
                }
                t.CorrectAnswer = Reader.ReadInt32();
                task = t;
            }
            else
                if (typetask == TypeTask.Numeric)
            {
                NumericTask t = new NumericTask();
                t.Question = Reader.ReadString();
                t.CorrectAnswer = Reader.ReadInt32();
                task = t;
            }

            GameServer.Game.ResultSuperBattle(loginWinner, loginLoser, pointWinner, pointLoser, answerWiner, anserLoser, task);
        }

        public void ChoosEnemy()
        {
            int time = Reader.ReadInt32();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.ChoosEnemy(time)));

        }

        public void ChoosEnemyCentral()
        {
            int time = Reader.ReadInt32();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.ChoosEnemyCentral(time)));
        }

        public void ResultTask()
        {
            int my = Reader.ReadInt32();
            int enemy = Reader.ReadInt32();
            int numTask = Reader.ReadInt32();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.ResultTask(my, enemy,numTask)));
        }

        public void ResulAnsver()
        {
            int ansver = Reader.ReadInt32();
            string myTime = Reader.ReadString();
            string enemyTime = Reader.ReadString();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.ResulAnsver(ansver,myTime,enemyTime)));
        }

        public void GameOver()
        {

            bool win = Reader.ReadBoolean();

            int myCorrectAnswer = Reader.ReadInt32();
            string myTimeAnsver = Reader.ReadString();

            int enemyCorrectAnswer = Reader.ReadInt32();
            string enemyTimeAnsver = Reader.ReadString();

            int numNumStep = Reader.ReadInt32();
            int NumAnswer = Reader.ReadInt32();

            string timeGame = Reader.ReadString();
            Dispatcher.BeginInvoke((Action)(() => GameServer.Game.GameOver(win, myCorrectAnswer, myTimeAnsver, enemyCorrectAnswer, enemyTimeAnsver, numNumStep, NumAnswer, timeGame)));
        }


    }

    class LoginHadler : DispatcherObject
    {
        BinaryReader Reader { get; set; }
        Server Server { get; set; }
        public void Processing(Server server, BinaryReader reader)
        {
            Reader = reader;
            Server = server;
            ServerLoginCommand command = (ServerLoginCommand)Reader.ReadInt32();
            switch (command)
            {
                case ServerLoginCommand.Authoriztion:
                    {
                        Authoriztion();
                        break;
                    }
                case ServerLoginCommand.Registation:
                    {
                        Registation();
                        break;
                    }
            }


        }

        void Authoriztion()
        {
            bool test = Reader.ReadBoolean();
            string mes = "";
            if (test)
            {
                string login = Reader.ReadString();
                string name = Reader.ReadString();
                string surname = Reader.ReadString();
                ClientProfile profile = new ClientProfile(login, name, surname);
                MainClient.Profile = profile;
            }
            else
            {
                mes = Reader.ReadString();
            }
            Dispatcher.BeginInvoke((Action<bool, string>)((tests, message) => OccasionHandler.Authoriztion(tests, message)), test, mes);
        }

        void Registation()
        {
            bool t = Reader.ReadBoolean();
            string mes = Reader.ReadString();
            Dispatcher.BeginInvoke((Action<bool, string>)((test, message) => OccasionHandler.Registation(test, message)), t, mes);
        }
    }

    class GameServerHadler
    {
        BinaryReader Reader { get; set; }
        Server Server { get; set; }
        public void Processing(Server server, BinaryReader reader)
        {
            Reader = reader;
            Server = server;
            ServerGameComman command = (ServerGameComman)Reader.ReadInt32();
            switch (command)
            {
                case ServerGameComman.QuickGame:
                    {
                        QuickGame();
                        break;
                    }
            }
        }

        void QuickGame()
        {
            int port = Reader.ReadInt32();

            GameServer gameServer = new GameServer(port, MainClient.Server.IpEndPoint.Address);

            if (gameServer.connect("0000"))
            {
                if (MainClient.GameServer != null)
                {
                    MainClient.GameServer.Disconnect();
                }
                MainClient.GameServer = gameServer;

            }
        }
    }


    class DataHadler : DispatcherObject
    {
        BinaryReader Reader { get; set; }
        Server Server { get; set; }
        public void Processing(Server server, BinaryReader reader)
        {
            Reader = reader;
            Server = server;
            ServerDataCommand command = (ServerDataCommand)Reader.ReadInt32();
            switch (command)
            {
                case ServerDataCommand.GetImage:
                    {
                        GetImage();
                        break;
                    }
                case ServerDataCommand.GetImageClient:
                    {
                        GetImageClient();
                        break;
                    }
                case ServerDataCommand.GetStatistics:
                    {
                        GetStatistics();
                        break;
                    }
            }
        }

        void GetImage()
        {


            int length = Reader.ReadInt32();
            byte[] img = new byte[length+ length];



            int imgLength = 0;
            while (imgLength < length)
            {
                byte[] buffer = new byte[length+ length];


                int bufferLength = Server.Socket.Receive(buffer);

                buffer.Take(bufferLength).ToArray().CopyTo(img, imgLength);



                imgLength += bufferLength;
            }
            Dispatcher.BeginInvoke((Action)(() => DataOccasionHandler.GetImage(img)));

        }

        void GetImageClient()
        {
            try
            {
                //System.Windows.Forms.MessageBox.Show("2");

                string login = Reader.ReadString();

                int length = Reader.ReadInt32();
                byte[] img = new byte[length+ length];



                int imgLength = 0;
                while (imgLength < length)
                {
                    byte[] buffer = new byte[length + length];


                    int bufferLength = Server.Socket.Receive(buffer);

                    buffer.Take(bufferLength).ToArray().CopyTo(img, imgLength);

                    imgLength += bufferLength;

                    //System.Windows.Forms.MessageBox.Show(imgLength.ToString() +"            " +login);
                }
                //System.Windows.Forms.MessageBox.Show(img.Length.ToString()+login);
                Dispatcher.BeginInvoke((Action)(() => DataOccasionHandler.GetImageClient(img, login)));
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        void GetStatistics()
        {
            string login = Reader.ReadString();

            int NumGame=Reader.ReadInt32();
            int WinGame = Reader.ReadInt32();
            int LeaveGame = Reader.ReadInt32();
            int NumAnswers = Reader.ReadInt32();
            int CorrectAnswers = Reader.ReadInt32();
            string Name = Reader.ReadString();
            string SurName = Reader.ReadString();

            Statistics stat = new Statistics(NumGame, WinGame, LeaveGame, NumAnswers, CorrectAnswers,Name,SurName,login);

            Dispatcher.BeginInvoke((Action)(() => DataOccasionHandler.GetStatistics(stat, login)));
        }
    }
}
