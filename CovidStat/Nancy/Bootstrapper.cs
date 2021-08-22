using Autofac;
using CovidStat.Db.Context;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public static IContainer Container;
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder =>
            {
                builder.Register(c => Configuration).As<IConfiguration>();
                builder.Register(l => Logger).As<ILogger>();
                builder.RegisterType<BaseService>().As<IBaseService>();
                builder.RegisterType<CovidStatDbContext>().WithParameter("options", GetDbOptions())
                    .InstancePerLifetimeScope();;
                builder.Register<ConnectionMultiplexer>(c => ConnectionMultiplexer.Connect(Configuration[RedisConnectionString])).SingleInstance();
                builder.Register<IDatabase>(c => c.Resolve<ConnectionMultiplexer>().GetDatabase());
                builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();
            });
            base.ConfigureApplicationContainer(container);
            Container = (IContainer) container;
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