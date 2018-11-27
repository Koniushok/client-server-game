using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Command;
using System.Windows;
using System.Windows.Threading;

namespace Client
{
    public delegate void Event();
    public static class MainClient
    {
        #region Свойства 

        public static GameServer GameServer { get; set; }

        public static event Event ServerOff;

        public static Server Server { get; set; }

        public static ClientProfile Profile { get; set; }

        public static bool Status { get; set; }

        static bool Authorization { get; set; }

        public static DataGeneration GenerationData { get; set; }

        /// <summary>
        /// Класс для обработки полученных данных
        /// </summary>
        public static HandlerDATA HandlerData { get; set; }
        #endregion

        #region Методы
        static public bool Start()
        {
            if (!Status)
            {
                HandlerData = new HandlerDATA();
                GenerationData = new DataGeneration();
                Server = new Server();
                Status = Server.connect();
                OccasionHandler.Processing(Occasion.MainClient, MainClientOccacion.Start, null);
            }
            return Status;
        }

        static public bool Start(Server server)
        {
            if (!Status)
            {
                HandlerData = new HandlerDATA();
                GenerationData = new DataGeneration();
                Server = server;
                Status = Server.connect();
                OccasionHandler.Processing(Occasion.MainClient, MainClientOccacion.Start, null);
            }
            return Status;
        }

        //public static void Send(ClientCommand command, object TypeCommand, params object[] arg)
        //{
        //    if (Status)
        //    {
        //        byte[] buffer = GenerationData.Generation(command, TypeCommand, arg);
        //        OccasionHandler.Processing(Occasion.MainClient, MainClientOccacion.Send, arg);
        //        Server.Send(buffer);
        //    }
        //    else
        //    {
        //        OccasionHandler.Processing(Occasion.MainClient, MainClientOccacion.SendException, null);
        //    }
        //}

        public static void DisconnectToServer()
        {
            if(Server!=null)
                Server.Disconnect();                                 
        }

        public static void Disconnect()
        {
            if (Status)
            {
                if (ServerOff != null)
                    ServerOff.Invoke();
                Server = null;
                Status = false;                
            }

            if (GameServer != null)
            {
                GameServer.Disconnect();
                GameServer = null;
            }
        }

        public static bool Registration(string login, string password, string name, string surname)
        {
            if (Status)
            {
                byte[] buffer=GenerationData.login.Registation(login,password,name,surname);
                Server.Send(buffer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Authoriztion(string login, string password)
        {
            if (Status)
            {
                byte[] buffer = GenerationData.login.Authoriztion(login,password);
                Server.Send(buffer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void QuickGame()
        {
            byte[] buffer = GenerationData.gameServer.QuickGame();
            Server.Send(buffer);
        }

        public static void NewImage(byte[] image)
        {
            Server.Send(GenerationData.data.NewImage(image.Length));
            Server.Send(image);
        }

        public static void UpData(bool PasswordTest,string password, string name, string surname)
        {
            Server.Send(GenerationData.data.UpData(PasswordTest,password, name, surname));
        }

        public static void GetImage()
        {
            Server.Send(GenerationData.data.GetImage());
        }

        public static void GetImageClient(string login)
        {
            Server.Send(GenerationData.data.GetImageClient(login));
        }

        public static void Exit()
        {
            Profile = null;
            Server.Send(GenerationData.login.Exit());
        }

        public static void GetStatistics(string login)
        {
            Server.Send(GenerationData.data.GetStatistics(login));
        }

        public static void Report(ReporType type,string message)
        {
            Server.Send(GenerationData.data.Report(type,message));
        }

        public static void RepTask(int k,int numTask)
        {
            Server.Send(GenerationData.data.RepTask(k,numTask));
        }
        
        #endregion
    }
}
