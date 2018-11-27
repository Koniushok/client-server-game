using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Server
    {
        #region Свойства
        /// <summary>
        /// Поток получения данных от сервера.
        /// </summary>
        Thread ThreadReceive { get; set; }
        /// <summary>
        /// Порт сервера.
        /// </summary>
        int Port { set; get; }
        /// <summary>
        /// Сокет сервера.
        /// </summary>
        public Socket Socket { get; set; }
        /// <summary>
        /// Конечная точка(ip сервера).
        /// </summary>
        IPEndPoint IpEndPoint { get; set; }
        /// <summary>
        /// Размер буфера.
        /// </summary>
        int LengthBuffer { get; set; }
        #endregion

        #region Конструкторы
        public Server()
        {
            ThreadReceive = new Thread(Receive);
            Port = 3333;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IpEndPoint = new IPEndPoint(ipAddress, Port);
        }

        public Server(int port,string ip)
        {
            ThreadReceive = new Thread(Receive);
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(ip);
            IpEndPoint = new IPEndPoint(ipAddress, port);
        }

        public Server(int port, IPAddress ipAddress)
        {
            ThreadReceive = new Thread(Receive);
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(ipAddress, port);
        }
        #endregion

        #region Методы 
        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        public bool connect()
        {
            try
            {             
                Socket.Connect(IpEndPoint);
                ThreadReceive.Start();
                OccasionHandler.Processing(Occasion.Server,ServerOccacion.Connect,null);
                return true;
            }
            catch(Exception e)
            {
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.ConnectException, null);
                return false;
            }
        }

        /// <summary>
        /// Отключение от сервера.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                Socket.Close();
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.Disconnect, null);
                MainClient.Disconnect();
                ThreadReceive.Abort();
                
            }
            catch (Exception e)
            {
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.DisconnectException, null);
            }

        }

        /// <summary>
        /// Отправка команды серверу.
        /// </summary>
        /// <param name="command">Команда</param>
        public void Send(byte[] command)
        {
            try
            {
                int bytesSent = Socket.Send(command);
                OccasionHandler.Processing(Occasion.Server, ServerOccacion.Send, null);
            }
            catch (Exception e)
            {

                OccasionHandler.Processing(Occasion.Server, ServerOccacion.SendException, null);
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
                    OccasionHandler.Processing(Occasion.Server, ServerOccacion.Receive, null);
                    MainClient.HandlerData.Processing(this,reader);                   
                }
                catch
                {
                    OccasionHandler.Processing(Occasion.Server, ServerOccacion.ReceiveException, null);
                    this.Disconnect();
                    return;
                }
            }
        }
        #endregion
    }
}
