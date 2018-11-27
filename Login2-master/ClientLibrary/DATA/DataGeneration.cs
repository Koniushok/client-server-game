using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;
using System.IO;

namespace Client
{
    /// <summary>
    /// Класс для генерации команды серверу.
    /// </summary>
    public class DataGeneration
    {
        public MessageCommand message = new MessageCommand();
        public LoginCommand login = new LoginCommand();
        public GameServerCommand gameServer = new GameServerCommand();
        public GameCommmand game = new GameCommmand();
        public DataCommand data = new DataCommand();

    }

    public class MessageCommand
    {

        public byte[] MessageAll(string mes)
        {
            ClientCommand TypeCommand = ClientCommand.Message;
            ClientMessageCommand command = ClientMessageCommand.MessageAll;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(mes);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }

    public class LoginCommand
    {
        public byte[] Registation(string login, string password, string name, string surname)
        {
            ClientCommand TypeCommand = ClientCommand.Login;
            ClientLoginCommand command = ClientLoginCommand.Registation;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);


            writer.Write(login);
            writer.Write(password);
            writer.Write(name);
            writer.Write(surname);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] Authoriztion(string login, string password)
        {

            ClientCommand TypeCommand = ClientCommand.Login;
            ClientLoginCommand command = ClientLoginCommand.Authoriztion;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(login);
            writer.Write(password);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] Exit()
        {

            ClientCommand TypeCommand = ClientCommand.Login;
            ClientLoginCommand command = ClientLoginCommand.Exit;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        
    }

    public class GameServerCommand
    {
        public byte[] QuickGame()
        {

            ClientCommand TypeCommand = ClientCommand.Game;
            ClientGamesCommand command = ClientGamesCommand.QuickGame;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }

    public class GameCommmand
    {
        public byte[] ChooseCentralResult(int ter)
        {
            GameCommandClient command = GameCommandClient.ChooseCentral;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(ter);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] ChooseResult(int ter)
        {
            GameCommandClient command = GameCommandClient.Choose;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(ter);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }

        public byte[] AnswerResult(int answer)
        {
            GameCommandClient command = GameCommandClient.Answer;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)command);

            writer.Write(answer);

            byte[] buffer = stream.GetBuffer();
            return buffer;

        }
    }

    public class DataCommand
    {
        public byte[] NewImage(int length)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.Image;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(length);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] UpData(bool PasswordTest,string password, string name, string surname)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.UpData;
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(PasswordTest);
            if(PasswordTest)
            writer.Write(password);

            writer.Write(name);
            writer.Write(surname);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GetImage()
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.GetImage;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GetImageClient(string login)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.GetImageClient;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);


            
            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(login);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] GetStatistics(string login)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.GetStatistics;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(login);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public byte[] Report(ReporType type,string message)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.Report;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write((int)type);
            writer.Write(message);

            byte[] buffer = stream.GetBuffer();
            return buffer;
        }


        public byte[] RepTask(int k,int numTask)
        {
            ClientCommand TypeCommand = ClientCommand.Data;
            ClientDataCommand command = ClientDataCommand.RepTask;

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write((int)TypeCommand);
            writer.Write((int)command);

            writer.Write(k);
            writer.Write(numTask);


            byte[] buffer = stream.GetBuffer();
            return buffer;
        }
    }
    




}
