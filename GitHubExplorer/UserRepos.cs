using System;

namespace GitHubExplorer
{
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
}