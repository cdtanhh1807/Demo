using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Transport
    {
        private string iDTransport = "";
        private string seat = "";
        private string type = "";
        private string brand = "";
        private int yearOfPurchase;
        private string km = "";
        private bool insurance;
        private string price = "";
        private string status = "";
        private string cCCDOwner = "";

        public string Seat { get => seat; set => seat = value; }
        public string Type { get => type; set => type = value; }
        public string Brand { get => brand; set => brand = value; }
        public int YearOfPurchase { get => yearOfPurchase; set => yearOfPurchase = value; }
        public string Km { get => km; set => km = value; }
        public bool Insurance { get => insurance; set => insurance = value; }
        public string Price { get => price; set => price = value; }
        public string Status { get => status; set => status = value; }
        public string IDTransport { get => iDTransport; set => iDTransport = value; }
        public string CCCDOwner { get => cCCDOwner; set => cCCDOwner = value; }

        public Transport(string iDTransport, string seat, string type, string brand, int yearOfPurchase, string km, bool insurance, string price, string status, string cCCDOwner)
        {
            IDTransport = iDTransport;
            Seat = seat;
            Type = type;
            Brand = brand;
            YearOfPurchase = yearOfPurchase;
            Km = km;
            Insurance = insurance;
            Price = price;
            Status = status;
            CCCDOwner = cCCDOwner;
        }

        public Transport() { }

        public void AddStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}
