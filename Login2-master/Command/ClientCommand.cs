using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public enum ClientCommand
    {
        /// <summary>
        /// Отправка сообщение всем клиентам сервера.
        /// </summary>
        Message,
        Login,
        Game,
        Data,
    }

    public enum ClientMessageCommand
    {
        MessageAll,
    }

    public enum ClientLoginCommand
    {
        Registation,
        Authoriztion,
        Exit,
    }

    public enum ClientGamesCommand
    {
        QuickGame,
    }

    public enum ClientDataCommand
    {
        Image,
        UpData,
        GetImage,
        GetImageClient,
        GetStatistics,
        Report,
        RepTask,

    }

}
