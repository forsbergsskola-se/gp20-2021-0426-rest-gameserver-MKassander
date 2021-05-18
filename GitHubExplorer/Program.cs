using System;
using System.Collections.Generic;
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

    public class UserRepos
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime pushed_at { get; set; }
        public DateTime LastPush => pushed_at.ToLocalTime();
        
        public void PrintFields()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("Last push: " + LastPush);
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
                var firstInput = Console.ReadLine();
                
                var userTask = GetFromGit(firstInput, client, false);
                userTask.Wait();
                
                Console.WriteLine("Enter \"1\" to go to repositories");
                Console.WriteLine("Enter any other input to exit");
                var secondInput = Console.ReadLine();
                if (secondInput == "1")
                {
                    var url = firstInput + "/repos";
                    var repoTask = GetFromGit(url, client, true);
                    repoTask.Wait();
                }
                Console.WriteLine("Closing client.");
                client.Dispose();
            }
        }

        static async Task GetFromGit(string input, HttpClient client, bool repos)
        {
            var response = await client.GetAsync(input);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            var streamReader = new StreamReader(stream);
            var responseString = await streamReader.ReadToEndAsync();

            if (repos)
            {
                Console.WriteLine(responseString);
                var userResponse = JsonSerializer.Deserialize<List<UserRepos>>(responseString);
                Separator(ConsoleColor.Yellow);
                foreach (var repo in userResponse)
                    repo.PrintFields();
                Separator(ConsoleColor.Yellow);
            }
            else
            {
                var userResponse = JsonSerializer.Deserialize<UserResponse>(responseString);
                Separator(ConsoleColor.Blue);
                userResponse.PrintFields();
                Separator(ConsoleColor.Blue);
            }
        }

        static void Separator(ConsoleColor color)
        {
            Console.ForegroundColor = color;
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
            client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            return client;
        }
    }
}
