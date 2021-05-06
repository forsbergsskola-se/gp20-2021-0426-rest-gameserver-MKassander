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
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("GitHubExplorer", "1.0");
                
                Console.WriteLine("Enter username");
                var input = Console.ReadLine();
                var baseUrl = "https//api.github.com/users/";
                var requestUrl = baseUrl + input;
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                client.Send(requestMessage);


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
