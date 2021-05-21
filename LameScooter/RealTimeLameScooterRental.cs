using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter
{
    public class RealTimeLameScooterRental : ILameScooterRental
    {
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            HttpClient client = new HttpClient();
            string url = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";

            var getString = await client.GetStringAsync(url);
            var file = JsonSerializer.Deserialize<LameScooterStationList>(getString);
            
            foreach (var station in file.stations)
            {
                if (stationName == station.name)
                {
                    Print.WritelineWithColor($"{station.name}, available scooters:", ConsoleColor.Cyan);
                    return station.bikesAvailable;
                }
            }
            throw new Exception();
        }
    }
}