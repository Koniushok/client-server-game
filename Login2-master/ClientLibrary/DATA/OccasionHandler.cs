using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Games;

namespace Client
{
   
    public static class GameOccasionHandler
    {
        public static event Action ExitGameEvent;
        public static void ExitGame()
        {
            if (ExitGameEvent != null)
            {
                ExitGameEvent.Invoke();
            }
        }
    }

    public static class GameServerOccasionHandler
    {
        public static event Action StartGameEvent;
        public static void StartGame()
        {
            if (StartGameEvent != null)
            {
                StartGameEvent.Invoke();
            }
        }

        public static event Action StopGameEvent;
        public static void  StopGame()
        {

            if (StopGameEvent != null)
            {
                StopGameEvent.Invoke();
            }
        }
    }

    public static class DataOccasionHandler
    {
      
        public static event Action<byte[]> GetImageEvent;
        public static void GetImage(byte[] img)
        {
            if (GetImageEvent != null)
            {
                GetImageEvent.Invoke(img);
            }
        }

        public static event Action<byte[],string> GetImageClientEvent;
        public static void GetImageClient(byte[] img,string login)
        {
            if (GetImageClientEvent != null)
            {
                GetImageClientEvent.Invoke(img,login);
            }
        }

        public static event Action<Statistics, string> GetStatisticsEvent;
        public static void GetStatistics(Statistics stat, string login)
        {
            if (GetStatisticsEvent != null)
            {
                GetStatisticsEvent.Invoke(stat, login);
            }
        }
        
    }

    public static class OccasionHandler
    {
        #region классы событий
        /// <summary>
        /// Обработчик события от полученных данных.
        /// </summary>
        static HandlerDataEvent sendData = new HandlerDataEvent();

        static ServerEvent serverEvent = new ServerEvent();

        static MainClientEvent mainClientEvent = new MainClientEvent();

        static DataGenerationEvent datagenarationEvent = new DataGenerationEvent();

        static GameServerEvent gameServerEvent = new GameServerEvent();

        static MainPlayerEvent mainPlayerEvent = new MainPlayerEvent();
        #endregion

        public static event Action<bool, string> RegistationEvent;
        public static void Registation(bool test,string message)
        {
            if (RegistationEvent != null)
            {
                RegistationEvent.Invoke(test, message);
            }
        }

        public static event Action<bool, string> AuthoriztionEvent;
        public static void Authoriztion(bool test, string message)
        {
            if (AuthoriztionEvent != null)
            {
                AuthoriztionEvent.Invoke(test, message);
            }
        }



        public static void Processing(Occasion occasin, object typeOccasion, params object[] arg)
        {
            
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
                case Occasion.MainClient:
                    {
                        mainClientEvent.Processing((MainClientOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.DataGeneration:
                    {
                        datagenarationEvent.Processing((DataGenerationOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.GameServerOccacion:
                    {
                        gameServerEvent.Processing((GameServerOccacion)typeOccasion, arg);
                        break;
                    }
                case Occasion.MainPlayerOccacion:
                    {
                        mainPlayerEvent.Processing((MainPlayerOccacion)typeOccasion, arg);
                        break;
                    }

            }
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
            }
        }
    }

    class ServerEvent
    {
        public void Processing(ServerOccacion occacin, params object[] arg)
        {

            switch (occacin)
            {
                case ServerOccacion.Connect:
                    {
                        break;
                    }
                case ServerOccacion.ConnectException:
                    {
                        break;
                    }
                case ServerOccacion.Disconnect:
                    {
                        break;
                    }
                case ServerOccacion.DisconnectException:
                    {
                        break;
                    }
                case ServerOccacion.Receive:
                    {
                        break;
                    }
                case ServerOccacion.ReceiveException:
                    {
                        break;
                    }
                case ServerOccacion.Send:
                    {
                        break;
                    }
                case ServerOccacion.SendException:
                    {
                        break;
                    }
            }
        }
    }

    class MainClientEvent
    {
        public void Processing(MainClientOccacion occacin, params object[] arg)
        {

            switch (occacin)
            {
                case MainClientOccacion.Send:
                    {
                        break;
                    }
                case MainClientOccacion.SendException:
                    {
                        break;
                    }
                case MainClientOccacion.Start:
                    {
                        break;
                    }
            }
        }
    }

    class DataGenerationEvent
    {
        public void Processing(DataGenerationOccacion occacin, params object[] arg)
        {

            switch (occacin)
            {
                case DataGenerationOccacion.Ganeration:
                    {
                        Console.WriteLine("Размер генерируемого буфера = {0}",arg[0]);
                        break;
                    }
            }
        }
    }

    class GameServerEvent
    {
        public void Processing(GameServerOccacion occacin, params object[] arg)
        {

            switch (occacin)
            {
                case GameServerOccacion.Authorization:
                    {
                        break;
                    }
                case GameServerOccacion.ConncetException:
                    {
                        break;
                    }
                case GameServerOccacion.Connect:
                    {
                        break;
                    }
                case GameServerOccacion.Disconnect:
                    {
                        break;
                    }
                case GameServerOccacion.DisconnectException:
                    {
                        break;

                    }
                case GameServerOccacion.Receive:
                    {
                        break;
                    }
                case GameServerOccacion.ReceiveException:
                    {
                        break;
                    }
                case GameServerOccacion.Send:
                    {
                        break;
                    }
                case GameServerOccacion.SendException:
                    {
                        break;
                    }
                case GameServerOccacion.StartGame:
                    {
                        break;
                    }
            }
        }
    }

    class MainPlayerEvent
    {
        public void Processing(MainPlayerOccacion occacin, params object[] arg)
        {

            switch (occacin)
            {
                case MainPlayerOccacion.Disconnect:
                    {
                        break;
                    }
                case MainPlayerOccacion.DisconnectToServer:
                    {
                        break;
                    }
                case MainPlayerOccacion.Send:
                    {
                        break;

                    }
                case MainPlayerOccacion.Start:
                    {
                        break;
                    }
            }
        }
    }
}
