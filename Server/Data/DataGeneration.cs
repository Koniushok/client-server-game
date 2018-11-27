using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;
using System.IO;
using Games;

namespace Server
{
    /// <summary>
    /// Класс для генерации команды клиенту.
    /// </summary>
    //class DataGeneration
    //{
    //    LoginCommand login = new LoginCommand();
    //    public byte[] Generation(ServerCommand TypeCommand, object command, params object[] arg)
    //    {
    //        MemoryStream stream = new MemoryStream();
    //        BinaryWriter writer = new BinaryWriter(stream);
    //        writer.Write((int)TypeCommand);
    //        switch (TypeCommand)
    //        {
    //            case ServerCommand.Login:
    //                {
    //                    login.Ganeration((ServerLoginCommand)command, writer, arg);
    //                    break;
    //                }
    //        }
    //        byte[] buffer = stream.GetBuffer();
    //        return buffer;
    //    }

    //    GameCommand game = new GameCommand();
    //    public byte[] Generation(ServerGameComman command, params object[] arg)
    //    {
    //        MemoryStream stream = new MemoryStream();
    //        BinaryWriter writer = new BinaryWriter(stream);
    //        writer.Write((int)command);
    //        game.Ganeration(command, writer, arg);
    //        byte[] buffer = stream.GetBuffer();
    //        return buffer;
    //    }
    //}

    //class GameCommand
    //{
    //    BinaryWriter Writer { get; set; }
    //    public void Ganeration(ServerGameComman command, BinaryWriter writer, params object[] arg)
    //    {
    //        Writer = writer;
    //        switch (command)
    //        {
    //            case ServerGameComman.Battle:
    //                {
    //                    Battle((GameTask)arg[0], (int)arg[1], (int)arg[2]);
    //                    break;
    //                }
    //            case ServerGameComman.Choose:
    //                {
    //                    Choose((bool[])arg[0], (int)arg[1]);
    //                    break;
    //                }
    //            case ServerGameComman.ChooseCentral:
    //                {
    //                    ChooseCentral((bool[])arg[0], (int)arg[1]);
    //                    break;
    //                }
    //            case ServerGameComman.ResresultChoose:
    //                {

    //                    break;
    //                }
    //            case ServerGameComman.ResultChooseCetral:
    //                {
    //                    ResultChooseCetral((ClientGame)arg[0], (int)arg[1]);
    //                    break;
    //                }
    //            case ServerGameComman.ResultBattle:
    //                {
    //                    ResultBattle((ClientGame)arg[0], (ClientGame)arg[1], (GameTask)arg[2]);
    //                    break;
    //                }
    //            case ServerGameComman.ResultSuperBattle:
    //                {
    //                    ResultSuperBattle((ClientGame)arg[0], (ClientGame)arg[1], (GameTask)arg[2]);
    //                    break;
    //                }
    //            case ServerGameComman.SuperBattle:
    //                {
    //                    SuperBattle((GameTask)arg[0], (int)arg[1], (int)arg[2]);
    //                    break;
    //                }
    //            case ServerGameComman.UpData:
    //                {
    //                    UpData((Territory[])arg[0], (ClientGame)arg[1], (ClientGame)arg[2]);
    //                    break;
    //                }
    //            case ServerGameComman.UpTerritory:
    //                {
    //                    UpTerritory((Territory[])arg[0]);
    //                    break;
    //                }

    //        }
    //    }

    //    public void ChooseCentral(bool[] Territory, int Maxtime)
    //    {
    //        Writer.Write(Territory.Length);
    //        foreach (bool item in Territory)
    //        {
    //            Writer.Write(item);
    //        }
    //        Writer.Write(Maxtime);

    //    }

    //    public void Choose(bool[] Territory, int Maxtime)
    //    {
    //        foreach (bool item in Territory)
    //        {
    //            Writer.Write(item);
    //        }
    //        Writer.Write(Maxtime);
    //    }

    //    public void ResresultChoose(ClientGame client, int attackTerritory, bool assign)
    //    {
    //        Writer.Write(client.Client.Profile.Login);
    //        Writer.Write(attackTerritory);
    //        Writer.Write(assign);
    //    }

