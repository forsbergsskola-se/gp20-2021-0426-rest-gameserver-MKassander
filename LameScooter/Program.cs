using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter
{
    public interface ILameScooterRental
    {
        Task<int> GetScooterCountInStation(string stationName);
    }

    public class OfflineLameScooterRental: ILameScooterRental
    {
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            int result = 0;
            var file = await File.ReadAllTextAsync("scooters.json");
            var json = JsonSerializer.Deserialize<LameScooterStationList>(file);
            foreach (var station in json.ScooterStations)
                if (stationName == station.name)
                    result = station.bikesAvailable;

            return result;
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            OfflineLameScooterRental rental = new OfflineLameScooterRental();

            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine("Count: " + count);
        }
    }

    public class LameScooterStation
    {
        public string name { get; set; }
        public int bikesAvailable { get; set; }
    }
    
    public class LameScooterStationList
    {
        public List<LameScooterStation> ScooterStations;
    }
}
