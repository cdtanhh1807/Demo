using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_1
{
    public enum eGift
    {
        SummerGift,
        HolidayGift,
        Christmas,
        None
    }

    public enum eSincurred
    {
        AccessiBlewheels,
        CarScratches,
        BrokenLight,
        None
    }

    internal class ReturnTransport
    {
        static string CleanString(string input)
        {
            string cleanedString = new string(input.Where(char.IsDigit).ToArray());
            return cleanedString;
        }

        static long SplitStringToLong(string input)
        {
            string cleanString = CleanString(input);

            long result = long.Parse(cleanString);
            return result;
        }

        public static int IsDateLate(Contract contract)
        {
            DateTime date = DateTime.Now;
            int day = (date - contract.RentalDate).Days;
            return day > contract.RentalDays ? day : 0;
        }

        static eGift PreGift(string value)
        {
            switch (value)
            {
                case "SummerGift": return eGift.SummerGift;
                case "HolidayGift": return eGift.HolidayGift;
                case "Chrismas": return eGift.Christmas;
                default: return eGift.None;
            }
        }

        static eSincurred PreSincurred(string value)
        {
            switch (value)
            {
                case null:
                    return eSincurred.None;
                case "AccessiBlewheels":
                    return eSincurred.AccessiBlewheels;
                case "CarScratches":
                    return eSincurred.CarScratches;
                case "BrokenLight":
                    return eSincurred.BrokenLight;
                default: return eSincurred.None;
            }
        }

        public static long PreBill(Contract contract, Transport transport, string temp, string value)
        {
            eGift costGift = PreGift(temp);
            long howCostGift;
            eSincurred costSincurred = PreSincurred(value);
            long howcostSincurred;

            switch (costGift)
            {
                case eGift.SummerGift:
                    howCostGift = Gift.SummerGift(100000);
                    break;
                case eGift.HolidayGift:
                    howCostGift = Gift.HolidayGift(200000);
                    break;
                case eGift.Christmas:
                    howCostGift = Gift.Christmas(500000);
                    break;
                default:
                    howCostGift = 0;
                    break;
            }

            switch (costSincurred)
            {
                case eSincurred.AccessiBlewheels:
                    howcostSincurred = CostSincurred.AccessiBlewheels(1000000);
                    break;
                case eSincurred.CarScratches:
                    howcostSincurred = CostSincurred.CarScratches(500000);
                    break;
                case eSincurred.BrokenLight:
                    howcostSincurred = CostSincurred.BrokenLight(400000);
                    break;
                default:
                    howcostSincurred = 0;
                    break;
            }

            long moneyDayLate = CostSincurred.Daylate(100000, IsDateLate(contract));
            long PerAll = SplitStringToLong(transport.Price)*contract.RentalDays + howCostGift + howcostSincurred + moneyDayLate - SplitStringToLong(contract.Deposit);
            return PerAll;
        }
    }
}
