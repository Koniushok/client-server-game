using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Threading;

namespace Client
{
    public class GameServer: DispatcherObject
    {
        #region Свойства
        public Game Game { get; set; }
        /// <summary>
        /// Поток получения данных от игрового сервера.
        /// </summary>
        Thread ThreadReceive { get; set; }
        /// <summary>
        /// Порт игрового сервера.
        /// </summary>
        int Port { set; get; }
        /// <summary>
        /// Сокет игрового сервера.
        /// </summary>
        public Socket Socket { get; set; }
        /// <summary>
        /// Конечная точка(ip сервера).
        /// </summary>
        IPEndPoint IpEndPoint { get; set; }
        /// <summary>
        /// Размер буфера.
        /// </summary>
        int LengthBuffer { get; set; } = 1024;
        /// <summary>
        /// Состояния игрового сервера.
        /// </summary>
        bool Status { get; set; }
        #endregion

        #region Конструкторы

        public GameServer(int port, string ip)
        {
            Game = new Game();
            ThreadReceive = new Thread(Receive);
            ThreadReceive.IsBackground = true;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(ip);
            IpEndPoint = new IPEndPoint(ipAddress, port);
        }

        public GameServer(int port, IPAddress ipAddress)
        {
            Game = new Game();
            ThreadReceive = new Thread(Receive);
            ThreadReceive.IsBackground = true;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(ipAddress, port);
        }
        #endregion     

        /// <summary>
        /// Отправка команды серверу.
        /// </summary>
        /// <param name="command">Команда</param>
        public void Send(byte[] command)
        {
            try
            {
                int bytesSent = Socket.Send(command);
                OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.Send);
            }
            catch (Exception e)
            {
                OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.SendException);
                this.Disconnect();
            }
        }

        /// <summary>
        /// Ожидает получения данных от сервера.
        /// </summary>
        void Receive()
        {
            while (true)
            {
                MemoryStream stream = new MemoryStream(new byte[LengthBuffer], 0, LengthBuffer, true, true);
                BinaryReader reader = new BinaryReader(stream);
                try
                {
                    int bytesRec = Socket.Receive(stream.GetBuffer());
                    MainClient.HandlerData.Processing(this, reader);
                    OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.Receive);
                }
                catch
                {
                    OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.ReceiveException);
                    this.Disconnect();
                    return;
                }
            }
        }

        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        public bool connect(string password)
        {
            if (!Status)
            {
                try
                {

                    Socket.Connect(IpEndPoint);
                    ThreadReceive.Start();
                    Status = true;
                    Authorization(password);
                    OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.Connect);
                    return Status;
                }
                catch (Exception e)
                {
                    OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.ConncetException);
                    return Status;
                }
            }
            return !Status;
        }

        public void Authorization(string password)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            string login = MainClient.Profile.Login;
            writer.Write(login);
            writer.Write(password);
            Send(stream.GetBuffer());
            OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.Authorization);
        }

        /// <summary>
        /// Отключение от сервера.
        /// </summary>
        public void Disconnect()
        {
            if (Status)
            {
                try
                {
                    Socket.Close();
                    Status = false;
                    //Game = null;               
                    OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.Disconnect);
                    ThreadReceive.Abort();
                }
                catch (Exception e)
                {
                    // OccasionHandler.Processing(Occasion.GameServerOccacion, GameServerOccacion.DisconnectException);
                }
            }

        }


        #region ToServer
        public void ChooseCentralResult(int ter)
        {
            Send(MainClient.GenerationData.game.ChooseCentralResult(ter));
        }

        public void ChooseResult(int ter)
        {
            Send(MainClient.GenerationData.game.ChooseResult(ter));
        }

        public void AnswerResult(int answer)
        {
            Send(MainClient.GenerationData.game.AnswerResult(answer));
        }

        
        #endregion
    }
}
