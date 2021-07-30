using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Serilog;
using Nancy.TinyIoc;
using Serilog;

namespace CovidStat.Nancy
{
    public class Bootstrapper : AutofacNancyBootstrapper//: BaseBootstrapper
    {
        private const string ConfigPath = "appsettings.json";
        
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            var config = BuildConfiguration(ConfigPath);
            var logger = CreateLogger(config);
            container.Update(builder =>
            {
                builder.Register(c => config).As<IConfiguration>();
                builder.Register(l => logger).As<ILogger>();
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
        
        private static ILogger CreateLogger(IConfiguration configuration)
        {
            return Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Service", $"Adapter")
                .CreateLogger();
        }
    }
}