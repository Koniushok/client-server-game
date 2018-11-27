using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Command;

namespace Server
{
    public class Server
    {
        #region Свойства

        int MinGamePort { get; set; } = 4000;
        int MaxGamePort { get; set; } = 5000;
        int NextGamePort { get; set; } = 4000;

        /// <summary>
        /// Поток для ожидания клиента.
        /// </summary>
        Thread ThreadAccept { get; set; }
        /// <summary>
        /// Пор сервера.
        /// </summary>
        public int Port { set; get; }
        /// <summary>
        /// Сокет сервера.
        /// </summary>
        public Socket Socket { get; set; }
        /// <summary>
        /// Конечная точка(ip сервера).
        /// </summary>
        public IPEndPoint IpEndPoint { get; set; }
        /// <summary>
        /// Максимальное число клиентов на сервере.
        /// </summary>
        int MaxClient { get; set; }
        /// <summary>
        /// Все подключенные клиент.
        /// </summary>
        public List<Client> Clients = new List<Client>();

        public List<GameServer> GameServersStart = new List<GameServer>();

        public List<GameServer> GameServersPending = new List<GameServer>();

        #endregion

        #region Конструкторы
        public Server()
        {
            ThreadAccept = new Thread(ClientAccept);
            Port = 3333;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(IPAddress.Any, Port);
            MaxClient = 100;
        }

        public Server(int Port)
        {
            ThreadAccept = new Thread(ClientAccept);
            this.Port = Port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(IPAddress.Any, Port);
            MaxClient = 100;
        }

        public Server(IPEndPoint IpEndPoint, Socket Socket)
        {
            ThreadAccept = new Thread(ClientAccept);
            this.IpEndPoint = IpEndPoint;
            this.Socket = Socket;
            Port = IpEndPoint.Port;
            MaxClient = 100;
        }

        public Server(IPEndPoint IpEndPoint)
        {
            ThreadAccept = new Thread(ClientAccept);
            this.IpEndPoint = IpEndPoint;
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Port = IpEndPoint.Port;
            MaxClient = 100;
        }

        public Server(IPEndPoint IpEndPoint, Socket Socket, int MaxClient)
        {
            ThreadAccept = new Thread(ClientAccept);
            this.IpEndPoint = IpEndPoint;
            this.Socket = Socket;
            this.Port = IpEndPoint.Port;
            this.MaxClient = MaxClient;
        }

        #endregion

