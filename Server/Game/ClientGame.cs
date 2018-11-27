using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using Games;
using Command;

namespace Server
{
    public class ClientGame
    {
        #region Свойства
        public Player Player { get; set; }
        public GameServer GameServer { get; set; }
        /// <summary>
        /// Поток для получения данных от игрока
        /// </summary>
        Thread ThreadReceive { get; set; }
        /// <summary>
        /// Длинная получаемого буфера.
        /// </summary>
        int LengthBuffer { get; set; } = 1024;
        /// <summary>
        /// Сокет игрока.
        /// </summary>
        Socket Socket { get; set; }
        /// <summary>
        /// Клиент игрока.
        /// </summary>
        public Client Client { get; set; }

        public bool Status { get; set; } = false;
        #endregion

        #region Конструкторы
        public ClientGame(Client client, Socket socket, GameServer gameServer)
        {
            Player = new Player();
            Client = client;
            ThreadReceive = new Thread(Receive);
            ThreadReceive.IsBackground = true;
            GameServer = gameServer;
            Start(socket);
        }
        #endregion

        #region Метода
        void Receive()
        {
            while (true)
            {
                MemoryStream stream = new MemoryStream(new byte[LengthBuffer], 0, LengthBuffer, true, true);
                BinaryReader reader = new BinaryReader(stream);
                try
                {
                    int bytesRec = Socket.Receive(stream.GetBuffer());
                    ProcessingData(reader);

                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    this.Disconnect();
                    return;
                }
            }
        }

        public void Start(Socket socket)
        {
            Socket = socket;
            Status = true;
            try
            {
                ThreadReceive.Start();
                OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.Start);
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
                OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.StartException);
            }
        }

        public void Disconnect()
        {
            if (Status)
            {
                try
                {
                    Socket.Close();
                    Status = false;
                    GameServer.PlayerDisconnect(this);
                    OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.Disconnect);
                    ThreadReceive.Abort();
                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.DisconnectException);
                }
            }

        }

        void ProcessingData(BinaryReader reader)
        {
            OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.ProcessingData);
            MainServer.HandlerData.Processing(this, reader);
        }

        public void Send(byte[] command)
        {
            if (Status)
            {
                try
                {
                    int bytesSent = Socket.Send(command);
                    OccasionHandler.GameSend(command, this);
                    OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.Send);
                }
                catch (Exception e)
                {
                    OccasionHandler.Error(e);
                    OccasionHandler.Processing(Occasion.ClientGame, ClientGameOccacion.StartException);
                    this.Disconnect();
                }
            }
        }


        #region Game

        public void StartGame(string login)
        {
           
            byte[] img = SQL.GetImage(login);
            if (img != null)
            {
                Client.Send(MainServer.GenerationData.data.GetImageClient(img, login));
                Client.Send(img);
            }
        }

        /// <summary>
        /// Выбор центральной территории.
        /// </summary>
        /// <param name="Territory">территория</param>
        public void ChooseCentral(bool[] Territory, int Maxtime)
        {
            byte[] buffer = MainServer.GenerationData.game.ChooseCentral(Territory, Maxtime);
            Send(buffer);
        }
        /// <summary>
        /// Выбор территории.
        /// </summary>
        /// <param name="Territory">Территория</param>
        public void Choose(bool[] Territory, int Maxtime)
        {
            Send(MainServer.GenerationData.game.Choose(Territory, Maxtime));
        }

        /// <summary>
        /// Результат выбора территории.
        /// </summary>
        public void ResresultChoose(ClientGame client, int attackTerritory, bool assign)
        {
            Send(MainServer.GenerationData.game.ResresultChoose(client, attackTerritory, assign));
        }

        /// <summary>
        /// Результат выбора центральной территории.
        /// </summary>
        public void ResresultChooseCetral(ClientGame client, int Territory)
        {
            Send(MainServer.GenerationData.game.ResultChooseCetral(client, Territory));
        }

        /// <summary>
        /// Обновления данных.
        /// </summary>
        /// <param name="territory">Территория</param>
        /// <param name="enemy">Противник</param>
        public void UpData(Territory[] Territories, ClientGame enemy)
        {
            Send(MainServer.GenerationData.game.UpData(Territories, this, enemy));
        }

        public void UpTerritory(Territory[] Territories)
        {
            MainServer.GenerationData.game.UpTerritory(Territories);
        }

        public void Battle(GameTask task, int AttackTerritory, int Maxtime)
        {
            Send(MainServer.GenerationData.game.Battle(task, AttackTerritory, Maxtime));
        }

        public void SuperBattle(GameTask task, int AttackTerritory, int Maxtime)
        {
            Send(MainServer.GenerationData.game.SuperBattle(task, AttackTerritory, Maxtime));
        }

        public void ResultBattle(ClientGame winner, ClientGame loser, GameTask task)
        {
            MainServer.GenerationData.game.ResultBattle(winner, loser, task);
        }

        public void ResultSuperBattle(ClientGame winner, ClientGame loser, GameTask task)
        {
            MainServer.GenerationData.game.ResultSuperBattle(winner, loser, task);
        }

        public void ChoosEnemy(int time)
        {
            Send(MainServer.GenerationData.game.ChoosEnemy(time));
        }

        public void ChoosEnemyCentral(int time)
        {
            Send(MainServer.GenerationData.game.ChoosEnemyCentral(time));
        }


        public void ResultTask(int my,int enemy)
        {
            Send(MainServer.GenerationData.game.ResultTask(my,enemy));
        }

        public void ResultTask(int my, int enemy,int num)
        {
            Send(MainServer.GenerationData.game.ResultTask(my, enemy,num));
        }

        public void ResulAnsver(int ansver,TimeSpan myTime, TimeSpan enemyTime)
        {
            Send(MainServer.GenerationData.game.ResulAnsver(ansver,myTime,enemyTime));
        }

       

        public void GameOver(bool win,ClientGame myPlayer, ClientGame enemyPlayer,int numNumStep, int NumAnswer, TimeSpan StartTime)
        {
            Send(MainServer.GenerationData.game.GameOver(win,myPlayer,enemyPlayer,numNumStep,NumAnswer,StartTime));
        }

        #endregion
        #endregion
    }
}
