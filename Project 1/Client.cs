using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_1
{
    public class Client : Person
    {
        public Client(string name, string cCCD, string phone, string address, float vote, string comment) : base(name, cCCD, phone, address, vote, comment) { }

        public Client(string name, string cCCD, string phone, string address) : base(name, cCCD, phone, address) { }

        public Client() { }
    }
}