        #region Методы
        /// <summary>
        /// Запуск сервера.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            try
            {
                Socket.Bind(IpEndPoint);
                Socket.Listen(MaxClient);
                ThreadAccept.Start();
                OccasionHandler.ServerEvent(this, "Сервер запущен");
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.Start, null);
                return true;
            }
            catch
            {
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.StartException, null);
                return false;
            }
        }

        /// <summary>
        /// Остановка сервера.
        /// </summary>
        public void Stop()
        {
            foreach (Client client in Clients)
            {
                client.Disconnect(" ");
            }
            Socket.Close();
            MainServer.Stop();
            OccasionHandler.ServerEvent(this, "Сервер отключен");
            OccasionHandler.Processing(Occasion.Server, ServerOccacion.Stop, null);
            try
            {
                ThreadAccept.Abort();
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
            }
        }

        /// <summary>
        /// Отключение клиент от сервера.
        /// </summary>
        /// <param name="clien">Клиент</param>
        public void RemoveClient(Client clien)
        {
            Clients.Remove(clien);
            OccasionHandler.ServerEvent(this, "Удаление клиента");
            OccasionHandler.Processing(Occasion.Server, ServerOccacion.RemoveClient, null);
        }

        /// <summary>
        /// Принудительное отключение от сервера 
        /// </summary>
        /// <param name="clien">Клиент</param>
        /// <param name="cause">Причина отключения</param>
        public void RemoveClient(Client clien, string cause)
        {
            Clients.Remove(clien);
            clien.Disconnect(cause);
            OccasionHandler.ServerEvent(this, "Удаление клиента.\nПричина:" + cause);
            OccasionHandler.Processing(Occasion.Server, ServerOccacion.RemoveClient, null);
        }

        /// <summary>
        /// Ожидание подключения клиента.
        /// </summary>
        void ClientAccept()
        {
            while (true)
            {
                try
                {
                    Socket user = Socket.Accept();
                    OccasionHandler.ServerEvent(this, "Пользователь подключился");
                    OccasionHandler.Processing(Occasion.Server, ServerOccacion.ClientAccept, null);
                    NewClient(user);

                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.Server, ServerOccacion.ClientAcceptException, null);
                }
            }
        }

        /// <summary>
        /// Подключения нового клиента.
        /// </summary>
        /// <param name="socket"></param>
        void NewClient(Socket socket)
        {
            Client client = new Client(socket, this);
            Clients.Add(client);
            OccasionHandler.ServerEvent(this, "Создание нового клиента");
            OccasionHandler.Processing(Occasion.Server, ServerOccacion.NewClient, null);

        }
        /// <summary>
        /// Передача данных одному клиенту.
        /// </summary>
        /// <param name="client">Клиент которому передаются данные.</param>
        /// <param name="type">Тип команды</param>
        /// <param name="arg">Дополнительные параметры</param>
        public void SendToClient(Client client, byte[] buffer)
        {
            client.Send(buffer);

            OccasionHandler.Processing(Occasion.Server, ServerOccacion.SendToClient, null);
        }

        /// <summary>
        /// Передача дынных всем клиентам кроме NotClient.
        /// </summary>
        /// <param name="Notclient">Клиент которому не передаются данные.</param>
        /// <param name="type">Тип передаваемой команды.</param>
        /// <param name="arg">Дополнительные параметры.</param>
        public void SendToAll(Client Notclient, byte[] buffer)
        {
            foreach (Client client in Clients)
            {
                if (client != Notclient && client.Authorization == true)
                {
                    client.Send(buffer);
                }
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.SendToAll, null);
            }
        }

        /// <summary>
        /// Поиск пользователя по авторизованным пользователям.
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Client SearchClient(string login)
        {
            foreach (Client client in Clients)
            {
                if (client.Profile != null)
                {
                    if (client.Profile.Login == login)
                    {
                        return client;
                    }
                }
            }
            return null;
        }
        #endregion

        #region Методы Game

        public GameServer NewGameServer(string password = "0000")
        {
            OccasionHandler.ServerEvent(this, "Создание нового game сервера");
            bool check = false;
            while (!check)
            {
                check = true;
                foreach (GameServer Ser in GameServersStart)
                {
                    if (Ser.Port == NextGamePort)
                    {
                        check = false;
                        NextGamePort++;
                        break;
                    }
                }

                foreach (GameServer Ser in GameServersPending)
                {
                    if (Ser.Port == NextGamePort)
                    {
                        check = false;
                        NextGamePort++;
                        break;
                    }
                }
            }
            GameServer server = new GameServer(NextGamePort, password);

            NextGamePort++;
            if (server.Start())
            {
                return server;
            }
            else
            {
                return null;
            }
        }

        public GameServer AnyServer()
        {
            OccasionHandler.ServerEvent(this, "Поиск любого game сервера");
            foreach (GameServer item in GameServersPending)
            {
                if (item.NumberOfPlayers != 2)
                {
                    return item;
                }
            }
            return NewGameServer();
        }

        public void RremoveGameServer(GameServer game)
        {
            OccasionHandler.ServerEvent(this, "Удаление game сервера");
            GameServersStart.Remove(game);
        }

        public void RremoveGameServerPending(GameServer game)
        {
            try
            {
                OccasionHandler.ServerEvent(this, "Удаление game сервера из списка ожидаемых");
                GameServersPending.Remove(game);
            }
            catch (Exception ex)
            {
                OccasionHandler.Error(ex);
            }
        }

        public void StartGameServer(GameServer game)
        {
            OccasionHandler.ServerEvent(this, "Запуск game севера");
            GameServersPending.Add(game);
        }

        public void StartGame(GameServer game)
        {
            try
            {
                OccasionHandler.ServerEvent(this, "Запуск игры на game сервере");
                RremoveGameServerPending(game);
                GameServersStart.Add(game);
            }
            catch (Exception ex)
            {
                OccasionHandler.Error(ex);
            }
        }

        public void StopGame(GameServer game)
        {
              GameServersStart.Remove(game);           
        }
        #endregion
    }
}
