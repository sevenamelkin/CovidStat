using Autofac;
using CovidStat.Db.Context;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nancy.Bootstrappers.Autofac;
using Serilog;
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
                builder.RegisterType<BaseService>().As<IBaseService>();
                builder.RegisterType<CovidStatDbContext>().WithParameter("options", GetDbOptions())
                    .InstancePerLifetimeScope();;
            });
            base.ConfigureApplicationContainer(container);
        }

        private static DbContextOptions GetDbOptions()
        {
            return new DbContextOptionsBuilder<CovidStatDbContext>()
                .UseNpgsql(Configuration[NpgConnectionString])
                .Options;
        }
    }
}