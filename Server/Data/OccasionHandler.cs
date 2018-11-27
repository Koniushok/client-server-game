using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Command;

namespace Server
{
    static class OccasionHandler
    {
        #region класс событий
        /// <summary>
        /// Обработчик события от полученных данных.
        /// </summary>
        static HandlerDataEvent sendData = new HandlerDataEvent();

        /// <summary>
        /// Обработчик серверных событий. 
        /// </summary>
        static ServerEvent serverEvent = new ServerEvent();

        /// <summary>
        /// Обработчик клиентских событий. 
        /// </summary>
        static ClientEvent clientEvent = new ClientEvent();

        /// <summary>
        /// Обработка событий основного сервера.
        /// </summary>
        static MainServerEvent mainServerEvent = new MainServerEvent();

        static ClientGameEvent clientGameEvent = new ClientGameEvent();

        static GameServerEvent gameServerEvent = new GameServerEvent();

        static GameEvent gameEvent = new GameEvent();
        #endregion

        static public object blockObject = new object();
        static public bool print = true;

        public static ConsoleColor ErrorColor=ConsoleColor.Red;
        public static ConsoleColor ClientEventColor = ConsoleColor.Blue;
        public static ConsoleColor ServerEventColor = ConsoleColor.Cyan;
        public static ConsoleColor SendColor = ConsoleColor.Green;
        public static ConsoleColor SQLColor = ConsoleColor.Yellow;
        public static ConsoleColor GameServerColor = ConsoleColor.Magenta;
        public static ConsoleColor GameSendColor = ConsoleColor.Gray;
        public static ConsoleColor GameColor = ConsoleColor.DarkMagenta;