    //    public void ResultChooseCetral(ClientGame client, int Territory)
    //    {
    //        Writer.Write(client.Client.Profile.Login);
    //        Writer.Write(Territory);
    //    }

    //    public void UpData(Territory[] Territories, ClientGame myPlayer, ClientGame enemy)
    //    {
    //        Writer.Write(myPlayer.Client.Profile.Login);
    //        Writer.Write(myPlayer.Player.Point);
    //        Writer.Write(enemy.Client.Profile.Login);
    //        Writer.Write(enemy.Player.Point);

    //        UpTerritory(Territories);
    //    }

    //    public void UpTerritory(Territory[] Territories)
    //    {
    //        Writer.Write(Territories.Length);
    //        foreach (Territory item in Territories)
    //        {
    //            if (item.Owner != null)
    //            {
    //                Writer.Write(item.Owner.Client.Profile.Login);
    //            }
    //            else
    //            {
    //                Writer.Write("null");
    //            }
    //            Writer.Write(item.Point);
    //            Writer.Write(item.Central);
    //            Writer.Write(item.HP);
    //        }
    //    }

    //    public void Battle(GameTask task, int AttackTerritory, int Maxtime)
    //    {
    //        if (task is TaskAnswer)
    //        {
    //            Writer.Write((int)TypeTask.Answer);
    //            TaskAnswer t = task as TaskAnswer;
    //            Writer.Write(t.Question);
    //            foreach (string item in t.Answers)
    //            {
    //                Writer.Write(item);
    //            }
    //        }

    //        if (task is NumericTask)
    //        {
    //            Writer.Write((int)TypeTask.Numeric);
    //            NumericTask t = task as NumericTask;
    //            Writer.Write(t.Question);
    //        }

    //        Writer.Write(AttackTerritory);
    //        Writer.Write(Maxtime);
    //    }

    //    public void SuperBattle(GameTask task, int AttackTerritory, int Maxtime)
    //    {
    //        if (task is TaskAnswer)
    //        {
    //            TaskAnswer t = task as TaskAnswer;
    //            Writer.Write(t.Question);
    //            foreach (string item in t.Answers)
    //            {
    //                Writer.Write(item);
    //            }
    //        }

    //        if (task is NumericTask)
    //        {
    //            NumericTask t = task as NumericTask;
    //            Writer.Write(t.Question);
    //        }

    //        Writer.Write(AttackTerritory);
    //        Writer.Write(Maxtime);
    //    }

    //    public void ResultBattle(ClientGame winner, ClientGame loser, GameTask task)
    //    {
    //        if (winner != null)
    //        {
    //            Writer.Write(winner.Client.Profile.Login);
    //            Writer.Write(winner.Player.Point);
    //            Writer.Write(winner.Player.Answer);
    //        }
    //        else
    //        {
    //            Writer.Write("null");
    //        }

    //        if (loser != null)
    //        {
    //            Writer.Write(loser.Client.Profile.Login);
    //            Writer.Write(loser.Player.Point);
    //            Writer.Write(loser.Player.Answer);
    //        }
    //        else
    //        {
    //            Writer.Write("null");
    //        }

    //        if (task is TaskAnswer)
    //        {
    //            TaskAnswer t = task as TaskAnswer;
    //            Writer.Write(t.Question);
    //            foreach (string item in t.Answers)
    //            {
    //                Writer.Write(item);
    //            }
    //            Writer.Write(t.CorrectAnswer);
    //        }

    //        if (task is NumericTask)
    //        {
    //            NumericTask t = task as NumericTask;
    //            Writer.Write(t.Question);
    //            Writer.Write(t.CorrectAnswer);
    //        }
    //    }

    //    public void ResultSuperBattle(ClientGame winner, ClientGame loser, GameTask task)
    //    {
    //        if (winner != null)
    //        {
    //            Writer.Write(winner.Client.Profile.Login);
    //            Writer.Write(winner.Player.Point);
    //            Writer.Write(winner.Player.Answer);
    //        }
    //        else
    //        {
    //            Writer.Write("null");
    //        }

    //        if (loser != null)
    //        {
    //            Writer.Write(loser.Client.Profile.Login);
    //            Writer.Write(loser.Player.Point);
    //            Writer.Write(loser.Player.Answer);
    //        }
    //        else
    //        {
    //            Writer.Write("null");
    //        }

