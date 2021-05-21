using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LameScooter
{
    class DeprecatedLameScooterRental : ILameScooterRental
    {
        private string path = "scooters.txt";

        public async Task<int> GetScooterCountInStation(string stationName)
        {
            var file = await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, path));
            var stations = new List<LameScooterStation>();
            var split = file.Split("\n", StringSplitOptions.TrimEntries);

            for (var i = 0; i < split.Length - 1; i++)
            {
                var keyValueText = split[i].Split(':', StringSplitOptions.TrimEntries);
                var newStation = new LameScooterStation();
                newStation.name = keyValueText[0];
                newStation.bikesAvailable = int.Parse(keyValueText[1]);
                stations.Add(newStation);
            }

            foreach (var station in stations)
            {
                if (stationName == station.name)
                {
                    Console.WriteLine($"{station.name}, available scooters:");
                    return station.bikesAvailable;
                }
            }
            var notFoundException = new Exception("Not found: " + stationName);
            throw notFoundException;
        }
    }
}