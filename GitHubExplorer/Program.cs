using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    public class UserResponse
    {
        public string name { get; set; }
        public string company { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string bio { get; set; }
        public int public_repos { get; set; }
        public int private_Repos { get; set; }

        public void PrintFields()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Company: " + company);
            Console.WriteLine("Bio: " + bio);
            Console.WriteLine("Location: " + location);
            Console.WriteLine("Private repos: " + private_Repos);
            Console.WriteLine("public repos: " + public_repos);
            Console.WriteLine("Email: " + email);
            Console.ResetColor();
        }
    }
    class Program
    {
        static string separatorString = "*******";
        static void Main(string[] args)
        {
            while (true)
            {
                var client = SetUp();
                
                Console.WriteLine("Enter username");
                var input = Console.ReadLine();
                
                var task = UserFromGit(input, client);
                task.Wait();
                
                Console.WriteLine("Enter \"1\" to go to repositories");
                Console.WriteLine("Enter any other input to exit");
                input = Console.ReadLine();
                if (input == "1")
                {
                    
                }
                Console.WriteLine("Closing client.");
                client.Dispose();
            }
        }

        static async Task UserFromGit(string input, HttpClient client)
        {
            var response = await client.GetAsync(input);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            var streamReader = new StreamReader(stream);
            var responseString = await streamReader.ReadToEndAsync();

            var userResponse = JsonSerializer.Deserialize<UserResponse>(responseString);
            

            
            Separator();
            Console.WriteLine(response);
            Separator();
            Console.WriteLine(responseString);
            Separator();
            userResponse.PrintFields();
            Separator();
        }

        static void Separator()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 3; i++)
                Console.WriteLine(separatorString);
            Console.ResetColor();
        }

        static HttpClient SetUp()
        {
            HttpClient client = new HttpClient();
            var token = Env.token; //Class Env need to be created with your token in string "token" 
                
            client.BaseAddress = new Uri("https://api.github.com/users/");
            client.DefaultRequestHeaders.UserAgent.Add
                (new ProductInfoHeaderValue("GithubExplorer", "1.0"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("token", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
