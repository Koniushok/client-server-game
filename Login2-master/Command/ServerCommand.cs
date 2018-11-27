using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public enum ServerCommand
    {
        Login,
        Game,
        Data,
    }

    public enum ServerLoginCommand
    {
        Registation,
        Authoriztion,
    }

    public enum ServerGameComman
    {
        QuickGame,
    }

    public enum ServerDataCommand
    {
        GetImage,
        GetImageClient,
        GetStatistics,
    }
}
