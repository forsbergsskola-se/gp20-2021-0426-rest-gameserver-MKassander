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
            Print.WritelineWithColor(count.ToString(), ConsoleColor.Cyan);
        }

        static async Task<int> ReadArgsGetCount(string[] argument)
        {
            var deprecatedRental = new DeprecatedLameScooterRental();
            var offlineRental = new OfflineLameScooterRental();
            var realtimeRental = new RealTimeLameScooterRental();
            int count;
            if (argument.Length > 1)
            {
                if (argument[1] == "offline")
                {
                    Print.WritelineWithColor("Using offlineRental...", ConsoleColor.Blue);
                    count = await offlineRental.GetScooterCountInStation(argument[0]);
                    return count;

                }
                if (argument[1] == "deprecated")
                {
                    Print.WritelineWithColor("Using deprecatedRental...", ConsoleColor.Blue);
                    count = await deprecatedRental.GetScooterCountInStation(argument[0]);
                    return count;
                }
                if (argument[1] == "realtime")
                {
                    Print.WritelineWithColor("Using realtimeRental...", ConsoleColor.Blue);
                    count = await realtimeRental.GetScooterCountInStation(argument[0]);
                    return count;
                }
            }
            else
            {
                Console.WriteLine("else");
                count = await offlineRental.GetScooterCountInStation(argument[0]);
                return count;
            }
            throw new Exception();
        }
        
        
    }

    static class Print
    {
        public static void WritelineWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
