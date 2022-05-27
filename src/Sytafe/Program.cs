using Microsoft.Extensions.Configuration;
using Sytafe.Models;

namespace Sytafe
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var settings = config.Get<AppSettings>();

            ApplicationConfiguration.Initialize();
            Application.Run(new AppForm(settings));
        }
    }
}