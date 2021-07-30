using System;
using Autofac;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrappers.Autofac;
using NLog;
using Serilog;

namespace CovidStat.Nancy
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private const string ConfigPath = "appsettings.json";
        
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            var config = BuildConfiguration(ConfigPath);
            var logger = CreateLogger();
            container.Update(builder =>
            {
                builder.Register(c => config).As<IConfiguration>();
                builder.Register(l => logger).As<ILogger>();
                builder.RegisterType<BaseService>().As<IBaseService>();
            });
            base.ConfigureApplicationContainer(container);
        }
        
        private static IConfiguration BuildConfiguration(string configPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(configPath)
                .Build();
        }
        
        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"log\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}