using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = Env.token; //Class Env need to be created with your token in string "token" 
            while (true)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", token);
                
                Console.WriteLine("Enter username");
                var input = Console.ReadLine();
                client.BaseAddress = new Uri("https://api.github.com/users/");// 
                //var requestUrl = baseUrl + input;
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, input);
                client.Send(requestMessage);
                
                Console.WriteLine("Sending...");
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                var stream = responseMessage.Content.ReadAsStream();

                Console.WriteLine("Received response...");
                StreamReader streamReader = new StreamReader(stream);
                var stringFromStream = streamReader.ReadToEnd();
                Console.WriteLine(stringFromStream);
                Console.WriteLine("End");

                client.Dispose();
            }
        }
    }
}
