using System.Linq;
using CovidStat.Db.Context;
using CovidStat.Db.Entities;
using CovidStat.Interfaces;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;

namespace CovidStat.Services
{
    ///<inheritdoc cref = "IIpService"/>
    public class IpService : IIpService
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;
        private readonly CovidStatDbContext _dbContext;

        public IpService(CovidStatDbContext dbContext, ILogger logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
            _dbContext = dbContext;
        }

        ///<inheritdoc />
        public IpLocation GetCountryByIp(long ip)
        {
            _logger.Information($"{nameof(GetCountryByIp)} start with ip: {ip}");
            var ipString = ip.ToString();
            
            var ipLocationFromCache = _database.StringGet(ipString);

            if (!string.IsNullOrEmpty(ipLocationFromCache))
            {
                _logger.Information($"ip location by ip: {ipString} found in cache, return");
                return JsonConvert.DeserializeObject<IpLocation>(ipLocationFromCache);
            }

            var ipLocationFromEf = _dbContext
                .IpLocations
                .FirstOrDefault(iplocation => iplocation.IpFrom < ip && iplocation.IpTo > ip);

            if (ipLocationFromEf is null || ipLocationFromEf.CountryName.Equals("-"))
            {
                return null;
            }
            
            var successfullyAddCache = _database.StringSet(ipString, JsonConvert.SerializeObject(ipLocationFromEf));

            if (successfullyAddCache)
            {
                _logger.Information($"ip location by ip: {ipString} found in database and added to cache, return");
            }
            
            return ipLocationFromEf;
        }
    }
}