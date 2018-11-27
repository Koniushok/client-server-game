using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   

    enum Occasion
    {
        /// <summary>
        /// Обработка полученного сообщения всем клиентам сервера.
        /// </summary>
        HandlerData,
        Server,
        Client,
        MainServer,

        ClientGame,
        Game,
        GameServer,
    }

    enum MainServerOccacion
    {
        Start,
        SendToClient,
        SendToClientException,
        SendToAll,
        SendToAllException,

    }
    enum HandlerDataOccacion
    {
        MessageAll,

    }

    enum ServerOccacion
    {
        Start,
        StartException,
        Stop,
        RemoveClient,
        ClientAccept,
        ClientAcceptException,
        SendToClient,
        SendToAll,
        NewClient,
    }
    enum ClientOccacion
    {
        Receive,
        ReceiveException,
        ProcessingData,
        Disconnect,
        DisconnectException,
        Send,
        SendException,
    }

    #region Game
    enum ClientGameOccacion
    {
        Receive,
        ReceiveException,
        Start,
        StartException,
        Disconnect,
        DisconnectException,
        ProcessingData,
        Send,
        SendException,

    }

    /// <summary>
    /// не доработан!!!!!!
    /// </summary>
    enum GameOccacion
    {
        StartBattle,
        SuperBattle,
        Battle,
        ResultTask,
        ResultBattle,
        ResultSuperBattle,
        NextStep,
        Reset,
        EndGameCheck,
        End,
        Start,
    }

    enum GameServerOccacion
    {
        NewPlayer,
        Start,
        Stop,
        Authentication,
        PlayerDisconnect,
        ConnectPlayer,
        PlayerAccept,
        ClientAccept,
        StartException,
        StopException,
        PlayerAcceptException,
        ClientAcceptException,
        NewPlayerException,
    }
    #endregion



}
