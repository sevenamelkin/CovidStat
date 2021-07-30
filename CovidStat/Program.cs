using System;
using System.Net;
using Nancy.Hosting.Self;

namespace CovidStat
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() {CreateAutomatically = true}
            };
            
            using (var host = new NancyHost(hostConfigs, new Uri("http://localhost:12554/")))
            {
                /*logger.Information($"Try start nancy with address: {httpConfig.UriAddress}");
                host.Start();
                logger.Information("Nancy started");
                Console.ReadLine();*/
                host.Start();
                Console.ReadLine();
            }
        }
    }
}