using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server
{
    public class GameServer
    {
        #region Свойства
        Thread ThreadAccept { get; set; }

        string Password { get; set; }

        public int Port { get; set; }

        public Socket Socket { get; set; }

        public IPEndPoint IpEndPoint { get; set; }

        public Game NormalGame { get; set; }

        public bool StatusAccept { get; set; }

        int LengthBuffer { get; set; } = 1024;

        public int NumberOfPlayers { get; private set; } = 0;

        Thread ThreadPlayerAccet { get; set; }

        public bool Open { get; set; } = true;
        #endregion

        #region Конструктор
        public GameServer(int port, string password="0000")
        {
            ThreadAccept = new Thread(ClientAccept);
            ThreadPlayerAccet = new Thread(PlayerAccept);
            this.Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(IPAddress.Any, Port);

            if (password != "0000")
            {
                Password = password;
                Open = false;
            }

            NormalGame = new Game(this);

            OccasionHandler.GameServerEvent(this, "Создание нового сервера");
        }
        #endregion
        
        #region Методы

        public void NewPlayer(Client client,Socket socket)
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.NewPlayer);
            if (StatusAccept)
            {
                if (NormalGame.Player1 == null)
                {
                    NormalGame.Player1 = new ClientGame(client,socket,this);
                    OccasionHandler.GameServerEvent(this, "Player1 подключён");
                    NumberOfPlayers++;
                    EndAccept();
                }
                else if (NormalGame.Player2 == null)
                {
                    NormalGame.Player2 = new ClientGame(client,socket,this);
                    OccasionHandler.GameServerEvent(this, "Player2 подключён");
                    NumberOfPlayers++;
                    EndAccept();                 
                }

            }
        }

        void EndAccept()
        {
            if(NormalGame.Player1 == null)
            {
                OccasionHandler.GameServerEvent(this, "Player1=null");
            }
            if (NormalGame.Player2 == null)
            {
                OccasionHandler.GameServerEvent(this, "Player2=null");
            }
            if (NormalGame.Player1 != null && NormalGame.Player2 != null)
            {
                if (NormalGame.Player2.Client.Profile.Login == NormalGame.Player1.Client.Profile.Login)
                {
                    OccasionHandler.GameServerEvent(this, "Player1=Player2");
                    return;
                }

               
                StatusAccept = false;
                try
                {
                    MainServer.Server.StartGame(this);
                    OccasionHandler.GameServerEvent(this, "Сервер готов к запуску игры");
                    NormalGame.Start();
                    ThreadAccept.Abort();

                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.NewPlayerException);
                }
                
            }
        }

        public bool Start()
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.Start);
            try
            {

                if (!StatusAccept)
                {
                    Socket.Bind(IpEndPoint);
                    Socket.Listen(3);
                    ThreadAccept.Start();
                    MainServer.Server.StartGameServer(this);
                    StatusAccept = true;
                    //Thread.Sleep(1000);                  
                    OccasionHandler.GameServerEvent(this, "Игровой сервер запущен");
                }
                return StatusAccept;
            }
            catch
            {
                OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.StartException);
                return StatusAccept;
            }
        }

        public void Stop()
        {
            
            try
            {
                OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.Stop);
                NormalGame.Player1.Disconnect();
                NormalGame.Player2.Disconnect();
                Socket.Close();
                OccasionHandler.GameServerEvent(this, "Игровой сервер выключен");
                ThreadAccept.Abort();

            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
                OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.StopException);
            }          
        }

        bool Authentication(BinaryReader reader,Socket socket)
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.Authentication);

            string login = reader.ReadString();

            string gamePassword = reader.ReadString();
            OccasionHandler.GameServerEvent(this, "Авторизация пользователя");
            if (!this.Open)
            {
                if (gamePassword != Password)
                {
                    OccasionHandler.GameServerEvent(this, "Авторизация:Не верный пароль");
                    return false;
                }
            }

            Client client = MainServer.Server.SearchClient(login);

            if (client != null)
            {
                OccasionHandler.GameServerEvent(this, "Авторизация прошла успешно");
                NewPlayer(client,socket);
                return true;
            }
            OccasionHandler.GameServerEvent(this, "Авторизация не прошла");
            return false;
        }

        #region Переподключение 
        public void PlayerDisconnect(ClientGame player)
        {
            NumberOfPlayers--;
            if (!NormalGame.Status)
            {
                if (NormalGame.Player1 == player)
                {
                    NormalGame.Player1 = null;
                OccasionHandler.GameServerEvent(this, "Player1 был отключён");
                }
                if (NormalGame.Player2 == player)
                {
                    OccasionHandler.GameServerEvent(this, "Player2 был отключон");
                    NormalGame.Player2 = null;
                }
            }
            else
            {
                SQL.AddLeaveGame(player.Client.Profile.Login);
                NormalGame.Status = false;
                player.Player.Point = 0;
            }
            

            //if (player==NormalGame.Player1||player==NormalGame.Player2)
            //{
            //    if (!StatusAccept)
            //    {                 
            //        ThreadPlayerAccet.Start();
            //    }
            //    else
            //    {
            //        NormalGame.Player1 = null;
            //    }
            //}

            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.PlayerDisconnect);
        }

        void ConnectPlayer(Socket player,string login,string password)
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.ConnectPlayer);
            if (NormalGame.Player2.Status==false)
            {
                if(NormalGame.Player2.Client.Profile.Login == login)
                {
                    NormalGame.Player2.Start(player);
                    
                    try
                    {
                        ThreadPlayerAccet.Abort();
                    }catch
                    {
                       
                    }
                }
            }
            if (NormalGame.Player1.Status == false)
            {
                if (NormalGame.Player1.Client.Profile.Login == login)
                {
                    NormalGame.Player1.Start(player);
                 
                    try
                    {
                        ThreadPlayerAccet.Abort();
                    }
                    catch
                    {

                    }
                }
            }
        }

        void PlayerAccept()
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.PlayerAccept);
            while (true)
            {
                Socket player = Socket.Accept();
                MemoryStream stream = new MemoryStream(new Byte[LengthBuffer], 0, LengthBuffer, true, true);
                BinaryReader reader = new BinaryReader(stream);
                try
                {
                    int bytesRec = Socket.Receive(stream.GetBuffer());
                    string login = reader.ReadString();
                    string password = reader.ReadString();
                    ConnectPlayer(player, login,password);
                    player.Close();
                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.PlayerAcceptException);
                    player.Close();
                }
            }
        }
        #endregion

        void ClientAccept()
        {
            OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.ClientAccept);
            while (true)
            {
                Socket user=null;
                MemoryStream stream = new MemoryStream(new Byte[LengthBuffer], 0, LengthBuffer, true, true);
                BinaryReader reader = new BinaryReader(stream);
                bool chack=true;
                try
                {
                    OccasionHandler.GameServerEvent(this, "Ждём подключение клиента");
                    user = Socket.Accept();
                    OccasionHandler.GameServerEvent(this, "Клиент подключён");
                    try
                    {
                        int bytesRec = user.Receive(stream.GetBuffer());
                    }
                    catch (Exception e)
                    {
                        chack = false;
                        OccasionHandler.Error(e);
                        OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.ClientAcceptException);
                        user.Close();
                    }


                }
                catch (Exception e)
                {
                    chack = false;
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.GameServer, GameServerOccacion.ClientAcceptException);
                }

                if (chack)
                {
                    if (!Authentication(reader, user))
                    {
                        user.Close();
                        OccasionHandler.GameServerEvent(this, "Клиент отключён");
                    }
                }
            }
        }
        #endregion
    }
}