        public static void Processing(Occasion occasin, object typeOccasion, params object[] arg)
        {
            //Console.WriteLine("{0}-------{1}", occasin, typeOccasion);
            switch (occasin)
            {
                case Occasion.HandlerData:
                    {
                        sendData.Processing((HandlerDataOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.Server:
                    {
                        serverEvent.Processing((ServerOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.Client:
                    {
                        clientEvent.Processing((ClientOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.MainServer:
                    {
                        mainServerEvent.Processing((MainServerOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.ClientGame:
                    {
                        clientGameEvent.Processing((ClientGameOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.GameServer:
                    {
                        gameServerEvent.Processing((GameServerOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.Game:
                    {
                        gameEvent.Processing((GameOccacion)typeOccasion, arg);
                        break;
                    }

            }
        }

        public static void Error(Exception ex)
        {
            if (print)
                lock (blockObject)
                {
                    
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ErrorColor;
                    Console.WriteLine(new string('-',80));
                    Console.WriteLine("ERROR:");
                    Console.WriteLine("Messaga: {0}", ex.Message);
                    Console.WriteLine("Source: {0}", ex.Source);
                    Console.WriteLine("TargetSite: {0}", ex.TargetSite);
                    Console.WriteLine("HelpLink: {0}", ex.HelpLink);
                    Console.WriteLine(new string('-', 80));
                    Console.ForegroundColor = color;
                }
        }

        public static void ClientEvent(Client client, string messages)
        {
            if (print)
                lock (blockObject)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ClientEventColor;
                    Console.WriteLine(new string('-', 30));
                   
                    Console.WriteLine("ClientEvent:");
                    Console.WriteLine("Event:{0}",messages);
                    if(client.Profile!=null)
                    Console.WriteLine("Login:{0}",client.Profile.Login);
                    Console.WriteLine("IP-Port:{0}", client.Socket.RemoteEndPoint);

                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void ServerEvent(Server server, string messages)
        {
            if (print)
                lock (blockObject)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ServerEventColor;
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine("ServerEvent:");
                    Console.WriteLine(messages);
                    Console.WriteLine("Port:{0}", server.Port);
                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void Send(byte[] buffer, Client client)
        {
            if (print)
                lock (blockObject)
                {
                   
                    if (buffer.Length < 1024)
                    {
                        MemoryStream stream = new MemoryStream(buffer);
                        BinaryReader reader = new BinaryReader(stream);
                        ServerCommand command = (ServerCommand)reader.ReadInt32();
                        object typeCommand = null;
                        switch (command)
                        {
                            case ServerCommand.Game:
                                {
                                    typeCommand = (ServerGameComman)reader.ReadInt32();
                                    break;
                                }
                            case ServerCommand.Login:
                                {
                                    typeCommand = (ServerLoginCommand)reader.ReadInt32();
                                    break;
                                }
                            case ServerCommand.Data:
                                {
                                    typeCommand = (ServerDataCommand)reader.ReadInt32();
                                    break;
                                }
                        }
                        ConsoleColor color = Console.ForegroundColor;
                        Console.ForegroundColor = SendColor;
                        Console.WriteLine(new string('-', 30));


                        Console.WriteLine("Send:");
                        Console.WriteLine("Отправление команды пользователю");
                        Console.WriteLine("длинна={0}", buffer.Length);
                        Console.WriteLine("Тип команды:{0}", command);
                        Console.WriteLine("команда:{0}", typeCommand);

                        Console.WriteLine(new string('-', 30));

                        Console.ForegroundColor = color;
                       
                    }
                    else
                    {

                        ConsoleColor color = Console.ForegroundColor;
                        Console.ForegroundColor = SendColor;
                        Console.WriteLine(new string('-', 30));


                        Console.WriteLine("Send:");
                        Console.WriteLine("Отправление большого пакета данных пользователю:");
                        Console.WriteLine("длинна={0}", buffer.Length);

                     
                        Console.WriteLine(new string('-', 30));

                        Console.ForegroundColor = color;
                       
                    }
                }
        }

        public static void SQL(string messages)
        {
            if (print)
                lock (blockObject)
                {
                    ConsoleColor color = Console.ForegroundColor;

                    Console.ForegroundColor = SQLColor;
                    Console.WriteLine(new string('-', 30));

                    Console.WriteLine("SQL EVENT:");
                    Console.WriteLine(messages);

                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void DataGeneration()
        {

        }

        public static void GameServerEvent(GameServer server, string messages)
        {
            if (print)
                lock (blockObject)
                {
                    ConsoleColor color = Console.ForegroundColor;

                    Console.ForegroundColor = GameServerColor;
                    Console.WriteLine(new string('-', 30));

                    Console.WriteLine("GameServer EVENT:");
                    Console.WriteLine(messages);
                    Console.WriteLine("Port:{0}", server.Port);

                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void GameSend(byte[] buffer, ClientGame client)
        {
            if (print)
                lock (blockObject)
                {
                    MemoryStream stream = new MemoryStream(buffer);
                    BinaryReader reader = new BinaryReader(stream);
                    GameCommanServer command = (GameCommanServer)reader.ReadInt32();
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = GameSendColor;
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine("Отправление команды ИГРОКУ:");
                    Console.WriteLine("длинна={0}", buffer.Length);
                    Console.WriteLine("Команды:{0}", command);
                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void Game(GameServer server, string messages)
        {
            if (print)
                lock (blockObject)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = GameColor;
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine("GameEvent:");
                    Console.WriteLine(messages);
                    Console.WriteLine("Port:{0}", server.Port);
                    Console.WriteLine(new string('-', 30));
                    Console.ForegroundColor = color;
                }
        }

        public static void Write(ConsoleColor color, int k)
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            ConsoleColor colorBackg = Console.BackgroundColor;
            Console.BackgroundColor = color;
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine("1", 80);
            }
            Console.BackgroundColor = colorBackg;
            Console.SetCursorPosition(left, top);
        }

    }

    /// <summary>
    /// События после получения данных.
    /// </summary>
    class HandlerDataEvent
    {
        public void Processing(HandlerDataOccacion occacin, params object[] arg)
        {
            switch (occacin)
            {
                case HandlerDataOccacion.MessageAll:
                    {
                        MessageAll((string)arg[0]);
                        break;
                    }
            }

        }

        static void MessageAll(string mes)
        {
            Console.WriteLine("получено сообщения для всем пользователей\n{0}", mes);
        }
    }

    /// <summary>
    /// Серверные события.
    /// </summary>
    class ServerEvent
    {
        public void Processing(ServerOccacion occacion, params object[] arg)
        {

            switch (occacion)
            {
                case ServerOccacion.ClientAccept:
                    {
                        break;
                    }
                case ServerOccacion.ClientAcceptException:
                    {
                        break;
                    }

                case ServerOccacion.NewClient:
                    {
                        break;
                    }

                case ServerOccacion.RemoveClient:
                    {
                        break;
                    }

                case ServerOccacion.SendToAll:
                    {
                        break;
                    }
                case ServerOccacion.SendToClient:
                    {
                        break;
                    }

                case ServerOccacion.Start:
                    {
                        break;
                    }
                case ServerOccacion.StartException:
                    {
                        break;
                    }
                case ServerOccacion.Stop:
                    {
                        break;
                    }
            }

        }
    }

    /// <summary>
    /// События клиента.
    /// </summary>
    class ClientEvent
    {
        public void Processing(ClientOccacion occacion, params object[] arg)
        {

            switch (occacion)
            {
                case ClientOccacion.Disconnect:
                    {

                        break;
                    }
                case ClientOccacion.DisconnectException:
                    {
                        break;
                    }
                case ClientOccacion.ProcessingData:
                    {
                        break;
                    }
                case ClientOccacion.Receive:
                    {
                        break;
                    }
                case ClientOccacion.ReceiveException:
                    {
                        break;
                    }
                case ClientOccacion.Send:
                    {
                        break;
                    }
                case ClientOccacion.SendException:
                    {
                        break;
                    }
            }

        }
    }

    class MainServerEvent
    {
        public void Processing(MainServerOccacion occacion, params object[] arg)
        {

            switch (occacion)
            {
                case MainServerOccacion.SendToAll:
                    {
                        break;
                    }
                case MainServerOccacion.SendToAllException:
                    {
                        break;
                    }
                case MainServerOccacion.SendToClient:
                    {
                        break;
                    }
                case MainServerOccacion.SendToClientException:
                    {
                        break;
                    }
                case MainServerOccacion.Start:
                    {
                        break;
                    }
            }

        }
    }

    class ClientGameEvent
    {
        public void Processing(ClientGameOccacion occacion, params object[] arg)
        {
            switch (occacion)
            {
                case ClientGameOccacion.Disconnect:
                    {
                        break;
                    }
                case ClientGameOccacion.ProcessingData:
                    {
                        break;
                    }
                case ClientGameOccacion.Receive:
                    {
                        break;
                    }
                case ClientGameOccacion.Send:
                    {
                        break;
                    }
                case ClientGameOccacion.Start:
                    {
                        break;
                    }
                case ClientGameOccacion.DisconnectException:
                    {
                        break;
                    }
                case ClientGameOccacion.ReceiveException:
                    {
                        break;
                    }
                case ClientGameOccacion.SendException:
                    {
                        break;
                    }
                case ClientGameOccacion.StartException:
                    {
                        break;
                    }

            }

        }
    }

    class GameEvent
    {
        public void Processing(GameOccacion occacion, params object[] arg)
        {
            switch (occacion)
            {
                case GameOccacion.Battle:
                    {
                        break;
                    }
                case GameOccacion.End:
                    {
                        break;
                    }

                case GameOccacion.EndGameCheck:
                    {
                        break;
                    }

                case GameOccacion.NextStep:
                    {
                        break;
                    }

                case GameOccacion.Reset:
                    {
                        break;
                    }

                case GameOccacion.ResultBattle:
                    {
                        break;
                    }

                case GameOccacion.ResultSuperBattle:
                    {
                        break;
                    }

                case GameOccacion.ResultTask:
                    {
                        break;
                    }

                case GameOccacion.Start:
                    {
                        break;
                    }

                case GameOccacion.StartBattle:
                    {
                        break;
                    }
                case GameOccacion.SuperBattle:
                    {
                        break;
                    }

            }

        }
    }

    class GameServerEvent
    {
        public void Processing(GameServerOccacion occacion, params object[] arg)
        {
            switch (occacion)
            {
                case GameServerOccacion.Authentication:
                    {
                        break;
                    }
                case GameServerOccacion.ClientAccept:
                    {
                        break;
                    }
                case GameServerOccacion.ConnectPlayer:
                    {
                        break;
                    }
                case GameServerOccacion.NewPlayer:
                    {
                        break;
                    }
                case GameServerOccacion.PlayerAccept:
                    {
                        break;
                    }
                case GameServerOccacion.PlayerDisconnect:
                    {
                        break;
                    }
                case GameServerOccacion.Start:
                    {
                        break;
                    }
                case GameServerOccacion.Stop:
                    {
                        break;
                    }
                case GameServerOccacion.ClientAcceptException:
                    {
                        break;
                    }
                case GameServerOccacion.PlayerAcceptException:
                    {
                        break;
                    }
                case GameServerOccacion.StartException:
                    {
                        break;
                    }
                case GameServerOccacion.StopException:
                    {
                        break;
                    }


            }

        }
    }

}
