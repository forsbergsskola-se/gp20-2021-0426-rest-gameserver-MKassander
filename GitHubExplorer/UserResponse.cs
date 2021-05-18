using System;

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
}