using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public class Client
    {

        #region Поля
        /// <summary>
        /// Сервер к которому привязан клиент.
        /// </summary>
        Server Server { get; set; }
        /// <summary>
        /// Профиль клиента.
        /// </summary>
        public ClientProfile Profile { get; set; }
        /// <summary>
        /// Статус авторизации.
        /// </summary>
        public bool Authorization { get; set; }
        /// <summary>
        /// Сокет клиента.
        /// </summary>
        public Socket Socket { get; set; }
        /// <summary>
        /// Поток для получения данных.
        /// </summary>
        Thread ThreadReceive { get; set; }

        /// <summary>
        /// Длинна получаемого буфера.
        /// </summary>
        int LengthBuffer { get; set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создание класса клиент.
        /// </summary>
        /// <param name="Socket">Сокет клиент</param>
        /// <param name="Server">Сервер подключения</param>
        public Client(Socket Socket, Server Server)
        {
            this.Authorization = false;
            LengthBuffer = 1024;
            this.Server = Server;
            this.Profile = null;
            this.Socket = Socket;
            this.ThreadReceive = new Thread(Receive);
            ThreadReceive.IsBackground = true;
            this.ThreadReceive.Start();

            OccasionHandler.ClientEvent(this, "Создание клиента");
        }
        #endregion

        #region Методы
        /// <summary>
        /// Ожидает получения данных ок клиента.
        /// </summary>
        void Receive()
        {
            while (true)
            {
                MemoryStream stream = new MemoryStream(new Byte[LengthBuffer], 0, LengthBuffer, true, true);
                BinaryReader reader = new BinaryReader(stream);
               try
                {
                    int bytesRec = Socket.Receive(stream.GetBuffer());
                    OccasionHandler.Processing(Occasion.Client, ClientOccacion.Receive, null);
                    OccasionHandler.ClientEvent(this, "Получено соманда от клиента");
                    ProcessingData(reader);

                }
               catch (Exception e)
                {
                  OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.Client, ClientOccacion.ReceiveException, null);
                  this.Disconnect();
                  return;
                }

            }
        }


        /// <summary>
        /// Обработка полученных данных от клиента.
        /// </summary>
        /// <param name="Data">Данные</param>
        void ProcessingData(BinaryReader reader)
        {
            OccasionHandler.Processing(Occasion.Client, ClientOccacion.ProcessingData, null);
            OccasionHandler.ClientEvent(this, "Обработка команды клиента");
            MainServer.HandlerData.Processing(this, reader);
        }

        /// <summary>
        /// Отключение клиента от сервера.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                OccasionHandler.ClientEvent(this, "Отключение клиента");
                this.Server.RemoveClient(this);
                Socket.Close();
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.Disconnect, null);
                ThreadReceive.Abort();

            }
            catch (Exception e)
            {
               
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.DisconnectException, null);
            }

        }

        /// <summary>
        /// Отключения клиента с передачей причины.
        /// </summary>
        /// <param name="cause">Причина</param>
        public void Disconnect(string cause)
        {
            try
            {
                Socket.Close();
                OccasionHandler.ClientEvent(this, "Отключение клиента по причине:"+cause);
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.Disconnect, null);
                ThreadReceive.Abort();
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.DisconnectException, null);
            }
        }

        /// <summary>
        /// Отправка команды клиенту.
        /// </summary>
        /// <param name="command">Команда</param>
        public void Send(byte[] command)
        {          
            try
            {
                int bytesSent = Socket.Send(command);
                OccasionHandler.Send(command, this);
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.Send, null);
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
                OccasionHandler.Processing(Occasion.Client, ClientOccacion.SendException, null);
                this.Disconnect();
            }
        }
        #endregion
    }
}
