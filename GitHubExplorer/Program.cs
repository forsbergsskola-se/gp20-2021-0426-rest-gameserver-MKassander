using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    public class UserResponse
    {
        
    }
    class Program
    {
        static string separatorString = "*******";
        static void Main(string[] args)
        {
            while (true)
            {
                HttpClient client = new HttpClient();
                var token = Env.token; //Class Env need to be created with your token in string "token" 
                
                client.BaseAddress = new Uri("https://api.github.com/users/");
                client.DefaultRequestHeaders.UserAgent.Add
                    (new ProductInfoHeaderValue("GithubExplorer", "1.0"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("token", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                Console.WriteLine("Enter username");
                var input = Console.ReadLine();
                
                var task = ToAndFromGit(input, client);
                task.Wait();
                
                client.Dispose();
            }
        }

        static async Task ToAndFromGit(string input, HttpClient client)
        {
            var response = await client.GetAsync(input);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            var streamReader = new StreamReader(stream);
            var responseString = await streamReader.ReadToEndAsync();

            var userResponse = JsonSerializer.Deserialize<UserResponse>(responseString);
            //var user = new UserResponse(userResponse);

            Separator();
            Console.WriteLine(response);
            Separator();
            Console.WriteLine(responseString);
            Separator();
        }

        static void Separator()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 3; i++)
                Console.WriteLine(separatorString);
            Console.ResetColor();
        }
    }
}
