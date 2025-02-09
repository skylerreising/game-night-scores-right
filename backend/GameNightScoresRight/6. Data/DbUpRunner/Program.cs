using DbUp;
using Microsoft.Extensions.Configuration;

namespace DbUpRunner
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var connectionString = configuration.GetConnectionString("GameNightDatabase");
            var testConnectionString = configuration.GetConnectionString("GameNightTestDatabase");

            var connectionStrings = new List<string>
            {
                connectionString,
                testConnectionString
            };

            var scriptsPath = "../../6. Data/SQL Scripts";

            foreach (var connectString in connectionStrings)
            {
                EnsureDatabase.For.SqlDatabase(connectString);

                var upgrader =
                    DeployChanges.To
                        .SqlDatabase(connectString)
                        .WithScriptsFromFileSystem(scriptsPath)
                        .LogToConsole()
                        .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + result.Error);
                    Console.ResetColor();
                    return -1;
                }
                else
                {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Database upgrade successful!");
                Console.ResetColor();
                }
            }

            return 0;
        }
    }
}
