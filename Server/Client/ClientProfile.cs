using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    

    public class ClientProfile
    {
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public ClientProfile(string login,string name,string surname)
        {
            Login = login;
            Name = name;
            Surname = surname;
        }
    }
}
