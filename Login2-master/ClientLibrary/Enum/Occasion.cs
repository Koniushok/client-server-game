using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum Occasion
    {
        /// <summary>
        /// События вызванное при обработке полученных данных 
        /// </summary>
        HandlerData,
        /// <summary>
        /// События вызванные при работе с сервером.
        /// </summary>
        Server,
        /// <summary>
        /// События вызванные при работе с основным клиентом.
        /// </summary>
        MainClient,
        DataGeneration,
        GameServerOccacion,
        MainPlayerOccacion,
    }
    enum DataGenerationOccacion
    {
        Ganeration,
    }
    enum HandlerDataOccacion
    {
    }

    enum ServerOccacion
    {
        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        Connect,
        /// <summary>
        /// Ошибка при подключении к серверу.
        /// </summary>
        ConnectException,
        /// <summary>
        /// Отключение от сервера.
        /// </summary>
        Disconnect,
        /// <summary>
        /// Ошибка при отключении от сервера.
        /// </summary>
        DisconnectException,
        /// <summary>
        /// Оправка данных серверу.
        /// </summary>
        Send,
        /// <summary>
        /// Ошибка при отправлении данных серверу.
        /// </summary>
        SendException,
        /// <summary>
        /// Получение данных.
        /// </summary>
        Receive,
        /// <summary>
        /// Ошибка при получении данных.
        /// </summary>
        ReceiveException,
    }
    enum MainClientOccacion
    {
        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        Start,
        /// <summary>
        /// Отправка данных к серверу.
        /// </summary>
        Send,
        /// <summary>
        /// Ошибка при отправке данных.
        /// </summary>
        SendException
    }



    enum GameServerOccacion
    {
        SendException,
        Send,
        Receive,
        ReceiveException,
        Connect,
        ConncetException,
        Authorization,
        StartGame,
        Disconnect,
        DisconnectException,
    }
    enum MainPlayerOccacion
    {
        Start,
        Send,
        DisconnectToServer,
        Disconnect,
    }
}
