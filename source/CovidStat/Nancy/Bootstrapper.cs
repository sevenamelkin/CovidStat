using System;
using Autofac;
using CovidStat.Db.Context;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Serilog.Extensions.Logging;
using StackExchange.Redis;
using static CovidStat.Program;
using static CovidStat.Constants;

namespace CovidStat.Nancy
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder =>
            {
                CheckConfiguration(Configuration);
                builder.Register(c => Configuration).As<IConfiguration>();
                builder.Register((c, p) =>
                    new LoggerFactory(new ILoggerProvider[] 
                    {
                        new SerilogLoggerProvider(Logger)
                    }))
                    .As<ILoggerFactory>();
                builder.RegisterType<CountryService>().As<ICountryService>();
                builder.RegisterType<IpService>().As<IIpService>();
                builder.RegisterType<MainService>().As<IMainService>();
                builder.RegisterType<CovidStatDbContext>().WithParameter("options", GetDbOptions())
                    .InstancePerLifetimeScope();;
                builder.Register(c => ConnectionMultiplexer.Connect(Configuration[RedisConnectionString])).SingleInstance();
                builder.Register(c => c.Resolve<ConnectionMultiplexer>().GetDatabase());
            });
            base.ConfigureApplicationContainer(container);
        }

        private static void CheckConfiguration(IConfiguration configuration)
        {
            var isConfigurationInvalid = string.IsNullOrEmpty(configuration[NpgConnectionString])
                || string.IsNullOrEmpty(configuration[NpgConnectionString])
                || string.IsNullOrEmpty(configuration[RedisConnectionString])
                || string.IsNullOrEmpty(configuration[Host])
                || string.IsNullOrEmpty(configuration[Port])
                || string.IsNullOrEmpty(configuration[UrlRequest]);

            if (isConfigurationInvalid)
            {
                throw new Exception("Configuration invalid, not all sections are filled");
            }
        }
        
        private static DbContextOptions GetDbOptions()
        {
            return new DbContextOptionsBuilder<CovidStatDbContext>()
                .UseNpgsql(Configuration[NpgConnectionString])
                .Options;
        }
        
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}