using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;

namespace Client
{
    public static class MainPlayer
    {
        #region Свойства
        
        static GameServer GameServer { get; set; }

        //public static bool Authorization { get; set; }

        public static bool Status { get; set; }
        #endregion


        static public bool Start(string password)
        {
            if (!Status)
            {
                Status = GameServer.connect(password);
                OccasionHandler.Processing(Occasion.MainPlayerOccacion, MainPlayerOccacion.Start);
            }
            return Status;
        }

        static public bool Start(GameServer server,string password)
        {
            if (!Status)
            {
                
                GameServer = server;
                Status = GameServer.connect(password);
                OccasionHandler.Processing(Occasion.MainPlayerOccacion, MainPlayerOccacion.Start);
            }
            return Status;
        }

        //public static void Send(ClientGameCommand command,params object[] arg)
        //{
        //    if (Status)
        //    {
        //        byte[] buffer = MainClient.GenerationData.Generation(command,arg);
        //        GameServer.Send(buffer);
        //        OccasionHandler.Processing(Occasion.MainPlayerOccacion, MainPlayerOccacion.Send);
        //    }
        //    else
        //    {
        //    }
        //}

        public static void DisconnectToServer()
        {
            GameServer.Disconnect();
            GameServer = null;
            Status = false;
            OccasionHandler.Processing(Occasion.MainPlayerOccacion, MainPlayerOccacion.DisconnectToServer);
        }

        public static void Disconnect()
        {
            OccasionHandler.Processing(Occasion.MainPlayerOccacion, MainPlayerOccacion.Disconnect);
            GameServer = null;
            Status = false;
            //Authorization = false;
        }

    }
}