    //        if (task is TaskAnswer)
    //        {
    //            TaskAnswer t = task as TaskAnswer;
    //            Writer.Write(t.Question);
    //            foreach (string item in t.Answers)
    //            {
    //                Writer.Write(item);
    //            }
    //            Writer.Write(t.CorrectAnswer);
    //        }

    //        if (task is NumericTask)
    //        {
    //            NumericTask t = task as NumericTask;
    //            Writer.Write(t.Question);
    //            Writer.Write(t.CorrectAnswer);
    //        }
    //    }
    //}

    //class LoginCommand
    //{
    //    BinaryWriter Writer { get; set; }
    //    public void Ganeration(ServerLoginCommand command, BinaryWriter writer, params object[] arg)
    //    {
    //        Writer = writer;
    //        switch (command)
    //        {
    //            case ServerLoginCommand.Authoriztion:
    //                {
    //                    Authoriztion((string)arg[0], (string)arg[1], (string)arg[2], (string)arg[3]);
    //                    break;
    //                }
    //            case ServerLoginCommand.Registation:
    //                {
    //                    Registation((bool)arg[0], (string)arg[1]);
    //                    break;
    //                }
    //        }
    //    }

    //    void Registation(bool test,string message)
    //    {
    //        Writer.Write(test);
    //        Writer.Write(message);
    //    }

    //    void Authoriztion(string login, string password, string name, string surname)
    //    {
    //        Writer.Write(login);
    //        Writer.Write(password);
    //        Writer.Write(name);
    //        Writer.Write(surname);
    //    }
    //}

    public class DataGeneration
    {
        public LoginCommand login = new LoginCommand();
        public GameCommand game = new GameCommand();
        public ServerGame serverGame = new ServerGame();
        public DataCommand data = new DataCommand();

    }

    public class GameCommand
    {
        public byte[] Start(ClientGame enemy)
        {
            GameCommanServer command = GameCommanServer.Start;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(enemy.Client.Profile.Login);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ChooseCentral(bool[] Territory, int Maxtime)
        {
            GameCommanServer command = GameCommanServer.ChooseCentral;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(Territory.Length);
            foreach (bool item in Territory)
            {
                writer.Write(item);
            }
            writer.Write(Maxtime);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] Choose(bool[] Territory, int Maxtime)
        {

            GameCommanServer command = GameCommanServer.Choose;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);
            writer.Write(Territory.Length);
            foreach (bool item in Territory)
            {
                writer.Write(item);
            }
            writer.Write(Maxtime);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] ResresultChoose(ClientGame client, int attackTerritory, bool assign)
        {

            GameCommanServer command = GameCommanServer.ResresultChoose;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);


            writer.Write(client.Client.Profile.Login);
            writer.Write(attackTerritory);
            writer.Write(assign);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ResultChooseCetral(ClientGame client, int Territory)
        {
            GameCommanServer command = GameCommanServer.ResultChooseCetral;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);


            writer.Write(client.Client.Profile.Login);
            writer.Write(Territory);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] UpData(Territory[] Territories, ClientGame myPlayer, ClientGame enemy)
        {
            GameCommanServer command = GameCommanServer.UpData;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);


            writer.Write(myPlayer.Client.Profile.Login);
            writer.Write(myPlayer.Player.Point);
            writer.Write(enemy.Client.Profile.Login);
            writer.Write(enemy.Player.Point);

