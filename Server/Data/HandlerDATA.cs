using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Command;

namespace Server
{


    class HandlerDATA
    {
        MessageHadler message = new MessageHadler();
        LoginHadler login = new LoginHadler();
        GameServerHadler gameServer = new GameServerHadler();
        DataHadler data = new DataHadler();

        public void Processing(Client client, BinaryReader reader)
        {

            ClientCommand command = (ClientCommand)reader.ReadInt32();

            #region Color
            if(OccasionHandler.print)
            lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Обработка команды от клиента:");
                Console.WriteLine("Тип команды:{0}", command);

                Console.ForegroundColor = color;
            }
            #endregion
            switch (command)
            {
                case ClientCommand.Message:
                    {
                        message.Processing(client, reader);
                        break;
                    }
                case ClientCommand.Login:
                    {
                        login.Processing(client, reader);
                        break;
                    }
                case ClientCommand.Game:
                    {
                        gameServer.Processing(client, reader);
                        break;
                    }
                case ClientCommand.Data:
                    {
                        data.Processing(client, reader);
                        break;
                    }
            }
            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new string('-', 60));
                Console.ForegroundColor = color;
            }
            #endregion
        }

        GameHandler game = new GameHandler();

        public void Processing(ClientGame player, BinaryReader reader)
        {
            game.Processing(player, reader);
        }

    }

    class GameHandler
    {

        BinaryReader Reader { get; set; }
        ClientGame Player { get; set; }
        public void Processing(ClientGame player, BinaryReader reader)
        {
            Reader = reader;
            Player = player;
            GameCommandClient command = (GameCommandClient)Reader.ReadInt32();

            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Обработка команды от игрока:");
                Console.WriteLine("Game команда:{0}", command);
                Console.ForegroundColor = color;
            }
            #endregion


            switch (command)
            {
                case GameCommandClient.ChooseCentral:
                    {
                        ChooseCentral();
                        break;
                    }
                case GameCommandClient.Choose:
                    {
                        Choose();
                        break;
                    }
                case GameCommandClient.Answer:
                    {
                        Answer();
                        break;
                    }
              
            }

            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(new string('-', 60));
                Console.ForegroundColor = color;
            }
            #endregion
        }

        void ChooseCentral()
        {
            int k = Reader.ReadInt32();
            Player.GameServer.NormalGame.PlayerSetCentral(Player, k);
        }

        void Choose()
        {
            int k = Reader.ReadInt32();
            Player.GameServer.NormalGame.PlayerSetTerritory(Player, k);
        }

        void Answer()
        {
            int k = Reader.ReadInt32();
            Player.GameServer.NormalGame.PlayerAnswer(Player, k);
        }
    }

    class MessageHadler
    {
        BinaryReader Reader { get; set; }
        public void Processing(Client client, BinaryReader reader)
        {
            Reader = reader;
            ClientMessageCommand command = (ClientMessageCommand)Reader.ReadInt32();

            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Команда:{0}", command);
                Console.ForegroundColor = color;
            }
            #endregion

            switch (command)
            {
                case ClientMessageCommand.MessageAll:
                    {
                        MessageAll();
                        break;
                    }
            }

        }

        void MessageAll()
        {
            OccasionHandler.Processing(Occasion.HandlerData, HandlerDataOccacion.MessageAll, Reader.ReadString());
        }
    }

    class LoginHadler
    {
        BinaryReader Reader { get; set; }
        Client client;
        public void Processing(Client client, BinaryReader reader)
        {
            Reader = reader;
            this.client = client;
            ClientLoginCommand command = (ClientLoginCommand)Reader.ReadInt32();
            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Команда:{0}", command);
                Console.ForegroundColor = color;
            }
            #endregion
            switch (command)
            {
                case ClientLoginCommand.Authoriztion:
                    {
                        Authoriztion();
                        break;
                    }
                case ClientLoginCommand.Registation:
                    {
                        Registation();
                        break;
                    }
                case ClientLoginCommand.Exit:
                    {
                        Exit();
                        break;
                    }
            }

        }

        void Authoriztion()
        {
            string login = Reader.ReadString();
            string password = Reader.ReadString();
            ClientProfile profile = SQL.Authorization(login, password);
            byte[] byffer = MainServer.GenerationData.login.Authoriztion(profile, client);
            client.Send(byffer);
        }

        void Registation()
        {
            string login = Reader.ReadString();
            string password = Reader.ReadString();
            string name = Reader.ReadString();
            string surname = Reader.ReadString();
            bool test = SQL.Registration(login, password, name, surname);
            if (test)
            {
                byte[] byffer = (MainServer.GenerationData.login.Registation(test, "Регистрация прошла успешно"));
                client.Send(byffer);
            }
            else
            {
                byte[] byffer = MainServer.GenerationData.login.Registation(test, "Пользователь с таким логином уже существует");
                client.Send(byffer);
            }
        }

        void Exit()
        {
            client.Profile = null;
        }
    }

    class GameServerHadler
    {
        BinaryReader Reader { get; set; }
        Client client;
        public void Processing(Client client, BinaryReader reader)
        {
            Reader = reader;
            this.client = client;
            ClientGamesCommand command = (ClientGamesCommand)Reader.ReadInt32();
            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Команда:{0}", command);
                Console.ForegroundColor = color;
            }
            #endregion
            switch (command)
            {
                case ClientGamesCommand.QuickGame:
                    {
                        QuickGame();
                        break;
                    }
            }
        }

        void QuickGame()
        {
            byte[] buffer = MainServer.GenerationData.serverGame.QuickServer(MainServer.Server.AnyServer());
            client.Send(buffer);
        }
    }

    class DataHadler
    {
        BinaryReader Reader { get; set; }
        Client Client { get; set; }
        public void Processing(Client client, BinaryReader reader)
        {
            Reader = reader;
            Client = client;
            ClientDataCommand command = (ClientDataCommand)Reader.ReadInt32();

            #region Color
            if (OccasionHandler.print)
                lock (OccasionHandler.blockObject)
            {
                ConsoleColor color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Команда:{0}", command);
                Console.ForegroundColor = color;
            }
            #endregion

            switch (command)
            {
                case ClientDataCommand.Image:
                    {
                        Image();
                        break;
                    }
                case ClientDataCommand.UpData:
                    {
                        UpData();
                        break;
                    }
                case ClientDataCommand.GetImage:
                    {
                        GetImage();
                        break;
                    }
                case ClientDataCommand.GetImageClient:
                    {
                        GetImageClient();
                        break;
                    }
                case ClientDataCommand.GetStatistics:
                    {
                        GetStatistics();
                        break;
                    }
                case ClientDataCommand.Report:
                    {
                        Report();
                        break;
                    }
                case ClientDataCommand.RepTask:
                    {
                        RepTask();
                        break;
                    }
            }

        }

        void Image()
        {
            try
            {
                int length = Reader.ReadInt32();
                Console.WriteLine("длинна {0}",length);

                Console.WriteLine("длинна {0}", length);
                Console.WriteLine("длинна {0}", length);
                Console.WriteLine("длинна {0}", length);
                byte[] img = new byte[length+ length];



                int imgLength = 0;
                while (imgLength < length)
                {
                    byte[] buffer = new byte[length+length];

                    // Console.WriteLine("Всё" + buffer.Length.ToString());
                    //Console.WriteLine("принимаю");

                    int bufferLength = Client.Socket.Receive(buffer);

                    buffer.Take(bufferLength).ToArray().CopyTo(img, imgLength);

                    //img.CopyTo(buffer.Take(bufferLength).ToArray(), imgLength);


                    imgLength += bufferLength;

                    // Console.WriteLine("bufferLength {0}", bufferLength);
                    // Console.WriteLine("imgLength {0}", imgLength);
                }
                //Console.WriteLine("Всё" + img.Length.ToString());

                SQL.AddImage(Client.Profile.Login, img);
            }
            catch
            {

            }
        }

        void UpData()
        {
            bool PasswordTest = Reader.ReadBoolean();
            string password = "";
            if (PasswordTest)
            {
                password = Reader.ReadString();
            }
            string name = Reader.ReadString();
            string surname= Reader.ReadString();

            SQL.UpData(Client.Profile.Login,password,name,surname, PasswordTest);
        }

        void GetImage()
        {
            byte[] img = SQL.GetImage(Client.Profile.Login);
            
            if (img != null)
            {
                Client.Send(MainServer.GenerationData.data.GetImage(img));
                Client.Send(img);
            }
           
        }

        void GetImageClient()
        {
            string login = Reader.ReadString();
            byte[] img = SQL.GetImage(login);

            if (img != null)
            {               
                Client.Send(MainServer.GenerationData.data.GetImageClient(img,login));
                Client.Send(img);
            }

        }

        void GetStatistics()
        {
            string login = Reader.ReadString();
            Client.Send(MainServer.GenerationData.data.GetStatistics(SQL.GetStatistics(login), login));
        }

        void Report()
        {
          
            ReporType type=(ReporType)Reader.ReadInt32();
            string message = Reader.ReadString();
            string login = Client.Profile.Login;
            SQL.AddRepor(type, message, login);
             
            ConsoleColor colorBackg=  Console.BackgroundColor;
            ConsoleColor colorForeg = Console.ForegroundColor;
            Console.WriteLine(new string('?',50));

            Console.WriteLine(new string('?', 50));
            Console.ForegroundColor = colorForeg;
            Console.BackgroundColor = colorBackg;
        }

        void RepTask()
        {
            int k = Reader.ReadInt32();
            int numTask = Reader.ReadInt32();
            Console.WriteLine("Пользователь оценил вопрос id:{0} оценка:{1} пользователь{2}",numTask,k,Client.Profile.Login);
           
        }
    }

}
