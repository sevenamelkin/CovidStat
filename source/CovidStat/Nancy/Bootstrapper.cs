using Autofac;
using CovidStat.Db.Context;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Serilog;
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
                builder.Register(c => Configuration).As<IConfiguration>();
                builder.Register(l => Logger).As<ILogger>();
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