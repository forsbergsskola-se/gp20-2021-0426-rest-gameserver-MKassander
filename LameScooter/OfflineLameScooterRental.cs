using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter
{
    public class OfflineLameScooterRental : ILameScooterRental
    {
        public string path = "scooters.json";

        public async Task<int> GetScooterCountInStation(string stationName)
        {
            using var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, path));
            var file = JsonSerializer.Deserialize<LameScooterStationList>(await sr.ReadToEndAsync());

            foreach (var station in file.stations)
            {
                if (stationName == station.name)
                {
                    Console.WriteLine($"{station.name}, available scooters:");
                    return station.bikesAvailable;
                }
            }
            var nf = new Exception(stationName + " is invalid");
            throw nf;
        }
    }
}