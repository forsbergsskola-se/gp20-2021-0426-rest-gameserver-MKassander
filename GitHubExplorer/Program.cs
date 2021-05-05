using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace GitHubExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                
                
                
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 10000);
                HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Add();

                

                HttpResponseMessage responseMessage = new HttpResponseMessage();
                var stream = responseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);
                var stringFromStream = streamReader.ReadToEnd();
                Console.WriteLine(stringFromStream);

                client.Dispose();
            }
        }
    }
}
