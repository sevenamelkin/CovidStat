using System;
using Microsoft.Extensions.Configuration;
using Nancy.Hosting.Self;
using Serilog;
using static CovidStat.Constants;

namespace CovidStat
{
    class Program
    {
        public static IConfiguration Configuration;
        public static ILogger Logger;
        private const string ConfigPath = "appsettings.json";

        public static void Main()
        {
            Configuration = BuildConfiguration(ConfigPath);

            Logger = CreateLogger();

            var uri = new UriBuilder(Uri.UriSchemeHttp, Configuration[Host], Convert.ToInt32(Configuration[Port])).Uri;

            using var host = new NancyHost(uri);
            Logger.Information($"Try start nancy with address: {uri}");

            host.Start();
            Logger.Information("Nancy started");

            Console.ReadLine();
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"log\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }

        private static IConfiguration BuildConfiguration(string configPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(configPath)
                .Build();
        }
    }
}