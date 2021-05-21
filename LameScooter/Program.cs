using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args[0].Any(char.IsDigit))
                throw  new ArgumentException("Invalid Argument");

            var count = await ReadArgsGetCount(args);
            Console.WriteLine(count);
        }

        static async Task<int> ReadArgsGetCount(string[] argument)
        {
            var deprecatedRental = new DeprecatedLameScooterRental();
            var offlineRental = new OfflineLameScooterRental();
            int count;
            if (argument[0].Contains(" "))
            {
                if (argument[0].Substring(argument[0].IndexOf(" ") +1 ) == "offline")
                {
                    count = await offlineRental.GetScooterCountInStation
                        (argument[0].Substring(0,argument[0].IndexOf(" ")));
                    return count;

                }
                if (argument[0].Substring(argument[0].IndexOf(" ") +1 ) == "deprecated")
                {
                    count = await deprecatedRental.GetScooterCountInStation
                        (argument[0].Substring(0,argument[0].IndexOf(" ")));
                    return count;
                }
            }
            else
            {
                count = await offlineRental.GetScooterCountInStation(argument[0]);
                return count;
            }
            throw new Exception();
        }
    }
}
