using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rental = new OfflineLameScooterRental();

            if (args[0].Any(char.IsDigit))
                throw  new ArgumentException("Invalid Argument");
            
            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine(count);
        }
    }
}
