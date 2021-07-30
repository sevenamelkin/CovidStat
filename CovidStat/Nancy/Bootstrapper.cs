using System;
using Autofac;
using CovidStat.Interfaces;
using CovidStat.Services;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrappers.Autofac;
using Serilog;
using static CovidStat.Program;

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
            });
            base.ConfigureApplicationContainer(container);
        }
    }
}