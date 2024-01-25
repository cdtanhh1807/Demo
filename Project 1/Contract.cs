using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Contract
    {
        private string cCCDOwner = "";
        private string cCCDClient = "";
        private string cCCDDriver = "";
        private string iDTransport = "";
        private DateTime rentalDate;
        private int rentalDays;
        private string deposit = "";
        private string iDContract = "";

        public string CCCDOwner { get => cCCDOwner; set => cCCDOwner = value; }
        public string CCCDClient { get => cCCDClient; set => cCCDClient = value; }
        public string CCCDDriver { get => cCCDDriver; set => cCCDDriver = value; }
        public string IDTransport { get => iDTransport; set => iDTransport = value; }
        public DateTime RentalDate { get => rentalDate; set => rentalDate = value; }
        public int RentalDays { get => rentalDays; set => rentalDays = value; }
        public string Deposit { get => deposit; set => deposit = value; }
        public string IDContract { get => iDContract; set => iDContract = value; }

        public Contract(string cCCDOwner, string cCCDClient, string cCCDDriver, string iDTransport, DateTime rentalDate, int rentalDays, string deposit, string iDContract)
        {
            CCCDOwner = cCCDOwner;
            CCCDClient = cCCDClient;
            CCCDDriver = cCCDDriver;
            IDTransport = iDTransport;
            RentalDate = rentalDate;
            RentalDays = rentalDays;
            Deposit = deposit;
            IDContract = iDContract;
        }

        public Contract() { }
    }
}
