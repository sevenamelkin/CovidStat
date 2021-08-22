using System;
using System.Linq;
using System.Threading.Tasks;
using CovidStat.Db.Context;
using CovidStat.Interfaces;
using EFCache.Redis;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;

namespace CovidStat.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly CovidStatDbContext _dbContext;
        private readonly ILogger _logger;
        
        public CacheService(ILogger logger, IDatabase database, CovidStatDbContext context)
        {
            _logger = logger;
            _database = database;
            _dbContext = context;
        }

        public void LoadCache()
        {
            while (true)
            {
                try
                {
                    _logger.Information($"{nameof(LoadCache)} start, try load cache");
                    var ipLocations = _dbContext.IpLocations.ToList();
                    
                    foreach (var ipLocation in ipLocations)
                    {
                        _database.SetAdd("ipLocation", JsonConvert.SerializeObject(ipLocation));
                    }
                    
                    _logger.Information($"{nameof(LoadCache)} successfully loaded");
                    break;
                }
                catch (Exception e)
                {
                    _logger.Error($"{nameof(LoadCache)} is failed with message: {e.Message}, wait 2min. for start a repeat");
                    Task.Delay(TimeSpan.FromMinutes(2)).Wait();
                }
            }
        }
    }
}