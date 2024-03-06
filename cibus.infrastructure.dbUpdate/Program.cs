using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace cibus.infrastructure.dbUpdate
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfiguration configuration = BuildConfiguration();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransactionPerScript()
                .WithExecutionTimeout(TimeSpan.FromMinutes(10))
                .LogToConsole()
                .Build();

#if DEBUG
            EnsureDatabase.For.SqlDatabase(connectionString);
#endif

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success..");
            Console.ResetColor();
            return 0;
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
