using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Person
    {
        private string name = "";
        private string cCCD = "";
        private string phone = "";
        private string address = "";
        private float vote;
        private string comment = "";

        public string Name { get => name; set => name = value; }
        public string CCCD { get => cCCD; set => cCCD = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Comment { get => comment; set => comment = value; }
        public float Vote { get => vote; set => vote = value; }

        public Person(string name, string cCCD, string phone, string address, float vote, string comment)
        {
            Name = name;
            CCCD = cCCD;
            Phone = phone;
            Address = address;
            Vote = vote;
            Comment = comment;
        }

        public Person(string name, string cCCD, string phone, string address)
        {
            Name = name;
            CCCD = cCCD;
            Phone = phone;
            Address = address;
        }

        public Person() { }

        public void AddEvaluate(string vote, string comment)
        {
            Vote = (Vote + float.Parse(vote)) / 2;
            Comment = comment;
        }
    }
}
