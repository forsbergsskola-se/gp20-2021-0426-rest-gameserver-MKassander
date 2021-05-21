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
            if (argument.Length > 1)
            {
                if (argument[1] == "offline")
                {
                    var offlineRental = new OfflineLameScooterRental();
                    Print.WritelineWithColor("Using offlineRental...", ConsoleColor.Blue);
                    return await offlineRental.GetScooterCountInStation(argument[0]);

                }
                if (argument[1] == "deprecated")
                {
                    var deprecatedRental = new DeprecatedLameScooterRental();
                    Print.WritelineWithColor("Using deprecatedRental...", ConsoleColor.Blue);
                    return await deprecatedRental.GetScooterCountInStation(argument[0]);
                }
                if (argument[1] == "realtime")
                {
                    var realtimeRental = new RealTimeLameScooterRental();
                    Print.WritelineWithColor("Using realtimeRental...", ConsoleColor.Blue);
                    return await realtimeRental.GetScooterCountInStation(argument[0]);
                }
            }
            else
            {
                Print.WritelineWithColor("No second argument for method entered!", ConsoleColor.Blue);
                Print.WritelineWithColor("Using offlineRental...", ConsoleColor.Blue);
                var offlineRental = new OfflineLameScooterRental();
                return await offlineRental.GetScooterCountInStation(argument[0]);
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
