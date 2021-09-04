using System.Linq;
using CovidStat.Db.Context;
using CovidStat.Db.Entities;
using CovidStat.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CovidStat.Services
{
    ///<inheritdoc cref = "IIpService"/>
    public class IpService : IIpService
    {
        private readonly ILogger<IpService> _logger;
        private readonly IDatabase _database;
        private readonly CovidStatDbContext _dbContext;

        public IpService(CovidStatDbContext dbContext, ILoggerFactory loggerFactory, IDatabase database)
        {
            _logger = loggerFactory.CreateLogger<IpService>();
            _database = database;
            _dbContext = dbContext;
        }

        ///<inheritdoc />
        public IpLocation GetCountryByIp(long ip)
        {
            _logger.LogInformation($"{nameof(GetCountryByIp)} start with ip: {ip}");
            var ipString = ip.ToString();
            
            var ipLocationFromCache = _database.StringGet(ipString);

            if (!string.IsNullOrEmpty(ipLocationFromCache))
            {
                _logger.LogInformation($"ip location by ip: {ipString} found in cache, return");
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
                _logger.LogInformation($"ip location by ip: {ipString} found in database and added to cache, return");
            }
            
            return ipLocationFromEf;
        }
    }
}