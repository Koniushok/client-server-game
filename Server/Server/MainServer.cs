using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;

namespace Server
{
    static class MainServer
    {
        #region Свойства 
        public static Server Server { get; set; }

        static bool Status { get; set; }

        public static DataGeneration GenerationData { get; set; }

        /// <summary>
        /// Класс для обработки полученных данных
        /// </summary>
        public static HandlerDATA HandlerData { get; set; }
        #endregion
        public static bool Start()
        {
            if (!Status)
            {
                GenerationData = new DataGeneration();
                HandlerData = new HandlerDATA();
                Server = new Server();
                Status = Server.Start();
                OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.Start, null);
            }
            return Status;
        }

        public static bool Start(Server server)
        {
            if (!Status)
            {
                GenerationData = new DataGeneration();
                HandlerData = new HandlerDATA();
                Status = Server.Start();
                OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.Start, null);
            }
            return Status;
        }

        //public static void SendToClient(Client client, ServerCommand command, params object[] arg)
        //{
        //    if (Status)
        //    {
        //        byte[] buffer = GenerationData.Generation(command, arg);
        //        Server.SendToClient(client, buffer);
        //        OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.SendToClient, null);
        //    }
        //    else
        //    {
        //        OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.SendToClientException, null);
        //    }
        //}

        //public static void SendToAll(Client Notclient, ServerCommand command, params object[] arg)
        //{
        //    if (Status)
        //    {
        //        byte[] buffer = GenerationData.Generation(command, arg);
        //        Server.SendToAll(Notclient, buffer);
        //        OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.SendToAll, null);
        //    }
        //    else
        //    {
        //        OccasionHandler.Processing(Occasion.MainServer, MainServerOccacion.SendToAllException, null);
        //    }
        //}

        public static void StopToServer()
        {
            if (Server != null)
            {
                Server.Stop();
                Server = null;
                Status = false;
            }
        }

        public static void Stop()
        {
            
            Server = null;
            Status = false;
        }
    }
}