            writer.Write(Territories.Length);
            foreach (Territory item in Territories)
            {
                if (item.Owner != null)
                {
                    writer.Write(item.Owner.Client.Profile.Login);
                }
                else
                {
                    writer.Write("null");
                }
                writer.Write(item.Point);
                writer.Write(item.Central);
                writer.Write(item.HP);
            }

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] UpTerritory(Territory[] Territories)
        {
            GameCommanServer command = GameCommanServer.UpTerritory;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);
            writer.Write(Territories.Length);

            foreach (Territory item in Territories)
            {
                if (item.Owner != null)
                {
                    writer.Write(item.Owner.Client.Profile.Login);
                }
                else
                {
                    writer.Write("null");
                }
                writer.Write(item.Point);
                writer.Write(item.Central);
                writer.Write(item.HP);
            }
            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] Battle(GameTask task, int AttackTerritory, int Maxtime)
        {
            Console.WriteLine(task.Question);
            GameCommanServer command = GameCommanServer.Battle;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            if (task is TaskAnswer)
            {
                writer.Write((int)TypeTask.Answer);
                TaskAnswer t = task as TaskAnswer;
                writer.Write(t.Question);
                foreach (string item in t.Answers)
                {
                    writer.Write(item);
                }
            }
            if (task is NumericTask)
            {
                writer.Write((int)TypeTask.Numeric);
                NumericTask t = task as NumericTask;
                writer.Write(t.Question);
            }

            writer.Write(AttackTerritory);
            writer.Write(Maxtime);
            byte[] buffer = stream.GetBuffer();
            return buffer;


        }

        public byte[] SuperBattle(GameTask task, int AttackTerritory, int Maxtime)
        {
            GameCommanServer command = GameCommanServer.SuperBattle;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            if (task is TaskAnswer)
            {
                writer.Write((int)TypeTask.Answer);
                TaskAnswer t = task as TaskAnswer;
                writer.Write(t.Question);
                foreach (string item in t.Answers)
                {
                    writer.Write(item);
                }
            }
            if (task is NumericTask)
            {
                writer.Write((int)TypeTask.Numeric);
                NumericTask t = task as NumericTask;
                writer.Write(t.Question);
            }

            writer.Write(AttackTerritory);
            writer.Write(Maxtime);
            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] ResultBattle(ClientGame winner, ClientGame loser, GameTask task)
        {
            GameCommanServer command = GameCommanServer.ResultBattle;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            if (winner != null)
            {
                writer.Write(winner.Client.Profile.Login);
                writer.Write(winner.Player.Point);
                writer.Write(winner.Player.Answer);
            }
            else
            {
                writer.Write("null");
            }

            if (loser != null)
            {
                writer.Write(loser.Client.Profile.Login);
                writer.Write(loser.Player.Point);
                writer.Write(loser.Player.Answer);
            }
            else
            {
                writer.Write("null");
            }

            if (task is TaskAnswer)
            {
                TaskAnswer t = task as TaskAnswer;
                writer.Write(t.Question);
                foreach (string item in t.Answers)
                {
                    writer.Write(item);
                }
                writer.Write(t.CorrectAnswer);
            }

            if (task is NumericTask)
            {
                NumericTask t = task as NumericTask;
                writer.Write(t.Question);
                writer.Write(t.CorrectAnswer);
            }

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ResultSuperBattle(ClientGame winner, ClientGame loser, GameTask task)
        {
            GameCommanServer command = GameCommanServer.ResultSuperBattle;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            if (winner != null)
            {
                writer.Write(winner.Client.Profile.Login);
                writer.Write(winner.Player.Point);
                writer.Write(winner.Player.Answer);
            }
            else
            {
                writer.Write("null");
            }

            if (loser != null)
            {
                writer.Write(loser.Client.Profile.Login);
                writer.Write(loser.Player.Point);
                writer.Write(loser.Player.Answer);
            }
            else
            {
                writer.Write("null");
            }

            if (task is TaskAnswer)
            {
                TaskAnswer t = task as TaskAnswer;
                writer.Write(t.Question);
                foreach (string item in t.Answers)
                {
                    writer.Write(item);
                }
                writer.Write(t.CorrectAnswer);
            }

            if (task is NumericTask)
            {
                NumericTask t = task as NumericTask;
                writer.Write(t.Question);
                writer.Write(t.CorrectAnswer);
            }

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ChoosEnemy(int maxTime)
        {
            GameCommanServer command = GameCommanServer.ChoosEnemy;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(maxTime);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ChoosEnemyCentral(int maxTime)
        {
            GameCommanServer command = GameCommanServer.ChoosEnemyCentral;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(maxTime);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ResultTask(int my, int enemy)
        {

            GameCommanServer command = GameCommanServer.ResultTask;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(my);
            writer.Write(enemy);
            writer.Write(-1);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ResultTask(int my, int enemy,int numTask)
        {

            GameCommanServer command = GameCommanServer.ResultTask;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(my);
            writer.Write(enemy);
            writer.Write(numTask);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] ResulAnsver(int ansver, TimeSpan myTime, TimeSpan enemyTime)
        {

            GameCommanServer command = GameCommanServer.ResulAnsver;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(ansver);
            
            writer.Write(myTime.ToString("mm\\:ss"));
            writer.Write(enemyTime.ToString("mm\\:ss"));

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GameOver(ClientGame winner, ClientGame loser)
        {
            GameCommanServer command = GameCommanServer.GameOver;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(winner.Client.Profile.Login);

            writer.Write(loser.Client.Profile.Login);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GameOver(bool win,ClientGame myPlayer, ClientGame enemyPlayer, int numNumStep, int NumAnswer, TimeSpan StartTime)
        {
            GameCommanServer command = GameCommanServer.GameOver;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(win);

            writer.Write(myPlayer.Player.CorrectAnswer);
            writer.Write(myPlayer.Player.TimeAnverAll.ToString("hh\\:mm\\:ss"));

            writer.Write(enemyPlayer.Player.CorrectAnswer);
            writer.Write(enemyPlayer.Player.TimeAnverAll.ToString("hh\\:mm\\:ss"));

            writer.Write(numNumStep);
            writer.Write(NumAnswer);
            writer.Write((StartTime- DateTime.Now.TimeOfDay).ToString("hh\\:mm\\:ss"));

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }

    public class LoginCommand
    {
        public byte[] Registation(bool test, string message)
        {
            ServerCommand TypeCommand = ServerCommand.Login;
            ServerLoginCommand command = ServerLoginCommand.Registation;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);



            writer.Write(test);
            writer.Write(message);

            byte[] buffer = stream.GetBuffer();

            return buffer;
        }

        public byte[] Authoriztion(ClientProfile profile, Client client)
        {

            ServerCommand TypeCommand = ServerCommand.Login;
            ServerLoginCommand command = ServerLoginCommand.Authoriztion;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            if (profile != null)
            {
                if (MainServer.Server.SearchClient(profile.Login) == null)
                {
                    client.Authorization = true;
                    writer.Write(true);
                    writer.Write(profile.Login);
                    writer.Write(profile.Name);
                    writer.Write(profile.Surname);
                    client.Profile = profile;
                }
                else
                {
                    writer.Write(false);
                    writer.Write("Этот профиль уже авторизован");
                }
            }
            else
            {
                writer.Write(false);
                writer.Write("Неверный логин или пароль");
            }

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }
    }

    public class ServerGame
    {
        public byte[] QuickServer(GameServer server)
        {
            ServerCommand command = ServerCommand.Game;
            ServerGameComman TypeCommand = ServerGameComman.QuickGame;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);
            writer.Write((int)TypeCommand);

            writer.Write(server.Port);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }

    public class DataCommand
    {
        public byte[] GetImage(byte[] img)
        {
            ServerCommand command = ServerCommand.Data;
            ServerDataCommand TypeCommand = ServerDataCommand.GetImage;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);
            writer.Write((int)TypeCommand);

               // writer.Write(true);
                writer.Write(img.Length);
            

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GetImageClient(byte[] img,string login)
        {
            ServerCommand command = ServerCommand.Data;
            ServerDataCommand TypeCommand = ServerDataCommand.GetImageClient;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            Console.WriteLine(img.Length);
            Console.WriteLine(img.Length);
            Console.WriteLine(img.Length);
            Console.WriteLine(img.Length);
            Console.WriteLine(img.Length);
            writer.Write((int)command);
            writer.Write((int)TypeCommand);

            // writer.Write(true);

            writer.Write(login);

            writer.Write(img.Length);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GetStatistics(Statistics stat,string login)
        {
            ServerCommand command = ServerCommand.Data;
            ServerDataCommand TypeCommand = ServerDataCommand.GetStatistics;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);


            writer.Write((int)command);
            writer.Write((int)TypeCommand);

            writer.Write(login);
            writer.Write(stat.NumGame);
            writer.Write(stat.WinGame);
            writer.Write(stat.LeaveGame);
            writer.Write(stat.NumAnswers);
            writer.Write(stat.CorrectAnswers);


            writer.Write(stat.Name);
            writer.Write(stat.SurName);



            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }
}
    

