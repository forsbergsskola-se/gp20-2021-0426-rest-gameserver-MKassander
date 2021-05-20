using System;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rental = new OfflineLameScooterRental();

            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine(count);
        }
    }
}
