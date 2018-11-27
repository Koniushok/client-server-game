using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProfile
    {
        public string Login { get;  set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public ClientProfile(string login,string name,string surName)
        {
            Login = login;
            Name = name;
            SurName = surName;
        }
    }
}
