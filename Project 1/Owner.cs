using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Owner : Person
    {
        public Owner(string name, string cCCD, string phone, string address, float vote, string comment) : base(name, cCCD, phone, address, vote, comment) { }

        public Owner(string name, string cCCD, string phone, string address) : base(name, cCCD, phone, address) { }

        public Owner() { }
    }
}
