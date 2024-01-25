using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Driver : Person
    {
        public Driver(string name, string cCCD, string phone, string address, float vote, string comment) : base(name, cCCD, phone, address, vote, comment) { }

        public Driver(string name, string cCCD, string phone, string address) : base(name, cCCD, phone, address) { }

        public Driver() { }
    }
}
